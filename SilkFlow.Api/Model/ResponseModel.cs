using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlow.Api.Model
{
    public class ResponseModel
    {
        public ResponseModel(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public ResponseModel(bool status, string message,string token)
        {
            this.Status = status;
            this.Message = message;
            this.Token = token;
        }
        public ResponseModel(bool status, string message, string token,Object data)
        {
            this.Status  = status;
            this.Message = message;
            this.Token   = token;
            this.Data    = data;
        }

        public ResponseModel(bool status, string message, string token, Object data, Object data1)
        {
            this.Status  = status;
            this.Message = message;
            this.Token   = token;
            this.Data    = data;
            this.Data1   = data1;
        }


        public ResponseModel(bool status, string message, Object model)
        {
            this.Status = status;
            this.Message = message;
            this.Data = model;
        }

        public ResponseModel(bool status, string message, Object model, Object model1)
        {
            this.Status = status;
            this.Message = message;
            this.Data = model;
            this.Data1 = model1;
        }
         public ResponseModel(bool status, string message, Object model, Object model1,string roleId)
        {
            this.Status = status;
            this.Message = message;
            this.Data = model;
            this.Data1 = model1;
            this.roleId = roleId;
        }
        public ResponseModel(bool status, string message, Object model, double totalIncome, double totalOutcome)
        {
            this.Status = status;
            this.Message = message;
            this.Data = model;
            this.TotalIncome = totalIncome;
            this.TotalOutcome = totalOutcome;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public string roleId { get; set; }
        public string Token { get; set; }
        public Object Data { get; set; }
        public Object Data1 { get; set; }
        public double TotalIncome { get; set; }
        public double TotalOutcome { get; set; }
    }
}
