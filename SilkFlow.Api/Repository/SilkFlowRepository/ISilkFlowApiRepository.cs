using Microsoft.AspNetCore.Mvc;
using SilkFlow.Api.Model;

namespace SilkFlow.Api.Repository.SilkFlowRepository
{
    public interface ISilkFlowApiRepository
    {
        public Task<ResponseModel> GetAllIdeas();

        public Task<ResponseModel> GetIdeasByUserId(int UserId);
    }
}
