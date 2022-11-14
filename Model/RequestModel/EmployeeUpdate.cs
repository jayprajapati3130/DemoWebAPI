using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModel
{
    public class EmployeeUpdate
    {
        //public int EmployeeId { get; set; }
        public string E_Name { get; set; } = null!;

        public int? E_Salary { get; set; }
    }
}
