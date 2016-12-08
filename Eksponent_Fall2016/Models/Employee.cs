using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Profileimage { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Skill> ISkill { get; set; }
        public virtual ICollection<EmployeeSkill> IEmployeeSkill { get; set; }
    }
}