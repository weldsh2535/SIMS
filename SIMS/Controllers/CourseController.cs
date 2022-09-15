using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly DataContext _dataContext;
        public CourseController(DataContext dataContext)
        {
            _dataContext = dataContext;
         }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.Courses.ToArrayAsync());
        }
        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get(int courseId)
        {
            return Json(await _dataContext.Courses.SingleOrDefaultAsync(c => c.cousreID == courseId));
        }
        [HttpPost]
        public async Task<IActionResult> Post(Course course)
        {
            _dataContext.Courses.Add(course);
            await _dataContext.SaveChangesAsync();
            return Json("Add success!!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Course course)
        {
            _dataContext.Courses.Update(course);
            await _dataContext.SaveChangesAsync();
            return Json("Updated!!");
        }
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> Delete(int courseId)
        {
          var dataOfCourse =  _dataContext.Courses.SingleOrDefault(c => c.cousreID == courseId);
            _dataContext.Remove(dataOfCourse);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
