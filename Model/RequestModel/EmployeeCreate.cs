using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModel
{
    /*public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
    {
        public EmployeeCreateValidator()
        {
            RuleFor(p => p.E_Name).NotEmpty().WithMessage("Enter Your Name");
        }
    }*/

    public class EmployeeCreate
    {
        public string E_Name { get; set; } = null!;

        public int? E_Salary { get; set; }
    }
}
