namespace LearningResourcesApi.Data;

public class LearningResource
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string Link { get; set; } = String.Empty;
    public bool Removed { get; set; }
}
