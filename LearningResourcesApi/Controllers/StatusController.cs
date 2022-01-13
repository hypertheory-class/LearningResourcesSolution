using LearningResourcesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningResourcesApi.Controllers;

public class StatusController : ControllerBase
{

    private readonly ISystemTime _systemTime;

    public StatusController(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }


    // GET /status
    [HttpGet("status")]
    public ActionResult LookupStatus()
    {
        var response = new StatusResponse()
        {
            Message = "The server is completely operational, captain!",
            LastChecked = _systemTime.GetCurrent(),
        };
        return Ok(response);
    }

    // GET /employees/19
    // Route Parameters
    [HttpGet("employees/{employeeId:int}")]
    public ActionResult GetEmployeeById(int employeeId)
    {
        return Ok("That is employee " + employeeId);
    }

    [HttpGet("employees")]
    public ActionResult GetAllEmployees([FromQuery] string dept = "All")
    {
        return Ok($"Here are the employees filtered by {dept}");
    }

    [HttpPost("employees")]
    public ActionResult HireEmployee([FromBody] EmployeRequest employee)
    {
        return Ok($"I see you want to hire {employee.FirstName} as a {employee.Dept} for {employee.Salary:c}");
    }
}


public class StatusResponse
{
    public string Message { get; set; } = "";
    public DateTime LastChecked { get; set; }
}


public class EmployeRequest
{
    public string  FirstName { get; set; }
    public string  LastName { get; set; }
    public string  Dept { get; set; }
    public decimal Salary { get; set; }
}

