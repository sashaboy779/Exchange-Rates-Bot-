using ExchangeRateApi.Infrastructure.Constants;

namespace ExchangeRateApiTest.Fixtures
{
    public class LocalizationFixture
    {
        public string FakeLanguage { get; }
        public string EnglishLanguage { get; }
        public string UkraineLanguage { get; }
        public string UkraineLanguageName { get; }
        public string DefaultCulture { get; }

        public LocalizationFixture()
        {
            FakeLanguage = "Language doesnt exists";
            EnglishLanguage = "en";
            UkraineLanguage = "uk";
            UkraineLanguageName = "Українська";
            DefaultCulture = AppSettings.DefaultCulture;
        }
    }
}