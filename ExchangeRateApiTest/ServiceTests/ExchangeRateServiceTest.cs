using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Exceptions;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services;
using ExchangeRateApi.Services.Interfaces;
using ExchangeRateApiTest.Configurations;
using ExchangeRateApiTest.Fixtures;
using Moq;
using Xunit;

namespace ExchangeRateApiTest.ServiceTests
{
    public class ExchangeRateServiceTest : IClassFixture<ExchangeRateFixture>, IClassFixture<UserFixture>
    {
        private readonly ExchangeRateFixture fixture;
        private readonly UserFixture userFixture;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IApiService> mockApiService;
        private readonly Mock<ObjectCache> mockCache;
        private readonly IExchangeRateService service;
        private readonly SetupLoadConfiguration setupLoadConfiguration;

        public ExchangeRateServiceTest(ExchangeRateFixture fixture, UserFixture userFixture)
        {
            this.fixture = fixture;
            this.userFixture = userFixture;

            mockUserService = new Mock<IUserService>();
            mockApiService = new Mock<IApiService>();
            mockCache = new Mock<ObjectCache>();

            service = new ExchangeRateService(mockUserService.Object, mockApiService.Object, mockCache.Object);
            setupLoadConfiguration = new SetupLoadConfiguration
            {
                MockApi = mockApiService,
                MockCache = mockCache
            };
        }

        [Fact]
        public async Task LoadExchangeRateAsync_CacheIsNotEmpty_ReturnResultFromCache()
        {
            setupLoadConfiguration.ExchangeRateReturnedByCache = fixture.ExchangeRateModel.ExchangeRate;
            SetupLoadExchangeRate(setupLoadConfiguration);

            var result = await service.LoadExchangeRateAsync(fixture.Date);

            Assert.Equal(result, fixture.ExchangeRateModel.ExchangeRate);
            VerifyLoadExchangeRate(Times.Never(), Times.Never());
        }

        [Fact]
        public async Task LoadExchangeRateAsync_ApiCallFailed_ThrowException()
        {
            var mockResponse = new Mock<HttpResponseMessage>(HttpStatusCode.InternalServerError);
            setupLoadConfiguration.MessageReturnedByApi = mockResponse.Object;
            SetupLoadExchangeRate(setupLoadConfiguration);

            await Assert.ThrowsAsync<ExchangeRateNotFoundException>(() => service.LoadExchangeRateAsync(fixture.Date));

            VerifyLoadExchangeRate(Times.Once(), Times.Never());
        }

        [Fact]
        public async Task FilterExchangeRatesAsync_BothFilteredAndNotFoundIsPresented_CorrectFiltering()
        {
            mockUserService.Setup(x => x.FindUserAsync(It.IsAny<int>())).ReturnsAsync(userFixture.UserWithCurrencies);

            var filtered = await service.FilterExchangeRatesAsync(userFixture.UserId, fixture.NotFilteredExchangeRates);

            mockUserService.Verify(x => x.FindUserAsync(userFixture.UserId), Times.Once);
            Assert.Collection(filtered.Filtered, item => Assert.Equal("EUR", item.Currency));
            Assert.Collection(filtered.NotFound, item => Assert.Equal(Currencies.USD, item.Currency));
        }

        private void SetupLoadExchangeRate(SetupLoadConfiguration config)
        {
            config.MockCache.SetupGet(x => x[It.IsAny<string>()]).Returns(config.ExchangeRateReturnedByCache);
            config.MockCache.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<object>(),
                It.IsAny<CacheItemPolicy>(), It.IsAny<string>()));

            config.MockApi.Setup(x => x.MakeApiCall(It.IsAny<string>())).Returns(config.
                 MessageReturnedByApi);
        }

        private void VerifyLoadExchangeRate(Times invocationApi, Times invocationSet)
        {
            mockCache.Verify(x => x[fixture.DateString], Times.Once);
            mockApiService.Verify(x => x.MakeApiCall(It.IsAny<string>()), invocationApi);
            mockCache.Verify(x => x.Set(fixture.DateString, fixture.ExchangeRateModel.ExchangeRate,
                It.IsAny<CacheItemPolicy>(), null), invocationSet);
        }
    }
}