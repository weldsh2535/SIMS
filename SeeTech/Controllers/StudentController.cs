using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SeeTech.Data;
using SeeTech.Models;
using System.Data.Common;
using System.Data; 

namespace SeeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  List<Student> Get()
        {
            var query =  from student in _context.Students
                         from enrollment in _context.Enrollments
                         from course in _context.Courses
       where student.Id == enrollment.StudentID && enrollment.CourseID==course.cousreID
       select new
       {
           StudentId = student.Id,
           FirstName = student.Fname,
           LastName = student.Lname,
           EnrollmentDate = student.enrollmentDate,
           CourseTitle = course.Title,
           CourseCredits = course.Credits
       };
            foreach (var smallOrder in query)
            {
                Console.WriteLine("Stduent ID: {0} StudentFName: {1}, {2} Order ID: {3} Total Due: ${4} ",
                    smallOrder.StudentId,
                    smallOrder.FirstName, smallOrder.LastName, smallOrder.EnrollmentDate,
                    smallOrder.CourseTitle, smallOrder.CourseCredits);
            }
            var stud = _context.Students.FromSqlRaw("SELECT * FROM Student").ToList();
            return stud;
        }
        [HttpGet("{Id}")]
        public JsonResult GetById(int Id)
        {
            Student student;
            try
            {
                student = _context.Students.SingleOrDefault(c => c.Id == Id);
             }
           catch(Exception ex)
            {
                throw;
            }
            return new JsonResult(student);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            _context.Add(student);
           await  _context.SaveChangesAsync();
            return new JsonResult("Add Seccuess");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return new JsonResult("Update success");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Student student = _context.Students.SingleOrDefault(c => c.Id == id);
                _context.Remove(student);
            }
            catch(Exception e) {
                Console.WriteLine("Inner Exception: " + e.Message);
                return BadRequest();
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
            return Ok("Deleted Success");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

    }
}
