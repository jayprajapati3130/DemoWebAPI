using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseModel
{
    public class EmployeeDTO
    {
        public int E_Id { get; set; }

        public string E_Name { get; set; } = null!;

        public int? E_Salary { get; set; }
    }
}
