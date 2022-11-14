using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
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

//------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Display Perticular Employee
        /// </summary>
        /// <param name="eId">Enter Id For Find Record</param>
        /// <returns></returns>
        [HttpGet("displaySingle{eId}")]
        public async Task<EmployeeDTO> GetEmployee([FromRoute]int eId)
        {
            var Find = context.Employees.Where(s => s.EmployeeId == eId).FirstOrDefault<Employee>();
            //Fetching details of employee
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                E_Id = Find.EmployeeId,
                E_Name = Find.EmployeeName,
                E_Salary = Find.Salary

            };
            return employeeDTO;
        }
//------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Create New Student
        /// </summary>
        /// <param name="employeeCreate">Enter Details about New Employees</param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<EmployeeDTO> CreateEmployee(EmployeeCreate employeeCreate)
        {
            #region For Create New Employee and save in DB
            Employee employee = new Employee()
            {
                EmployeeName = employeeCreate.E_Name,
                Salary = employeeCreate.E_Salary
            };
            this.context.Employees.Add(employee);
            await context.SaveChangesAsync();
            #endregion

            #region For Display Record from DB
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                E_Id = employee.EmployeeId,
                E_Name = employee.EmployeeName,
                E_Salary = employee.Salary
            };
            #endregion

            return employeeDTO;
 
        }
 //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update employee Details
        /// </summary>
        ///<param name="E_Id">Enter Id which You Want to Update</param>
        /// <param name="employeeUpdate">Enter Your New Record</param>
        /// <returns></returns>
        [HttpPut("Update{E_Id}")]
        public async Task<EmployeeDTO> UpdateEmployee([FromRoute]int E_Id,EmployeeUpdate employeeUpdate)
        {
            #region For Update Employee Detail in DB
            //var Edit = context.Employees.Where(s => s.EmployeeId == employeeUpdate.EmployeeId).FirstOrDefault<Employee>();
            var Edit = context.Employees.Where(s => s.EmployeeId == E_Id).FirstOrDefault<Employee>();
            if (Edit != null)
            {
                Edit.EmployeeName = employeeUpdate.E_Name;

                Edit.Salary = employeeUpdate.E_Salary;
                await context.SaveChangesAsync();
            }
            #endregion

            #region For Display Record From DB
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                E_Id = Edit.EmployeeId,
                E_Name = Edit.EmployeeName,
                E_Salary = Edit.Salary
            };
            #endregion

            return employeeDTO;

        }
//-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Remove Employee 
        /// </summary>
        /// <param name="eId">Enter Id Which You Want to Delete</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<String> DeleteEmployee(int eId)
        {
            var delet = await context.Employees.FindAsync(eId);

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
