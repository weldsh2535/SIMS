using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeAssignmentController : Controller
    {
        private readonly DataContext _dataContext;
        public OfficeAssignmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.OfficeAssignments.ToArrayAsync());
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
