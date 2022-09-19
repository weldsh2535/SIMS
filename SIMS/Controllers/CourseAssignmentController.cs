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
            List<CourseAssignment> courseAssignmentList = await _dataContext.CourseAssignments.ToListAsync();
            List<Instructor> instructorList = await _dataContext.Instructors.ToListAsync();
            List<Course> courseList = await _dataContext.Courses.ToListAsync();
            List<Department> departmentList = await _dataContext.Departments.ToListAsync();
            var instructor = from courseAss in courseAssignmentList
                             join ins in instructorList on courseAss.InstructorID equals ins.Id
                             join cou in courseList on courseAss.CourseID equals cou.cousreID
                             join dep in departmentList on ins.Id equals dep.DepartmnetID
                             select new
                             {
                                 CourseName = cou.Title,
                                 CourseCredit = cou.Credits,
                                 DepartmentId = cou.DepartmetID,
                                 InstructorName = ins.FullName,
                                 HireDate = ins.hireDate,
                                 DepartmentName = dep.Name,
                                 Buget = dep.Buget,
                                 StartDate = dep.StartDate
                             };
      /*      foreach(var ins in instructor)
            {
                Console.WriteLine($"Instructor Name ={ins.CourseName,-10} Hire Date {ins.HireDate,-10} CourseName {ins.CourseName,-10} CourseCredit{ins.CourseCredit}");
            }*/
            return Json(instructor);
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
