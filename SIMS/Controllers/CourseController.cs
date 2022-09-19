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
        private List<Course> _course;
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
        [HttpGet("searchElement/{txtSearch}")]
        public JsonResult SearchByAnyType(string txtSearch)
        {
            List<Course> courseList = _dataContext.Courses.ToList();
            try
            {
                if (!string.IsNullOrEmpty(txtSearch))
                {
                  _course =   courseList.Where(q => (q.Credits +  q.Title )
                        .ToLower()
                        .Contains(txtSearch.ToLower()))
                        .ToList();
                    if(_course.Count > 0)
                    {
                        foreach (var course in _course)
                        {
                            Console.WriteLine($"Course Title  {course.Title}  Credits {course.Credits}");
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(_course);
        }
        [HttpGet("allList")]
        public JsonResult GetAllList()
        {
            List<Course> courseList = _dataContext.Courses.ToList();
            List<Department> departmentList = _dataContext.Departments.ToList();
            List<Instructor> instructorList = _dataContext.Instructors.ToList();
            var courses = from cou in courseList
                          join dep in departmentList on cou.DepartmetID equals dep.DepartmnetID
                          join ins in instructorList on dep.InstructorID equals ins.Id
                          select new
                          {
                              courseId = cou.cousreID,
                              Title = cou.Title,
                              Credits = cou.Credits,
                              DepartmentName = dep.Name,
                              Buget = dep.Buget,
                              StatrDate = dep.StartDate,
                              InstructorFullName = ins.FullName,
                              HireDate = ins.hireDate
                          };
            return Json(courses);
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
