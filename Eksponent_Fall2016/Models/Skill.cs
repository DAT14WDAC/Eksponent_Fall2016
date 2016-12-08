using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        public string Skillname { get; set; }
      
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<EmployeeSkill> IEmployeeSkill { get; set; }
    }
}