using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Resources;
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Services
{
    public class LocalizationService : ILocalizationService
    {
        public static List<string> SupportedCultures { get; }

        private readonly IUserService service;

        public LocalizationService(IUserService userService)
        {
            service = userService;
        }

        static LocalizationService()
        {
            var cultures = AppSettings.SupportedCultures;
            var splitted = cultures.Split(',');
            SupportedCultures = new List<string>(splitted);
        }

        public async Task ApplyUserCultureAsync(int userTelegramId)
        {
            var user = await service.FindUserAsync(userTelegramId);
            CultureInfo culture;

            if (user == null)
            {
                var defaultCulture = AppSettings.DefaultCulture;
                await service.CreateUserAsync(new Models.User.User
                {
                    UserTelegramId = userTelegramId,
                    LanguageCode = defaultCulture
                });
                culture = CultureInfo.GetCultureInfo(defaultCulture);
            }
            else
            {
                culture = CultureInfo.GetCultureInfo(user.LanguageCode);
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        public async Task<bool> AddUserLanguageAsync(int userTelegramId, string language)
        {
            var manager = new ResourceManager(typeof(Languages));
            var languageCode = manager.GetString(language);

            if (!string.IsNullOrEmpty(languageCode))
            {
                await service.SetUserLanguageCodeAsync(userTelegramId, languageCode);
                return true;
            }

            return false;
        }
    }
}