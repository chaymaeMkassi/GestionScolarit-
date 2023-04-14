using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionScolarité.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        
    }
}