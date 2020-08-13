using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.DataAccess.Repository
{
    public class CachedUserCurrencyRepository : CachedRepository, IGenericRepository<UserCurrency> 
    {
        private readonly IGenericRepository<UserCurrency> repository;

        public CachedUserCurrencyRepository(IGenericRepository<UserCurrency> repository) 
        {
            this.repository = repository;
        }

        public void Create(UserCurrency entity)
        {
            repository.Create(entity);
        }

        public async Task<IEnumerable<UserCurrency>> GetAllAsync()
        {
            var allUserCurrency = Cache[CacheSettings.AllUserCurrency] as IEnumerable<UserCurrency>;

            if (allUserCurrency == null)
            {
                allUserCurrency = await repository.GetAllAsync();
                Cache.Set(CacheSettings.AllUserCurrency, allUserCurrency, Policy);
            }

            return allUserCurrency;
        }

        public async Task<UserCurrency> GetAsync(int id)
        {
            var currency = Cache[CacheSettings.Currency + id] as UserCurrency;

            if (currency == null)
            {
                currency = await repository.GetAsync(id);
                Cache.Set(CacheSettings.Currency + id, currency, Policy);
            }

            return currency;
        }

        public void Remove(UserCurrency entity)
        {
            repository.Remove(entity);
            Cache.Remove(entity.Id.ToString());
        }

        public async Task<UserCurrency> SingleOrDefaultAsync(Expression<Func<UserCurrency, bool>> predicate)
        {
            return await repository.SingleOrDefaultAsync(predicate);
        }

        public void Update(UserCurrency entity)
        {
            repository.Update(entity);
            Cache.Remove(entity.Id.ToString());
        }
    }
}