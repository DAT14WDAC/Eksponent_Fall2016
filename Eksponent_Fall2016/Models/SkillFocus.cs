﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class SkillFocus
    {
        [Key]
        public int SkillFocusId { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Startdate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Enddate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }

    }
}