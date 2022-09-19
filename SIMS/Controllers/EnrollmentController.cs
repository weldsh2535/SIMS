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
        [HttpGet("allList")]
        public JsonResult GetAllList()
        {
            List<Enrollment> enrollments = _dataContext.Enrollments.ToList();
            List<Course> courses = _dataContext.Courses.ToList();
            List<Student> students = _dataContext.Students.ToList();
            List<Department> departments = _dataContext.Departments.ToList();
            List<Instructor> instructors = _dataContext.Instructors.ToList();
           
            var enrollemts = from enr in enrollments
                             join cou in courses on enr.CourseID equals cou.cousreID
                             join stud in students on enr.StudentID equals stud.Id
                             join dep in departments on cou.DepartmetID equals dep.DepartmnetID
                             join ins in instructors on dep.InstructorID equals ins.Id
                             where ins.Id == 1 orderby dep.StartDate
                             select new
                             {
                                 FullName = stud.FullName,
                                 EnrollmentDate = stud.enrollmentDate,
                                 BirthDate = stud.BirthDate,
                                 Gender = stud.Gender,
                                 Age = (int.Parse(DateTime.Today.ToString("yyyyMMdd")) - int.Parse(stud.BirthDate.ToString("yyyyMMdd")))/10000,
                                 Grade = enr.Grade,
                                 courseTitle = cou.Title,
                                 credits = cou.Credits,
                                 DepartmentId = dep.DepartmnetID,
                                 DepartmentName = dep.Name,
                                 InstructorId = dep.InstructorID,
                                 InstructorName = ins.FullName,
                                 Buget = dep.Buget,
                                 StartDate = dep.StartDate,
                                
                             };
            Console.WriteLine(int.Parse(DateTime.Today.ToString("yyyyMMdd"))/10000);
            return Json(enrollemts);
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
