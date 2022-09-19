using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

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
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Json(await _dataContext.Departments.SingleOrDefaultAsync(c=>c.InstructorID == Id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Department department)
        {
            _dataContext.Departments.Add(department);
            await _dataContext.SaveChangesAsync();  
            return Json("Add Department!!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Department dep)
        {
            _dataContext.Departments.Update(dep);
           await _dataContext.SaveChangesAsync();
            return Json("Updated!!");
        }
        [HttpDelete("{DepId}")]
        public async Task<IActionResult> Delete(int DepId)
        {
            Department dep =  _dataContext.Departments.SingleOrDefault(c=>c.DepartmnetID==DepId);
            _dataContext.Departments.Remove(dep);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted!!");
        }
        
    }
}
