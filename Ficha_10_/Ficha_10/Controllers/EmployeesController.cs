﻿using Ficha_10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha_10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        //private readonly Employees employees;

        private readonly IEmployees employees;

        /*public EmployeesController()
        {
            employees = JsonLoader.LoadEmploeesJson();
        }*/

        public EmployeesController(IEmployees employees)
        {
            this.employees = employees;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employees.EmployeesL;
            
        }

        
        // GET Id api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetId(int id)
        {
            Employee? emp = employees.EmployeesL.Find(e => e.UserId == id);
            if (emp == null)
            {
                return NotFound($"Id: {id} not Found.");
            }
            else
            {
                return Ok(emp);
            }
        }
        
        // POST api/<ValuesController>
        [HttpPost(Name = "PostEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee != null)
            {
                employees.EmployeesL.Add(employee);
                return CreatedAtRoute("GetById", new { id = employee.UserId }, employee);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var emp = employees.EmployeesL.Find(e => e.UserId == id);
            if (emp == null)
            {
                return NotFound(String.Format("Id: {0} not found!", id));
            }
            else
            {
                emp.JobTitle = employee.JobTitle;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.EmployeeCode = employee.EmployeeCode;
                emp.Region = employee.Region;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.EmailAddress = employee.EmailAddress;
                return Ok(emp);
            }
        }
        

        // DELETE api/<ValuesController>/5
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            int removed = employees.EmployeesL.RemoveAll(e => e.UserId == id);
            if (removed == 0)
            {
                return NotFound(string.Format("Id: {0} not found.", id));
            }
            else
            {
                return Ok(String.Format("Employee with ID: {0} was deleted.", id));
            }
        }
        
        //Get Region
        [HttpGet("region/{region}", Name = "GetByRegion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByRegion(string region)
        {
            Employee? emp = employees.EmployeesL.Find(e => e.Region == region);
            if (emp == null)
            {
                return NotFound($"Region: {region} not found!");
            }
            else
            {
                return Ok(emp);
            }
        }

        
        //Download
        [HttpGet("download", Name = "GetByDownload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByDownload()
        {
            string jsonEmp = JsonSerializer.Serialize<IEmployees>(employees);
            //namespace.class.function
            System.IO.File.WriteAllText("./JsonFiles/employees.json", jsonEmp);
            
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("./JsonFiles/employees.json");
                return File(bytes, "aplication/json", "employees.json");

            }
            catch(FileNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
        
    }
}
