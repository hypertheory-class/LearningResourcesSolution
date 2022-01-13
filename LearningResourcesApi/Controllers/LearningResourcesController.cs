using LearningResourcesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningResourcesApi.Controllers;

public class LearningResourcesController : ControllerBase
{

    [HttpGet("learning-resources/{id:int}")]
    public ActionResult<GetLearningResourceResponse> GetALearningResource(int id)
    {
        if (id % 2 != 0) return NotFound();
        // do we have a resource that matches id in the database?
        // if yes - send it to them
        var response = new GetLearningResourceResponse
        {
            Id = id,
            Title = "Web Apis",
            Description = "Build yerself an API",
            Link = "http://blah.com"
        };
        return Ok(response);
        // if no - send a 404.
    }

    [HttpGet("/learning-resources")]
    public ActionResult<GetLearningResourceCollectionResponse> GetAll()
    {
        var response = new GetLearningResourceCollectionResponse
        {
            Data = new List<LearningResourceSummaryItem>
            {
                new LearningResourceSummaryItem { Id = 1, Title = "Git Stuff"},
                new LearningResourceSummaryItem { Id = 2, Title = "Go Programming"}
            }
        };
        return Ok(response);
    }

    
}
