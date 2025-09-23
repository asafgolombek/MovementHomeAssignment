using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Registry;
using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Data> DataItems { get; set; }
}

namespace MovementHomeAssignment.InfrastructureLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ResiliencePipeline _resiliencePipeline;

        public DataRepository(ApplicationDbContext context, ResiliencePipelineRegistry<string> pipelineRegistry)
        {
            _context = context;
            _resiliencePipeline = pipelineRegistry.GetPipeline("db-retry-policy");
        }

        public async Task<Data?> GetByIdAsync(string id)
        {
            return await _resiliencePipeline.ExecuteAsync(async token =>
                await _context.DataItems.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, token));
        }

        public async Task<Data> CreateAsync(Data data)
        {
            await _context.DataItems.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Data?> UpdateAsync(Data data)
        {
            var existing = await _context.DataItems.FindAsync(data.Id);
            if (existing == null)
            {
                return null;
            }
            
            _context.Entry(existing).CurrentValues.SetValues(data);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}

