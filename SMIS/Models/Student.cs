using System.ComponentModel.DataAnnotations;

namespace SeeTech.Models
{
    public class Student
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Fname { get; set; }
        [StringLength(50)]
        public string Lname { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy-mm-dd" , ApplyFormatInEditMode =true)] 
        public DateTime enrollmentDate { get; set; }
        public ICollection<Enrollment> enrollments { get; set; }
    }
}
