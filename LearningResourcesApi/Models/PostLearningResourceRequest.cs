using System.ComponentModel.DataAnnotations;

namespace LearningResourcesApi.Models;

public class PostLearningResourceRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Required]
    [Url]
    public string Link { get; set; } = string.Empty;
}
