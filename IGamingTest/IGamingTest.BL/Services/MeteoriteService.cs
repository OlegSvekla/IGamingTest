using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Entities.Meteorite;
using IGamingTest.Core.Models;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Text.Json;

namespace IGamingTest.BL.Services;

public interface IMeteoriteService
{
    Task SyncMeteoritesFromExternalSourceAsync(CancellationToken ct);
    Task<IReadOnlyCollection<MeteoriteGroupedDto>> GetFilteredGroupedMeteoritesAsync(
        MeteoriteFilterQuery rq,
        CancellationToken ct);
}

public class MeteoriteService(
    HttpClient httpClient,
    IUow uow
    ) : IMeteoriteService
{
    public async Task SyncMeteoritesFromExternalSourceAsync(CancellationToken ct)
    {
        var incomingDtos = await GetMeteoritesAsync(ct);

        var incomingIds = incomingDtos.Select(dto => dto.Id).ToList();
        var incomingById = incomingDtos.ToDictionary(dto => dto.Id);

        //await UpdateModifiedMeteoritesAsync(
        //    incomingIds,
        //    incomingById,
        //    incomingDtos,
        //    ct);

        //await uow.MeteoriteRepository.DeleteWhereAsync(
        //    predicate: x => !incomingIds.Contains(x.Id),
        //    ct: ct);

        //var entities = MapDtosToEntities(incomingDtos);

        //await uow.MeteoriteRepository.CreateIfNotExistAsync(
        //    entities,
        //    entity => x => x.Id == entity.Id,
        //    ct);
    }

    public async Task<IReadOnlyCollection<MeteoriteGroupedDto>> GetFilteredGroupedMeteoritesAsync(
        MeteoriteFilterQuery rq,
        CancellationToken ct)
    {
        var filter = BuildFilterExpression(rq);
        var sorter = BuildGroupedSorter(rq.SortBy);

        var grouped = await uow.MeteoriteRepository.GetAllGroupedAsync(
            groupSelector: g => new MeteoriteGroupedDto
            {
                Year = g.Key,
                Count = g.Count(),
                TotalMass = g.Sum(x => x.Mass)
            },
            groupBy: e => e.Year,
            amount: rq.Amount,
            offset: rq.Offset,
            predicate: filter,
            ct: ct);

        var sorted = BuildGroupedSorter(rq.SortBy)(grouped);

        return sorted.ToList();
    }

    private Expression<Func<MeteoriteEntity, bool>> BuildFilterExpression(MeteoriteFilterQuery rq)
    {
        return e =>
            (!rq.YearFrom.HasValue || e.Year >= rq.YearFrom.Value) &&
            (!rq.YearTo.HasValue || e.Year <= rq.YearTo.Value) &&
            (string.IsNullOrEmpty(rq.RecClass) || e.RecClass == rq.RecClass) &&
            (string.IsNullOrEmpty(rq.NameContains) || e.Name.Contains(rq.NameContains));
    }



    private Func<IEnumerable<MeteoriteGroupedDto>, IOrderedEnumerable<MeteoriteGroupedDto>> BuildGroupedSorter(string? sortBy)
    {
        var desc = sortBy?.EndsWith("desc", StringComparison.OrdinalIgnoreCase) ?? false;
        var prop = sortBy?.Split(' ')[0].ToLower();

        return q => prop switch
        {
            "count" => desc ? q.OrderByDescending(x => x.Count) : q.OrderBy(x => x.Count),
            "mass" => desc ? q.OrderByDescending(x => x.TotalMass) : q.OrderBy(x => x.TotalMass),
            "year" or _ => desc ? q.OrderByDescending(x => x.Year) : q.OrderBy(x => x.Year)
        };
    }

    public async Task<IReadOnlyCollection<MeteoriteDto>> GetMeteoritesAsync(CancellationToken ct)
    {
        try
        {
            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/biggiko/nasa-dataset/refs/heads/main/y77d-th95.json", ct);

            if (response == null || !response.IsSuccessStatusCode)
                throw new HttpRequestException("Invalid HTTP response from external service.");

            if (response.Content == null)
                throw new InvalidOperationException("Response content is null.");

            var contentType = response.Content.Headers.ContentType?.MediaType;
            if (!string.Equals(contentType, "text/plain", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Unexpected content type: {contentType}");

            var json = await response.Content.ReadAsStringAsync(ct);

            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("Received empty JSON.");

            var dtos = JsonSerializer.Deserialize<IReadOnlyCollection<MeteoriteDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return dtos ?? Array.Empty<MeteoriteDto>();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "Failed to fetch meteorites from remote service");
            throw new ApplicationException("Error occurred while fetching meteorites.", ex);
        }
    }

    private async Task UpdateModifiedMeteoritesAsync(
        IReadOnlyCollection<Guid> incomingIds,
        IDictionary<Guid, MeteoriteDto> incomingById,
        IReadOnlyCollection<MeteoriteDto> incomingDtos,
        CancellationToken ct)
    {
        var changedMeteorites = await GetMeteoritesToChangeAsync(
            incomingIds, incomingById, incomingDtos, ct);

        await UpdateMeteoritesAsync(incomingById, changedMeteorites.AsReadOnly(), ct);
    }

    private async Task UpdateMeteoritesAsync(
        IDictionary<Guid, MeteoriteDto> incomingById,
        IReadOnlyCollection<MeteoriteEntity> entitiesToUpdate,
        CancellationToken ct)
    {
        foreach (var entity in entitiesToUpdate)
        {
            //if (incomingById.TryGetValue(entity.Id, out var dto))
            //{
            //    await UpdateEntityIfChangedAsync(entity, dto, ct);
            //}
        }
    }

    private async Task UpdateEntityIfChangedAsync(
        MeteoriteEntity entity,
        MeteoriteDto dto,
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

        if (entity.Geolocation.Type != dto.Geolocation.Type)
        {
            entity.Geolocation.Type = dto.Geolocation.Type;
            await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation.Type, ct);
        }

        //if (entity.Geolocation.Geo.Latitude != dto.Geolocation.Coordinates.Latitude)
        //{
        //    entity.Geolocation.Geo.Latitude = dto.Geolocation.Coordinates.Latitude;
        //    await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation.Geo.Latitude, ct);
        //}

        //if (entity.Geolocation.Geo.Longitude != dto.Geolocation.Coordinates.Longitude)
        //{
        //    entity.Geolocation.Geo.Longitude = dto.Geolocation.Coordinates.Longitude;
        //    await uow.MeteoriteRepository.UpdateAsync(entity, e => e.Geolocation.Geo.Longitude, ct);
        //}

        await uow.CommitAsync(ct);
    }

    private async Task<IList<MeteoriteEntity>> GetMeteoritesToChangeAsync(
        IReadOnlyCollection<Guid> meteoriteIds,
        IDictionary<Guid, MeteoriteDto> meteoritesById,
        IReadOnlyCollection<MeteoriteDto> dtos,
        CancellationToken ct)
    {
        var entities = await uow.MeteoriteRepository.GetAllAsync(
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
            //predicate: x =>
            //    meteoriteIds.Contains(x.Id) &&
            //    (
            //        x.NameType != meteoritesById[x.Id].NameType ||
            //        x.RecClass != meteoritesById[x.Id].RecClass ||
            //        x.Mass != meteoritesById[x.Id].MassValue ||
            //        x.Fall != meteoritesById[x.Id].Fall ||
            //        x.Year != meteoritesById[x.Id].YearValue ||
            //        x.RecLat != meteoritesById[x.Id].RecLat ||
            //        x.RecLong != meteoritesById[x.Id].RecLong ||
            //        x.Geolocation.Geo.Latitude != meteoritesById[x.Id].Geolocation.Coordinates.Latitude ||
            //        x.Geolocation.Geo.Longitude != meteoritesById[x.Id].Geolocation.Coordinates.Longitude
            //    ),
            ct: ct
        );

        return entities;
    }

    //private List<MeteoriteEntity> MapDtosToEntities(IReadOnlyCollection<MeteoriteDto> dtos)
    //{
    //    return dtos.Select(dto => new MeteoriteEntity
    //    {
    //        Id = dto.Id,
    //        Name = dto.Name,
    //        NameType = dto.NameType,
    //        RecClass = dto.RecClass,
    //        Mass = dto.MassValue,
    //        Fall = dto.Fall,
    //        Year = dto.YearValue,
    //        RecLat = dto.RecLat,
    //        RecLong = dto.RecLong,

    //        Geolocation = new GeoLocationEntity
    //        {
    //            Id = Guid.NewGuid(),
    //            Type = dto.Geolocation.Type,
    //            Geo = dto.Geolocation.Coordinates
    //        }
    //    }).ToList();
    //}
}
