using Microsoft.AspNetCore.Mvc;
using Model;
//using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace DemoWebAPI.Controllers
{
    [ApiController]
    [Route("Student")]
    public class StudentController : ControllerBase
    {

        [HttpGet("ABC")]
        public List<StudentDTO> GetAllStudent()
        {
            List<StudentDTO> students = new List<StudentDTO>();

            students.Add(new StudentDTO()
            {
                StudentName = "jay",
                StudentId = 1
            });
            students.Add(new StudentDTO()
            {
                StudentName = "Om",
                StudentId = 2
            });

            return students;
        }


        /// <summary>
        /// create a new student
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateStudent")]
        public StudentDTO Post(StudentDTO studentDTO)
        {
            studentDTO.CollegeCode = 123;
            return studentDTO;
        }

        
        /// <summary>
        /// Get details about Student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}")]
        public StudentDTO GetStudent([FromQuery]int studentId)
        {
            return new StudentDTO();
        }


        /// <summary>
        /// for Uodate Student Details
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPut("{studentId}")]
        public StudentDTO UpdateStudent([FromQuery] int studentId,StudentDTO studentDTO) 
        {
            return new StudentDTO();
        }
    }
}
