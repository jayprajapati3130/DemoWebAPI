using System;
using System.Collections.Generic;

namespace Model.DBModel;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? Salary { get; set; }
}
