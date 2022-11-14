using FluentValidation;
using Model.DBModel;
using Model.RequestModel;
using Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelValidator
{
    public class EmployeeValidator:AbstractValidator<EmployeeCreate>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.E_Name).NotEmpty().WithMessage("Please Enter Your Name {EmployeeName} is required");
            RuleFor(e => e.E_Salary).NotNull().WithMessage("salary required");
        }
    }
}
