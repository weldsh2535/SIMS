using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

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
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Json(await _dataContext.Instructors.SingleOrDefaultAsync(c=>c.Id ==Id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(Instructor instructor)
        {
             _dataContext.Instructors.Add(instructor);
            await _dataContext.SaveChangesAsync();
            return Json("Add success");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Instructor instructor)
        {
            _dataContext.Instructors.Update(instructor);
            await _dataContext.SaveChangesAsync();
            return Json("Updated!!");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var data =  _dataContext.Instructors.SingleOrDefault(x => x.Id == Id);
            _dataContext.Instructors.Remove(data);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted!!");
        }
    }
}
