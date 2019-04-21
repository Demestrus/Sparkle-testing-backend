using Microsoft.AspNetCore.Mvc;
using SparkleTesting.Application.Services;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("[controller]")]
    public class AssignController : Controller
    {
        private readonly TestsService _service;

        public AssignController(TestsService service)
        {
            _service = service;
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            var tests = _service.GetTests();
            return View(tests);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddUser(int testId, string assigne)
        {
            await _service.AssignToTest(testId, assigne);
            return RedirectToAction("Index");
        }
    }
}