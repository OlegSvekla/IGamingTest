using IGamingTest.Core.Entities.Meteorite;
using IGamingTest.Infrastructure.Ef.Repositories;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;

namespace IGamingTest.Infrastructure.Repositories;

public class MeteoriteRepository : Repository<MeteoriteEntity>, IMeteoriteRepository
{
    public MeteoriteRepository(GameContext dbContext) : base(dbContext)
    {
    }
}
