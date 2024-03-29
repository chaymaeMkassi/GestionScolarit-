﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionScolarité.Models
{
    public class TeacherSection
    {
        public int Id { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}