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

    [HttpPost("learning-resources")]
    public ActionResult AddALearningResource([FromBody] PostLearningResourceRequest request)
    {
        // Validate the incoming data.
        if(!ModelState.IsValid)
        {
        // If it is bad, send a 400 Response, optionally give them some clue as to what they did wrong.
            return BadRequest(ModelState);
        }
        // If It is Good:
        // -- Add a new LearningResource to the Database (we have a PostLearningResourceRequest
        //    Map it to a LearningResource
        var learningResource = _mapper.Map<LearningResource>(request);
        //    Add it to the DbSet
        _context.LearningResources.Add(learningResource);
        //    Save the Changes
        _context.SaveChanges();

        // Response:
        // 201 - "Created"
        // You send them the URL of the new resource. 
        //   -- adding a Location header to the response (e.g. Location: http://localhost:1337/learning-resources/13)
        //   -- Send them a copy of EXACTLY what they'd get if they went to the location URL.
        var response = _mapper.Map<GetLearningResourceResponse>(learningResource);
        return CreatedAtRoute("learningresources-getalearningresource", new { id = response.Id }, response);

    }


    [HttpGet("learning-resources/{id:int}", Name ="learningresources-getalearningresource")]
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
