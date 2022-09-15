using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DataContext _dataContext;
        public DepartmentController(DataContext dataConetext)
        {
            _dataContext = dataConetext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.Departments.ToArrayAsync());
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
