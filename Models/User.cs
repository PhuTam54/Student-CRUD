using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Student_CRUD.Models;

namespace Student_CRUD.Models
{
    public class User
    {
        [Key]
        public string Rollnumber { get; set; }

        [StringLength(255)]
        public string Group { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        [StringLength(255)]
        
        public string Comment { get; set; }
        public bool isPresent { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}