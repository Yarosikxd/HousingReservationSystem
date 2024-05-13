using Microsoft.EntityFrameworkCore;
using Moq;

namespace HousingReservationSystemUnitTests.RepositoryesTests.DbSet
{
    public static class DbSetExtensions
    {
        public static void SetupQueryableData<TEntity>(this Mock<DbSet<TEntity>> mockSet, List<TEntity> data) where TEntity : class
        {
            var queryable = data.AsQueryable();

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        }
    }

}
