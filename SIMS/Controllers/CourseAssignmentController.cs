using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAssignmentController : Controller
    {
        private readonly DataContext _dataContext;

        public CourseAssignmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.CourseAssignments.ToArrayAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Json(await _dataContext.CourseAssignments.SingleOrDefaultAsync(c=>c.InstructorID == Id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CourseAssignment courseAssignment)
        {
            _dataContext.CourseAssignments.Add(courseAssignment);
           await _dataContext.SaveChangesAsync();
            return Json("Add Success!!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseAssignment courseAssignment)
        {
            _dataContext.CourseAssignments.Update(courseAssignment);
            await _dataContext.SaveChangesAsync();
            return Json("Updated");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        { 
            CourseAssignment data =  _dataContext.CourseAssignments.SingleOrDefault(c=>c.InstructorID==Id);
            _dataContext.CourseAssignments.Remove(data);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted!!");
        }
    }
}
