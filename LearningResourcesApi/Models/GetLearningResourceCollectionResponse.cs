using System.ComponentModel.DataAnnotations;

namespace LearningResourcesApi.Models;

public class GetLearningResourceCollectionResponse
{
    public List<LearningResourceSummaryItem> Data { get; set; } = new List<LearningResourceSummaryItem>();
}


public class LearningResourceSummaryItem
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = "";
}


