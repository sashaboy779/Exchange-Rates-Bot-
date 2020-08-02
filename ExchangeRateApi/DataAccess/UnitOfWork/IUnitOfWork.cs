using System;
using System.Threading.Tasks;
using ExchangeRateApi.DataAccess.Repository;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<UserCurrency> UserCurrencyRepository { get; }
        Task CommitAsync();
    }
}