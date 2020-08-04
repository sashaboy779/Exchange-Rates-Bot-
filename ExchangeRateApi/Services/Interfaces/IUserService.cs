using System.Threading.Tasks;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> FindUserAsync(int userTelegramId);
        Task ManageUserCurrencyAsync(int userTelegramId, Currencies pickedCurrency);
        Task SetUserLanguageCodeAsync(int userTelegramId, string languageCode);
        Task UpdateUserAsync(User user);
    }
}