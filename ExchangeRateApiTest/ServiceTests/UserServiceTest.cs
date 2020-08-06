using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeRateApi.DataAccess.UnitOfWork;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services;
using ExchangeRateApi.Services.Interfaces;
using ExchangeRateApiTest.Fixtures;
using Moq;
using Xunit;

namespace ExchangeRateApiTest.ServiceTests
{
    public class UserServiceTest : IClassFixture<UserFixture>, IClassFixture<LocalizationFixture>
    {
        private readonly UserFixture fixture;
        private readonly LocalizationFixture localizationFixture;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly IUserService userService;

        public UserServiceTest(UserFixture fixture, LocalizationFixture localizationFixture)
        {
            this.fixture = fixture;
            this.localizationFixture = localizationFixture;
            
            mockUnitOfWork = new Mock<IUnitOfWork>();
            userService = new UserService(mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateUserAsync_CreateNewUser_Success()
        {
            SetupFind(mockUnitOfWork, null);
            SetupCreate(mockUnitOfWork);

            await userService.CreateUserAsync(fixture.EmptyUser);
            
            Assert.True(fixture.EmptyUser.LanguageCode == localizationFixture.DefaultCulture);
            VerifyCreate(mockUnitOfWork, Times.Once());
        }
        
        [Fact]
        public async Task CreateUserAsync_CreateSameUser_UserNotCreated()
        {
            SetupFind(mockUnitOfWork, fixture.EmptyUser);
            SetupCreate(mockUnitOfWork);
            
            await userService.CreateUserAsync(fixture.EmptyUser);
            
            VerifyCreate(mockUnitOfWork, Times.Never());
        }
        
        [Fact]
        public async Task UpdateUserAsync_UpdateUser_Success()
        {
            SetupUpdate(mockUnitOfWork);

            await userService.UpdateUserAsync(fixture.EmptyUser);
            
            VerifyUpdate(mockUnitOfWork, fixture.EmptyUser, false);
        }
        
        [Fact]
        public async Task ManageUserCurrencyAsync_PassNewCurrency_CurrencyAdded()
        {
            SetupFind(mockUnitOfWork, fixture.UserWithCurrencies);
            SetupUpdate(mockUnitOfWork);
            mockUnitOfWork.Setup(x => x.UserCurrencyRepository.SingleOrDefaultAsync(It.
                IsAny<Expression<Func<UserCurrency, bool>>>())).ReturnsAsync(fixture.RubCurrency);
            
            await userService.ManageUserCurrencyAsync(fixture.UserId, Currencies.RUB);
            
            Assert.True(fixture.UserWithCurrencies.Currencies.Contains(fixture.RubCurrency));
            mockUnitOfWork.Verify(x => x.UserCurrencyRepository.SingleOrDefaultAsync(It.
                IsAny<Expression<Func<UserCurrency, bool>>>()), Times.Once);
            VerifyUpdate(mockUnitOfWork, fixture.UserWithCurrencies);
        }
        
        [Fact]
        public async Task ManageUserCurrencyAsync_PassSameCurrency_CurrencyRemoved()
        {
            SetupFind(mockUnitOfWork, fixture.UserWithCurrencies);
            SetupUpdate(mockUnitOfWork);

            await userService.ManageUserCurrencyAsync(fixture.UserId, Currencies.USD);
            
            Assert.False(fixture.UserWithCurrencies.Currencies.Any(x => x.Currency == Currencies.USD));
            VerifyUpdate(mockUnitOfWork, fixture.UserWithCurrencies);
        }
        
        [Fact]
        public async Task SetUserLanguageCodeAsync_SetLanguage_Success()
        {
            SetupFind(mockUnitOfWork, fixture.EmptyUser);
            SetupUpdate(mockUnitOfWork);

            await userService.SetUserLanguageCodeAsync(fixture.UserId, localizationFixture.EnglishLanguage);
            
            Assert.True(fixture.EmptyUser.LanguageCode == localizationFixture.EnglishLanguage);
            VerifyUpdate(mockUnitOfWork, fixture.EmptyUser);
        }
        
        private void SetupCreate(Mock<IUnitOfWork> mock)
        {
            mock.Setup(x => x.UserRepository.Create(It.IsAny<User>()));
            mock.Setup(x => x.CommitAsync());
        }
        
        private void SetupFind(Mock<IUnitOfWork> mock, User returnedUser)
        {
            mock.Setup(x => x.UserRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(returnedUser);
        }

        private void SetupUpdate(Mock<IUnitOfWork> mock)
        {
            mock.Setup(x => x.UserRepository.Update(It.IsAny<User>()));
            mock.Setup(x => x.CommitAsync());
        }

        private void VerifyCreate(Mock<IUnitOfWork> mock, Times methodInvocations)
        {
            VerifyFind(mock);
            mockUnitOfWork.Verify(x => x.UserRepository.Create(fixture.EmptyUser), methodInvocations);
            mockUnitOfWork.Verify(x => x.CommitAsync(), methodInvocations);
        }

        private void VerifyUpdate(Mock<IUnitOfWork> mock, User user, bool verifyFind = true)
        {
            if (verifyFind)
            {
                VerifyFind(mock);
            }

            mock.Verify(x => x.UserRepository.Update(user), Times.Once);
            mock.Verify(x => x.CommitAsync(), Times.Once);
        }

        private void VerifyFind(Mock<IUnitOfWork> mock)
        {
            mock.Verify(x => x.UserRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()),
                Times.Once);
        }
    }
}