using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeeTech.Models
{
    public class Department
    {
        [Key]
        public int DepartmnetID { get; set; }
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        public decimal Buget { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }
        public int? InstructorID { get; set; }
        public Instructor Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}