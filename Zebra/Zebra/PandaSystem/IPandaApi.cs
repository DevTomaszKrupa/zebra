using System.Threading.Tasks;
using RestEase;
using Zebra.PandaSystem.Models;

namespace Zebra.PandaSystem
{
    public interface IPandaApi
    {
        [Post("product")]
        Task BulkUpdate([Body] BulkUpdateRequest request);
    }
}
