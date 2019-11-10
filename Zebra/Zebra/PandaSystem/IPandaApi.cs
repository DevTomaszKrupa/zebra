using System.Threading.Tasks;
using Pigeon.PandaSystem.Models;
using RestEase;

namespace Zebra.PandaSystem
{
    public interface IPandaApi
    {
        [Post("product")]
        Task BulkUpdate([Body] BulkUpdateRequest request);
    }
}
