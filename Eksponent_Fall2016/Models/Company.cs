using System.Collections.Generic;

namespace Eksponent_Fall2016.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyLogo { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Employee> IEmployee { get; set; }
        public virtual ICollection<Skill> ISkill { get; set; }

    }
}