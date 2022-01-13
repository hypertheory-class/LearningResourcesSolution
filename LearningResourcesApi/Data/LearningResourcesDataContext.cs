using Microsoft.EntityFrameworkCore;

namespace LearningResourcesApi.Data;

public class LearningResourcesDataContext : DbContext
{
    public LearningResourcesDataContext(DbContextOptions<LearningResourcesDataContext> context) : base(context)
    {

    }

    public DbSet<LearningResource> LearningResources { get; set; }

}
