using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nami.DXP.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nami.DXP.Persistence
{
    public class NamiUserRepository : DbQueryExecutor, INamiUserRepository
    {
        private readonly NAMI_PLANT_DATAContext _context;

        public NamiUserRepository(NAMI_PLANT_DATAContext context, ILogger<NamiUserRepository> logger): base(logger)
        {
            _context = context;
        }

        public async Task<List<FacelookMainBak102720>> GetAllUsersAsync()
        {
            return await ExecuteSafelyAsync(async () =>
            {
                return await _context.FacelookMainBak102720.ToListAsync();
            });
        }

        public async Task<FacelookMainBak102720> GetUserAsync(string empId)
        {
            return await ExecuteSafelyAsync(async () =>
            {
                return await _context.FacelookMainBak102720.FirstOrDefaultAsync(x => x.EmpId == empId);
            });
        }
    }
}
