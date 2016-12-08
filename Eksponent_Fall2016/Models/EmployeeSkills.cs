using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class EmployeeSkills
    {
        public int EmployeeSkillsId { get; set; }
        public int SkillId { get; set; }
        public int EmployeeId { get; set; }
        public int LevelId { get; set; }

        public virtual ICollection<Skill> ISkills { get; set; }
        public virtual ICollection<Employee> IEmployees { get; set; }
    }
}