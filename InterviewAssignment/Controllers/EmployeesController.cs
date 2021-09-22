using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewAssignment.Controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, LastName = "Jackson", FirstName = "Alberta", Department = "Finance", HireDate = new DateTime(2007, 6, 5)},
            new Employee { Id = 2, LastName = "Bennett", FirstName = "Alicia", Department = "Human Resources", HireDate = new DateTime(2001, 4, 15)},
            new Employee { Id = 3, LastName = "Avent", FirstName = "Donna", Department = "Revenue", HireDate = new DateTime(2009, 4, 20) },
            new Employee { Id = 4, LastName = "Holder", FirstName = "Duane", Department = "Human Services", HireDate = new DateTime(2020,8, 15) }
        };


        [HttpGet]
        public IEnumerable<EmployeeSummary> GetEmployees()
        {
            return _employees
                .Select(e => new EmployeeSummary { FirstName = e.FirstName, LastName = e.LastName, Department = e.Department })
                .OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employees.SingleOrDefault(e => e.Id == id);

            if (employee is null)
            {
                return new NotFoundResult();
            }

            return employee;
        }
    }

    public record Employee
    {
        public int Id { get; init; }
        public string LastName { get; init; }
        public string FirstName { get; init; }
        public DateTime HireDate { get; init; }
        public string Department { get; init; }
    }

    public record EmployeeSummary
    {       
        public string LastName { get; init; }
        public string FirstName { get; init; }       
        public string Department { get; init; }
    }
}
