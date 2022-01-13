using LearningResourcesApi.Data;
using LearningResourcesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningResourcesApi.Controllers;

public class LearningResourcesController : ControllerBase
{
    private readonly LearningResourcesDataContext _context;

    public LearningResourcesController(LearningResourcesDataContext context)
    {
        _context = context;
    }

    [HttpGet("learning-resources/{id:int}")]
    public ActionResult<GetLearningResourceResponse> GetALearningResource(int id)
    {
        var resource = _context.LearningResources
            .Where(r => r.Id == id && r.Removed == false)
            .Select(r => new GetLearningResourceResponse
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description

            })
            .SingleOrDefault();

        if(resource != null)
        {
            return Ok(resource);
        } else
        {
            return NotFound();
        }

    }

    [HttpGet("/learning-resources")]
    public ActionResult<GetLearningResourceCollectionResponse> GetAll()
    {
        var data = _context.LearningResources.Where(r => r.Removed == false)
            .Select(r => new LearningResourceSummaryItem
            {
                Id = r.Id,
                Title = r.Title
            }).ToList();

        var response = new GetLearningResourceCollectionResponse
        {
            Data = data
        };
        return Ok(response);

    }

    
}
