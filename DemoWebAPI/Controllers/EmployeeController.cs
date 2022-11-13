using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using Model.DBModel;
using Model.RequestModel;
using Model.ResponseModel;

namespace DemoWebAPI.Controllers
{
    /// <summary>
    /// employees Controller
    /// </summary>
    [ApiController]
    [Route("Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly DemoApiContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EmployeeController(DemoApiContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// Get All Employees 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeDTO>> GetAllEmployee()
        {
            List<EmployeeDTO> employees = await context.Employees.Select(s => new EmployeeDTO()
            { 
                E_Id = s.EmployeeId,
                E_Name = s.EmployeeName,
                E_Salary = s.Salary
            }).ToListAsync();

            return employees; 
        }


        /// <summary>
        /// Create New Student
        /// </summary>
        /// <param name="employeeCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<EmployeeDTO> CreateEmployee(EmployeeCreate employeeCreate)
        {
            Employee employee = new Employee()
            {
                EmployeeName = employeeCreate.E_Name,
                Salary = employeeCreate.E_Salary
            };
            this.context.Employees.Add(employee);
            await context.SaveChangesAsync();

            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                E_Id = employee.EmployeeId,
                E_Name = employee.EmployeeName,
                E_Salary = employee.Salary
            };

            return employeeDTO;
            
            
        }
        /// <summary>
        /// Update employee Details
        /// </summary>
        /// <param name="employeeUpdate"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<EmployeeDTO> UpdateEmployee(EmployeeUpdate employeeUpdate)
        {
            var Edit = context.Employees.Where(s => s.EmployeeId == employeeUpdate.EmployeeId).FirstOrDefault<Employee>();

            if (Edit != null)
            {
                Edit.EmployeeName = employeeUpdate.E_Name;

                Edit.Salary = employeeUpdate.E_Salary;
                await context.SaveChangesAsync();
            }
           
           EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                E_Id = Edit.EmployeeId,
                E_Name = Edit.EmployeeName,
                E_Salary = Edit.Salary
            };

            return employeeDTO;

        }


        /// <summary>
        /// Remove Employee 
        /// </summary>
        /// <param name="Eid"></param>
        /// <returns></returns>
        [HttpDelete("Delete Employee")]
        public async Task<String> DeleteEmployee(int Eid)
        {
            var delet = await context.Employees.FindAsync(Eid);

            if (delet == null)
            {
                return "not found";
            }

            context.Employees.Remove(delet);
            await context.SaveChangesAsync();

            return "Employee removed";
        }

    }
}
