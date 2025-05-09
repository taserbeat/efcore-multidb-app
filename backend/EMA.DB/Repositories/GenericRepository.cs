using EMA.DB.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EMA.DB.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// エンティティを作成する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// エンティティを取得する
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(Guid guid);

        /// <summary>
        /// エンティティの一覧を取得する
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// エンティティを更新する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// エンティティを削除する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmaDbContextBase _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EmaDbContextBase dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Guid guid)
        {
            return await _dbSet.FindAsync(guid);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
