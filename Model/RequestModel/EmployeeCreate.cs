using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModel
{
    public class EmployeeCreate
    {
        public string E_Name { get; set; } = null!;

        public int? E_Salary { get; set; }
    }
}
