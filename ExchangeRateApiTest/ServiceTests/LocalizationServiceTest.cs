using System.Globalization;
using System.Threading.Tasks;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services;
using ExchangeRateApi.Services.Interfaces;
using ExchangeRateApiTest.Fixtures;
using Moq;
using Xunit;

namespace ExchangeRateApiTest.ServiceTests
{
    public class LocalizationServiceTest : IClassFixture<LocalizationFixture>, IClassFixture<UserFixture>
    {
        private readonly LocalizationFixture fixture;
        private readonly UserFixture userFixture;
        private readonly Mock<IUserService> mockUserService;
        private readonly ILocalizationService service;

        public LocalizationServiceTest(LocalizationFixture fixture, UserFixture userFixture)
        {
            this.fixture = fixture;
            this.userFixture = userFixture;
            
            mockUserService = new Mock<IUserService>();
            service = new LocalizationService(mockUserService.Object);
        }

        [Fact]
        public async Task ApplyUserCultureAsync_UserExists_UserCultureApplied()
        {
            SetDefaultCulture(fixture.EnglishLanguage);
            SetupFind(mockUserService, userFixture.UserWithLanguageCode);

            await service.ApplyUserCultureAsync(userFixture.UserId);
            
            VerifyApplyCulture(mockUserService, userFixture.UserWithLanguageCode.LanguageCode);
        }
        
        [Fact]
        public async Task ApplyUserCultureAsync_UserDoesntExists_DefaultCultureApplied()
        {
            SetDefaultCulture(fixture.UkraineLanguage);
            SetupFind(mockUserService, null);
            mockUserService.Setup(x => x.CreateUserAsync(It.IsAny<User>()));
            
            await service.ApplyUserCultureAsync(userFixture.UserId);
            
            mockUserService.Verify(x => x.CreateUserAsync(It.Is<User>(user 
                => user.UserTelegramId == userFixture.UserId 
                   && user.LanguageCode == fixture.DefaultCulture)), Times.Once);
            VerifyApplyCulture(mockUserService, fixture.DefaultCulture);
        }
        
        [Fact]
        public async Task AddUserLanguageAsync_LanguageExists_LanguageAdded()
        {
            SetupSetLanguageCode(mockUserService);

            var result = await service.AddUserLanguageAsync(userFixture.UserId, fixture.UkraineLanguageName);
            
            VerifySetLanguageCode(mockUserService, fixture.UkraineLanguage, Times.Once());
            Assert.True(result);
        }
        
        [Fact]
        public async Task AddUserLanguageAsync_LanguageDoesntExists_LanguageNotAdded()
        {
            SetupSetLanguageCode(mockUserService);
            
            var result = await service.AddUserLanguageAsync(userFixture.UserId, fixture.FakeLanguage);
            
            VerifySetLanguageCode(mockUserService, fixture.FakeLanguage, Times.Never());
            Assert.False(result);
        }

        private void SetDefaultCulture(string twoLetterLanguageName)
        {
            var culture = CultureInfo.GetCultureInfo(twoLetterLanguageName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        
        private void SetupFind(Mock<IUserService> mock, User returnedUser)
        {
            mock.Setup(x => x.FindUserAsync(It.IsAny<int>())).ReturnsAsync(returnedUser);
        }
        
        private void SetupSetLanguageCode(Mock<IUserService> mock)
        {
            mock.Setup(x => x.SetUserLanguageCodeAsync(It.IsAny<int>(), It.IsAny<string>()));
        }
        
        private void VerifyApplyCulture(Mock<IUserService> mock, string twoLetterLanguageName)
        {
            mock.Verify(x => x.FindUserAsync(userFixture.UserId), Times.Once);
            Assert.True(CultureInfo.DefaultThreadCurrentCulture.TwoLetterISOLanguageName
                .Equals(twoLetterLanguageName));
            Assert.True(CultureInfo.DefaultThreadCurrentUICulture.TwoLetterISOLanguageName
                .Equals(twoLetterLanguageName));
        }
        
        private void VerifySetLanguageCode(Mock<IUserService> mock, string languageCode, Times methodInvocations)
        {
            mock.Verify(x => x.SetUserLanguageCodeAsync(userFixture.UserId, languageCode), methodInvocations);
        }
    }
}