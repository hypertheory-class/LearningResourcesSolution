using System.ComponentModel.DataAnnotations;

namespace LearningResourcesApi.Models;

public class GetLearningResourceResponse
{

    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    [Required]
    public string Link { get; set; } = String.Empty;
}
