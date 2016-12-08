using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Skillname { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Employee> IEmployee { get; set; }
        public virtual ICollection<EmployeeSkill> IEmployeeSkill { get; set; }
    }
}