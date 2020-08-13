using System.Linq;
using System.Threading.Tasks;
using ExchangeRateApi.DataAccess.UnitOfWork;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User user)
        {
            if (await FindUserAsync(user.UserTelegramId) == null)
            {
                if (!LocalizationService.SupportedCultures.Contains(user.LanguageCode))
                {
                    user.LanguageCode = AppSettings.DefaultCulture;
                }

                unitOfWork.UserRepository.Create(user);
                await unitOfWork.CommitAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync();
        }

        public async Task<User> FindUserAsync(int userTelegramId)
        {
            return await unitOfWork.UserRepository.SingleOrDefaultAsync(x => x.UserTelegramId == userTelegramId);
        }

        public async Task ManageUserCurrencyAsync(int userTelegramId, Currencies pickedCurrency)
        {
            var user = await FindUserAsync(userTelegramId);
            var userCurrency = user.Currencies.SingleOrDefault(x => x.Currency == pickedCurrency);

            if (userCurrency == null)
            {
                var currency = await unitOfWork.UserCurrencyRepository
                    .SingleOrDefaultAsync(x => x.Currency == pickedCurrency);
                user.Currencies.Add(currency);
            }
            else
            {
                user.Currencies.Remove(userCurrency);
            }

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync();
        }

        public async Task SetUserLanguageCodeAsync(int userTelegramId, string languageCode)
        {
            var user = await FindUserAsync(userTelegramId);

            if (user != null)
            {
                user.LanguageCode = languageCode;
                unitOfWork.UserRepository.Update(user);

                await unitOfWork.CommitAsync();
            }
        }
    }
}