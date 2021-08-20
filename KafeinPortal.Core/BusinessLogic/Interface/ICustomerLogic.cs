using KafeinPortal.Core.Requests;
using KafeinPortal.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace KafeinPortal.Core.BusinessLogic.Interface
{
    public interface ICustomerLogic
    {
        public ActionResult<ApiResponse> GetCustomerWithDetails(int id);
        public ActionResult<ApiResponse> Get();
        public ActionResult<ApiResponse> GetCustomerProjects(int id);
        public ActionResult<ApiResponse> GetCustomer(int id);
        public ActionResult<ApiResponse> AddCustomer(CustomerRequest customerRequest);
        public ActionResult<ApiResponse> DeleteCustomer(int id);
        public ActionResult<ApiResponse> UpdateCustomer(CustomerRequest customerRequest);


    }
}
