using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_CMS.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeeGender { get; set; }

        public DateTime EmployeeBirthday { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
