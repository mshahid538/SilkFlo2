using Microsoft.AspNetCore.Mvc;
using SilkFlow.Api.Model;
using Microsoft.AspNetCore.Authorization;


namespace SilkFlow.Api.Repository.SilkFlowRepository
{
    public class SilkFlowApiRepository:ISilkFlowApiRepository
    {
       

        public async Task<ResponseModel> GetAllIdeas()
        {
            try
            {
                //var objs = await _applicationDbContext
                return new ResponseModel(true, "All Ideas!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetIdeasByUserId(int UserId)
        {
            try
            {
               // var objs = await _applicationDbContext
                return new ResponseModel(true, "Ideas By UserId!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
