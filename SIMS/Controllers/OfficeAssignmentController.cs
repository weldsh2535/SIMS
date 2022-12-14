using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeAssignmentController : Controller
    {
        private readonly DataContext _dataContext;
        private List<OfficeAssignment> _officeAssignments;
        public OfficeAssignmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _dataContext.OfficeAssignments.ToArrayAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            return Json(await _dataContext.OfficeAssignments.SingleOrDefaultAsync(c => c.InstructorID == Id));
        }
        [HttpGet("allList")]
        public  JsonResult GetAll()
        {
            List<OfficeAssignment> officeAssignmentList =  _dataContext.OfficeAssignments.ToList();
            List<Instructor> instructorList =  _dataContext.Instructors.ToList();

            var instructor = from off in officeAssignmentList
                             join ins in instructorList on off.InstructorID equals ins.Id
                             select new
                             {
                                 InstructorId = ins.Id,
                                 InstructorName = ins.FullName,
                                 hireDate = ins.hireDate,
                                 Location = off.Location
                             };

            return Json(instructor);
        }
        [HttpGet("SearchText/{txtSearch}")]
        public JsonResult Search(string txtSearch)
        {
            List<OfficeAssignment> officeAssignmentList = _dataContext.OfficeAssignments.ToList();

            try
            {
                if (!string.IsNullOrEmpty(txtSearch))
                {
                    _officeAssignments = officeAssignmentList.Where(c=>(c.Location)
                    .ToLower()
                    .Contains(txtSearch.ToLower()))
                    .ToList();
                    if(_officeAssignments.Count > 0)
                    {
                        foreach (var instructor in _officeAssignments)
                        {
                            Console.WriteLine($"InstructorId   {instructor.InstructorID}  Location  {instructor.Location}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(_officeAssignments);
        }
        [HttpPost]
        public async Task<IActionResult> Post(OfficeAssignment officeAssignment)
        {
            _dataContext.OfficeAssignments.Add(officeAssignment);
            await _dataContext.SaveChangesAsync();
            return Json("Add Success!!!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(OfficeAssignment officeAssignment)
        {
            _dataContext.OfficeAssignments.Update(officeAssignment);
            await _dataContext.SaveChangesAsync();
            return Json("Updated!!");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var data =  _dataContext.OfficeAssignments.SingleOrDefault(c => c.InstructorID == Id);
            _dataContext.OfficeAssignments.Remove(data);
            await _dataContext.SaveChangesAsync();
            return Json("Deleted!!");
        }
    }
}
