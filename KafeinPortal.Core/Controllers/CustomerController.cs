using KafeinPortal.Data.Model;
using KafeinPortal.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KafeinPortal.Data.Model.Models;


namespace KafeinPortal.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<Customer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customer;
        private readonly IRepository<Project> _Project;

        public CustomerController(IUnitOfWork unitOfWork, ILogger<Customer> logger, IRepository<Customer> customer, IRepository<Project> Project)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customer = customer;
            _Project = Project;
        }
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            try
            {

                _logger.LogInformation("Called Get method CustomerController");
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm müşteriler geldi.", _customer.GetAll().ToList());
                _logger.LogDebug("Debug message called get method CustomerController");

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        [Route("GetCustomerProjects")]
        [HttpGet]
        public ActionResult<ApiResponse> GetCustomerProjects(int id)
        {

            try
            {
                _logger.LogInformation("Called Get method GetCustomerProjects");
                var customer = _customer.Get(s => s.Id == id);
                var projects = _Project.GetAll(s => s.CustomerId == customer.Id);
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir müşterinin projeleri geldi.", projects);
                return apiResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }



        }
        [HttpGet("{id:int}", Name = "GetCustomer")]
        public ActionResult<ApiResponse> GetCustomer(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called GetCustomer method CustomerController");
                _logger.LogInformation("called GetCustomer method CustomerController");

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir müşteri geldi", _customer.Get(s => s.Id == id));

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public ActionResult<ApiResponse> AddCustomer(Customer customer)
        {
            try
            {
                _logger.LogDebug("Debug message called AddCustomer method CustomerController");
                _logger.LogInformation("called AddCustomer method CustomerController");

                _customer.Add(customer);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri Eklendi.", customer);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }


        }
        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        public ActionResult<ApiResponse> DeleteCustomer(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called DeleteCustomer method CustomerController");
                _logger.LogInformation("called DeleteCustomer method CustomerController");
                var deletedProject = _customer.Get(s => s.Id == id);

                _customer.Delete(deletedProject);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri  Silindi.", deletedProject);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }

        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateCustomer(Customer customer)
        {
            try
            {
                _logger.LogDebug("Debug message called UpdateCustomer method CustomerController");
                _logger.LogInformation("called UpdateCustomer method CustomerController");
                _customer.Update(customer);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri  Güncellendi.", customer);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }

        }


    }
}
