using System.Threading.Tasks;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface ILocalizationService
    {
        Task ApplyUserCultureAsync(int userTelegramId);
        Task<bool> AddUserLanguageAsync(int userTelegramId, string language);
    }
}