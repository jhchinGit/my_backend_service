using Microsoft.EntityFrameworkCore;
using MyBackendService.Models.POCOs;

namespace MyBackendService.Services
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}