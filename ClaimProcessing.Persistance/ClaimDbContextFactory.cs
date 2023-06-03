using ClaimProcessing.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Persistance
{
    public class ClaimDbContextFactory : DesignTimeDbContextFactoryBase<ClaimProcessingDbContext>
    {
        
        protected override ClaimProcessingDbContext CreateNewInstance(DbContextOptions<ClaimProcessingDbContext> options)
        {
            return new ClaimProcessingDbContext(options);
        }
    }
}
