using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers
{
    [Route(template:"api/[controller]")]            // Route
    [ApiController]
    public class DemoController : ControllerBase
    {
        // internal method - not accessible by the outside world
        public void m()
        {
        }

        // HTTP GET  https://localhost:xxxx/api/Demo/
        // HTTP GET  https://localhost:xxxx/api/Demo/GetMessage
        [HttpGet("GetMessage")]
        public string GetMessage()
        {
            return "Hello world from GetMessage()";
        }

        [HttpGet(template: "GetData")]
        public IActionResult GetData()
        {
            // return Ok();            // return VOID with Response Code 200 "Ok"
            return new OkObjectResult("hello world from GetData()"); // Response Code 200 OK with "hello world"
        }


        // Demonstrates Content Negotiation
        [HttpGet(template:"GetInfo")]
        public IActionResult GetInfo()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "First Employee" },
                new Employee() { Id = 2, Name = "Second Employee" },
                new Employee() { Id = 3, Name = "Third Employee" },
                new Employee() { Id = 4, Name = "Fourth Employee" },
            };
            return new OkObjectResult(employees);
        }


        // Enforce that the response is always in XML
        [HttpGet(template: "GetInfoInXml")]
        [Produces(System.Net.Mime.MediaTypeNames.Application.Xml)]      // "application/xml"
        public IActionResult GetInfoInXml()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "First Employee" },
                new Employee() { Id = 2, Name = "Second Employee" },
                new Employee() { Id = 3, Name = "Third Employee" },
                new Employee() { Id = 4, Name = "Fourth Employee" },
            };
            return new OkObjectResult(employees);
        }

    }


    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

}
