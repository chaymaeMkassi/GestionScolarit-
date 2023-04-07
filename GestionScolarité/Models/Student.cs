using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionScolarité.Models
{
    public enum StudentStatus
    {
        Rejected,
        Submitted
    }
    public class Student : User
    {
        [Required]
        public StudentStatus Status { get; set; }
        [DisplayName(@"Group")]
        [ForeignKey("Section")]
        public int? SectionId { get; set; } 
        public Section Section { get; set; }
    }

}