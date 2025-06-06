using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers
{
    // this url is: https://localhost:portnumber/api/students
    // the reason we know /students is because of the name of the controller is StudentsController
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = new List<string>
                {
                    "William",
                    "Nam",
                    "Craig",
                    "John",
                    "Sarah",
                    "Emily",
                    "Michael"
                };
            return Ok(students);
        }
        //GET: https://localhost:portnumber/api/students/1
        //[HttpGet("{id}")]
        //public IActionResult GetStudentById(int id)
        //{
        //    var students = new List<string>
        //        {
        //            "William",
        //            "Nam",
        //            "Craig",
        //            "John",
        //            "Sarah",
        //            "Emily",
        //            "Michael"
        //        };
        //    if (id < 0 || id >= students.Count)
        //    {
        //        return NotFound("Student not found.");
        //    }
        //    return Ok(students[id]);
        //}
    }
}
