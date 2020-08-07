using System.Threading.Tasks;
using ExchangeRateApi.DataAccess.Repository;
using ExchangeRateApi.Infrastructure;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<UserCurrency> UserCurrencyRepository { get; }

        private readonly ExchangeRateBotContext context;

        public UnitOfWork(ExchangeRateBotContext context, IGenericRepository<User> userRepository,
            IGenericRepository<UserCurrency> userCurrencyRepository)
        {
            this.context = context;
            UserRepository = userRepository;
            UserCurrencyRepository = userCurrencyRepository;
        }

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                Logger.Log.Info(e.Message, e);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}