using AutoMapper;
using LearningResourcesApi.Data;
using LearningResourcesApi.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
namespace LearningResourcesApi.Controllers;

public class LearningResourcesController : ControllerBase
{
    private readonly LearningResourcesDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public LearningResourcesController(LearningResourcesDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    [HttpGet("learning-resources/{id:int}")]
    public ActionResult<GetLearningResourceResponse> GetALearningResource(int id)
    {
        var resource = _context.LearningResources
            .Where(r => r.Id == id && r.Removed == false)
            .ProjectTo<GetLearningResourceResponse>(_config)
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
           .ProjectTo<LearningResourceSummaryItem>(_config)
            .ToList();

        var response = new GetLearningResourceCollectionResponse
        {
            Data = data
        };
        return Ok(response);

    }

    
}
