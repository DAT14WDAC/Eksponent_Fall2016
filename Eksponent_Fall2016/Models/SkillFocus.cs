using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eksponent_Fall2016.Models
{
    public class SkillFocus
    {
        public int FocusId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Skill> ISkill { get; set; }

    }
}