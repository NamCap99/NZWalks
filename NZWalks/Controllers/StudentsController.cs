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
            // this is a hardcoded list of students
            string[] studentNames = new string[]
            {
                "John Doe",
                "Jane Smith",
                "Sam Brown",
                "Lisa White"
            };
            return Ok(studentNames);
        }
    }
}
