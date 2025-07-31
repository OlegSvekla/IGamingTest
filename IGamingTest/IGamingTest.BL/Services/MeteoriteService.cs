using IGamingTest.Core.Entities.Common;
using IGamingTest.Core.Entities.Meteorite;
using IGamingTest.Core.Helpers;
using IGamingTest.Core.Http.Out;
using IGamingTest.Core.Http.Out.Models;
using IGamingTest.Core.Models;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace IGamingTest.BL.Services;

public interface IMeteoriteService
{
    Task SyncMeteoritesFromExternalSourceAsync(CancellationToken ct);
}

public class MeteoriteService(
    IHttpSender httpSender,
    ILogger<MeteoriteService> logger,
    IUow uow
    ) : IMeteoriteService
{
    public async Task SyncMeteoritesFromExternalSourceAsync(CancellationToken ct)
    {
        var incomingDtos = await GetMeteoritesAsync(ct);

        var incomingIds = incomingDtos.Select(dto => dto.IdValue).ToList();
        var incomingById = incomingDtos.ToDictionary(dto => dto.IdValue);

        await UpdateModifiedMeteoritesAsync(
            incomingIds,
            incomingById,
            incomingDtos,
            ct);

        await uow.MeteoriteRepository.DeleteWhereAsync(
            predicate: x => !incomingIds.Contains(x.Id),
            ct: ct);

        var entities = MapDtosToEntities(incomingDtos);

        await uow.MeteoriteRepository.CreateIfNotExistAsync(
            entities,
            entity => x => x.Id == entity.Id,
            ct);
    }

    public async Task<IReadOnlyCollection<GetMeteoriteQueryRs>> GetMeteoritesAsync(CancellationToken ct)
    {
        try
        {
            var method = HttpMethod.Get;
            var uri = new Uri($"https://raw.githubusercontent.com/biggiko/nasa-dataset/refs/heads/main/y77d-th95.json");

            var rq = new HttpRq(
            Uri: uri,
            Method: method);

            var meteorites = await httpSender.ForceSendAsync<IReadOnlyCollection<GetMeteoriteQueryRs>>(
                rq: rq,
                ct: ct);

            return meteorites;
        }
        catch (Exception)
        {
            logger.LogWarning($"Error occurred while fetching meteorites.");
            return Array.Empty<GetMeteoriteQueryRs>();
        }
    }

    private async Task UpdateModifiedMeteoritesAsync(
        IReadOnlyCollection<int> incomingIds,
        IDictionary<int, GetMeteoriteQueryRs> incomingById,
        IReadOnlyCollection<GetMeteoriteQueryRs> incomingDtos,
        CancellationToken ct)
    {
        var changedMeteorites = await GetMeteoritesToChangeAsync(
            incomingIds, incomingById, incomingDtos, ct);

        await UpdateMeteoritesAsync(incomingById, changedMeteorites.AsReadOnly(), ct);
    }

    private async Task UpdateMeteoritesAsync(
        IDictionary<int, GetMeteoriteQueryRs> incomingById,
        IReadOnlyCollection<MeteoriteEntity> entitiesToUpdate,
        CancellationToken ct)
    {
        foreach (var entity in entitiesToUpdate)
        {
            if (incomingById.TryGetValue(entity.Id, out var dto))
            {
                await UpdateEntityIfChangedAsync(entity, dto, ct);
            }
        }
    }

    private async Task UpdateEntityIfChangedAsync(
        MeteoriteEntity entity,
        GetMeteoriteQueryRs dto,
        CancellationToken ct)
    {
        if (entity.NameType != dto.NameType)
        {
            entity.NameType = dto.NameType;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.NameType, ct);
        }

        if (entity.RecClass != dto.RecClass)
        {
            entity.RecClass = dto.RecClass;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.RecClass, ct);
        }

        if (entity.Mass != dto.MassValue)
        {
            entity.Mass = dto.MassValue;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Mass, ct);
        }

        if (entity.Fall != dto.Fall)
        {
            entity.Fall = dto.Fall;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Fall, ct);
        }

        if (entity.Year != dto.YearValue)
        {
            entity.Year = dto.YearValue;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Year, ct);
        }

        if (entity.RecLat != dto.RecLat)
        {
            entity.RecLat = dto.RecLat;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.RecLat, ct);
        }

        if (entity.RecLong != dto.RecLong)
        {
            entity.RecLong = dto.RecLong;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.RecLong, ct);
        }

        await UpdateGeolocationIfChangedAsync(entity, dto, uow, ct);

        await uow.CommitAsync(ct);
    }

    private async Task UpdateGeolocationIfChangedAsync(
        MeteoriteEntity entity,
        GetMeteoriteQueryRs dto,
        IUow uow,
        CancellationToken ct)
    {
        var entityGeoLoc = entity.Geolocation;
        var dtoGeoLoc = dto.Geolocation;

        if (entityGeoLoc == null || dtoGeoLoc == null)
            return;

        if (entityGeoLoc.Type != dtoGeoLoc.Type)
        {
            entityGeoLoc.Type = dtoGeoLoc.Type;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation!.Type, ct);
        }

        var entityGeo = entityGeoLoc.Geo;
        var dtoGeo = dtoGeoLoc.Geo;

        if (entityGeo == null || dtoGeo == null)
            return;

        if (entityGeo.Latitude != dtoGeo.Latitude)
        {
            entityGeo.Latitude = dtoGeo.Latitude;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation!.Geo!.Latitude, ct);
        }

        if (entityGeo.Longitude != dtoGeo.Longitude)
        {
            entityGeo.Longitude = dtoGeo.Longitude;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation!.Geo!.Longitude, ct);
        }
    }


    private async Task<IList<MeteoriteEntity>> GetMeteoritesToChangeAsync(
        IReadOnlyCollection<int> meteoriteIds,
        IDictionary<int, GetMeteoriteQueryRs> meteoritesById,
        IReadOnlyCollection<GetMeteoriteQueryRs> dtos,
        CancellationToken ct)
    {
        const int batchSize = 20;
        var result = new List<MeteoriteEntity>();

        var allDtos = meteoritesById.Values.ToList();

        for (int i = 0; i < allDtos.Count; i += batchSize)
        {
            var batchDtos = allDtos.Skip(i).Take(batchSize).ToList();

            var predicate = PredicateBuilder.False<MeteoriteEntity>();

            foreach (var dto in batchDtos)
            {
                var id = dto.IdValue;
                var nameType = dto.NameType;
                var recClass = dto.RecClass;
                var mass = dto.MassValue;
                var fall = dto.Fall;
                var year = dto.YearValue;
                var recLat = dto.RecLat;
                var recLong = dto.RecLong;

                var geoLat = dto.Geolocation?.Geo?.Latitude;
                var geoLong = dto.Geolocation?.Geo?.Longitude;

                predicate = predicate.Or(x =>
                    x.Id == id &&
                    (
                        x.NameType != nameType ||
                        x.RecClass != recClass ||
                        x.Mass != mass ||
                        x.Fall != fall ||
                        x.Year != year ||
                        x.RecLat != recLat ||
                        x.RecLong != recLong ||
                        (x.Geolocation != null &&
                         x.Geolocation.Geo != null &&
                         (
                             x.Geolocation.Geo.Latitude != geoLat ||
                             x.Geolocation.Geo.Longitude != geoLong
                         ))
                    )
                );
            }

            var batchEntities = await uow.MeteoriteRepository.GetAllAsync(
                selector: x => new MeteoriteEntity
                {
                    Id = x.Id,
                    NameType = x.NameType,
                    RecClass = x.RecClass,
                    Mass = x.Mass,
                    Fall = x.Fall,
                    Year = x.Year,
                    RecLat = x.RecLat,
                    RecLong = x.RecLong,
                    Geolocation = x.Geolocation,
                },
                predicate: predicate,
                ct: ct
            );

            result.AddRange(batchEntities);
        }

        return result;
    }


    private List<MeteoriteEntity> MapDtosToEntities(IReadOnlyCollection<GetMeteoriteQueryRs> dtos)
    {
        return dtos.Select(dto => new MeteoriteEntity
        {
            Id = dto.IdValue,
            Name = dto.Name,
            NameType = dto.NameType,
            RecClass = dto.RecClass,
            Mass = dto.MassValue,
            Fall = dto.Fall,
            Year = dto.YearValue,
            RecLat = dto.RecLat,
            RecLong = dto.RecLong,

            Geolocation = dto.Geolocation == null ? null : new GeoLocationEntity
            {
                Type = dto.Geolocation.Type,
                Geo = dto.Geolocation.Geo == null ? null : new Geo
                {
                    Longitude = dto.Geolocation.Geo.Longitude,
                    Latitude = dto.Geolocation.Geo.Latitude
                }
            }
        }).ToList();
    }
}
