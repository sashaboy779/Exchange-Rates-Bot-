using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.DataAccess.Repository
{
    public class CachedUserRepository : CachedRepository, IGenericRepository<User>
    {
        private readonly IGenericRepository<User> repository;

        public CachedUserRepository(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }

        public void Create(User entity)
        {
            repository.Create(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var allUsers = Cache[CacheSettings.AllUsers] as IEnumerable<User>;

            if (allUsers == null)
            {
                allUsers = await repository.GetAllAsync();
                Cache.Set(CacheSettings.AllUsers, allUsers, Policy);
            }

            return allUsers;
        }

        public async Task<User> GetAsync(int id)
        {
            var user = Cache[CacheSettings.User + id] as User;

            if (user == null)
            {
                user = await repository.GetAsync(id);
                Cache.Set(CacheSettings.User + id, user, Policy);
            }

            return user;
        }

        public void Remove(User entity)
        {
            repository.Remove(entity);
            Cache.Remove(entity.Id.ToString());
        }

        public async Task<User> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await repository.SingleOrDefaultAsync(predicate);
        }

        public void Update(User entity)
        {
            repository.Update(entity);
            Cache.Remove(entity.Id.ToString());
        }
    }
}