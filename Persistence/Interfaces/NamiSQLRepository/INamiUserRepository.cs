using Nami.DXP.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nami.DXP.Persistence
{
    public interface INamiUserRepository
    {
        Task<FacelookMainBak102720> GetUserAsync(string id);
        Task<List<FacelookMainBak102720>> GetAllUsersAsync();
    }
}
