using Microsoft.EntityFrameworkCore;
using SeeTech.Models;

namespace SeeTech.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<CourseAssignment>().HasKey(c => new { c.CourseID, c.InstructorID });
        }
    }
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student { Fname = "Carson",   Lname = "Alexander",
                    enrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { Fname = "Meredith", Lname = "Alonso",
                    enrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Fname = "Arturo",   Lname = "Anand",
                    enrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Fname = "Gytis",    Lname = "Barzdukas",
                    enrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Fname = "Yan",      Lname = "Li",
                    enrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Fname = "Peggy",    Lname = "Justice",
                    enrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { Fname = "Laura",    Lname = "Norman",
                    enrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Fname = "Nino",     Lname = "Olivetto",
                    enrollmentDate = DateTime.Parse("2005-09-01") }
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { firstName = "Kim",     lastName = "Abercrombie",
                    hireDate = DateTime.Parse("1995-03-11") },
                new Instructor { firstName = "Fadi",    lastName = "Fakhouri",
                    hireDate = DateTime.Parse("2002-07-06") },
                new Instructor { firstName = "Roger",   lastName = "Harui",
                    hireDate = DateTime.Parse("1998-07-01") },
                new Instructor { firstName = "Candace", lastName = "Kapoor",
                    hireDate = DateTime.Parse("2001-01-15") },
                new Instructor { firstName = "Roger",   lastName = "Zheng",
                    hireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Buget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.lastName == "Abercrombie").Id },
                new Department { Name = "Mathematics", Buget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.lastName == "Fakhouri").Id },
                new Department { Name = "Engineering", Buget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.lastName == "Harui").Id },
                new Department { Name = "Economics",   Buget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.lastName == "Kapoor").Id }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {cousreID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmetID = departments.Single( s => s.Name == "Engineering").DepartmnetID
                },
                new Course {cousreID = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmetID = departments.Single( s => s.Name == "Economics").DepartmnetID
                },
                new Course {cousreID = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmetID = departments.Single( s => s.Name == "Economics").DepartmnetID
                },
                new Course {cousreID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmetID = departments.Single( s => s.Name == "Mathematics").DepartmnetID
                },
                new Course {cousreID = 3141, Title = "Trigonometry",   Credits = 4,
                    DepartmetID = departments.Single( s => s.Name == "Mathematics").DepartmnetID
                },
                new Course {cousreID = 2021, Title = "Composition",    Credits = 3,
                    DepartmetID = departments.Single( s => s.Name == "English").DepartmnetID
                },
                new Course {cousreID = 2042, Title = "Literature",     Credits = 4,
                    DepartmetID = departments.Single( s => s.Name == "English").DepartmnetID
                },
            };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.lastName == "Fakhouri").Id,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.lastName == "Harui").Id,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.lastName == "Kapoor").Id,
                    Location = "Thompson 304" },
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Kapoor").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Harui").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Zheng").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Zheng").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Calculus" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Fakhouri").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Harui").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Composition" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Abercrombie").Id
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Literature" ).cousreID,
                    InstructorID = instructors.Single(i => i.lastName == "Abercrombie").Id
                    },
            };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Alexander").Id,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).cousreID,
                    Grade = Grade.A
                },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Alexander").Id,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).cousreID,
                    Grade = Grade.C
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Alexander").Id,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.Lname == "Alonso").Id,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.Lname == "Alonso").Id,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Alonso").Id,
                    CourseID = courses.Single(c => c.Title == "Composition" ).cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Anand").Id,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).cousreID
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Anand").Id,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").cousreID,
                    Grade = Grade.B
                    },
                new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Barzdukas").Id,
                    CourseID = courses.Single(c => c.Title == "Chemistry").cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Li").Id,
                    CourseID = courses.Single(c => c.Title == "Composition").cousreID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.Lname == "Justice").Id,
                    CourseID = courses.Single(c => c.Title == "Literature").cousreID,
                    Grade = Grade.B
                    }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.Id == e.StudentID &&
                            s.Course.cousreID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
