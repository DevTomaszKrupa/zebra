using System.Threading.Tasks;
using Flurl.Http;
using Zebra.PandaSystem.Models;

namespace Zebra.PandaSystem
{
    public interface IPandaSystemApi
    {
        Task BulkUpdate(BulkUpdateRequest request);
    }
    public class PandaSystemApi : IPandaSystemApi
    {
        private readonly string _pandaUrl;

        public PandaSystemApi()
        {
            _pandaUrl = "http://panda-api/api";
        }

        public async Task BulkUpdate(BulkUpdateRequest request)
        {
            var bulkUpdateUrl = _pandaUrl + "/product";
            await bulkUpdateUrl.PostJsonAsync(request);
        }
    }
}
