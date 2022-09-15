using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : Controller
    {
        private readonly DataContext _dataContext;
        public InstructorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.Instructors.ToArrayAsync());
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
