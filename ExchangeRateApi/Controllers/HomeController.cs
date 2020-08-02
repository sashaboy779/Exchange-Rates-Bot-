using System.Threading.Tasks;
using System.Web.Mvc;
using ExchangeRateApi.DataAccess.UnitOfWork;

namespace ExchangeRateApi.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET
        public async Task<int> Index()
        {
            var users = await unitOfWork.UserRepository.GetAllAsync();
            return 0;
        }
    }
}