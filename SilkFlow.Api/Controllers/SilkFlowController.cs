using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SilkFlow.Api.Repository.SilkFlowRepository;
using SilkFlow.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace SilkFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SilkFlowController : ControllerBase
    {
        private readonly ISilkFlowApiRepository _SilkFlowRepository;
        public SilkFlowController(ISilkFlowApiRepository SilkFlowRepository)
        {
            _SilkFlowRepository = SilkFlowRepository;

        }


        [HttpGet(nameof(GetAllIdeas))]

        public async Task<ResponseModel> GetAllIdeas()
        {
            var res = await _SilkFlowRepository.GetAllIdeas();
            return res;
        }

        [HttpPost(nameof(GetIdeasByUserId))]
        public async Task<ResponseModel> GetIdeasByUserId(int UserId)
        {
            var res = await _SilkFlowRepository.GetIdeasByUserId(UserId);
            return res;
        }
    }
}
