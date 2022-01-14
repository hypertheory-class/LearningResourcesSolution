using AutoMapper;
using LearningResourcesApi.Data;
using LearningResourcesApi.Models;

namespace LearningResourcesApi.Profiles;

public class LearningResourcesProfile : Profile
{
    public LearningResourcesProfile()
    {
        CreateMap<LearningResource, GetLearningResourceResponse>();
        CreateMap<LearningResource, LearningResourceSummaryItem>();
    }
}
