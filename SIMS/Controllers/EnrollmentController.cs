using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : Controller
    {
        private readonly DataContext _dataContext;
        public EnrollmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.Enrollments.ToArrayAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Json(await _dataContext.Enrollments.SingleOrDefaultAsync(c=>c.CourseID == Id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(Enrollment enrollment)
        {
            _dataContext.Enrollments.Add(enrollment);
            await _dataContext.SaveChangesAsync();
            return Json("Add Success!!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Enrollment enrollment)
        {
            _dataContext.Enrollments.Update(enrollment);
            await _dataContext.SaveChangesAsync();
            return Json("Updated!!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           var data =  _dataContext.Enrollments.SingleOrDefault(c => c.EnrollmentID == id);
            _dataContext?.Enrollments.Remove(data);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted!!");
        }
       
    }
}
