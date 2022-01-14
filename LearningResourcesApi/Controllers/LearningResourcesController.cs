using AutoMapper;
using LearningResourcesApi.Data;
using LearningResourcesApi.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult> AddALearningResourceAsync([FromBody] PostLearningResourceRequest request)
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
        await _context.SaveChangesAsync();

        // Response:
        // 201 - "Created"
        // You send them the URL of the new resource. 
        //   -- adding a Location header to the response (e.g. Location: http://localhost:1337/learning-resources/13)
        //   -- Send them a copy of EXACTLY what they'd get if they went to the location URL.
        var response = _mapper.Map<GetLearningResourceResponse>(learningResource);
        return CreatedAtRoute("learningresources-getalearningresource", new { id = response.Id }, response);

    }


    [HttpGet("learning-resources/{id:int}", Name ="learningresources-getalearningresource")]
    public async Task<ActionResult<GetLearningResourceResponse>> GetALearningResourceAsync(int id)
    {
        var resource = await _context.LearningResources
            .Where(r => r.Id == id && r.Removed == false)
            .ProjectTo<GetLearningResourceResponse>(_config)
            .SingleOrDefaultAsync();

        if(resource != null)
        {
            return Ok(resource);
        } else
        {
            return NotFound();
        }

    }

    [HttpGet("/learning-resources")]
    public async Task<ActionResult<GetLearningResourceCollectionResponse>> GetAllAsync()
    {
        var data = await _context.LearningResources.Where(r => r.Removed == false)
           .ProjectTo<LearningResourceSummaryItem>(_config)
            .ToListAsync();

        var response = new GetLearningResourceCollectionResponse
        {
            Data = data
        };
        return Ok(response);

    }

    
}
