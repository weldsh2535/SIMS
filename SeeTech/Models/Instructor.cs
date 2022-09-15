using System.ComponentModel.DataAnnotations;

namespace SeeTech.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Last Name")]
        [StringLength(50)]
        public string lastName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime hireDate { get; set; }
        public string FullName { 
            get { return lastName + "," + firstName; }
        }
        public ICollection<CourseAssignment> courseAssignments { get; set; }
        public OfficeAssignment officeAssignment { get; set; }
    }
}
