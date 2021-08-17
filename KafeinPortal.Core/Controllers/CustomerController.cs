using KafeinPortal.Data.Model;
using KafeinPortal.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using KafeinPortal.Data.Model.Models;
using KafeinPortal.Core.Responses;
using KafeinPortal.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using KafeinPortal.Data.Context;
using KafeinPortal.Data.Model.Dtos;
using System.Net.Http;
using System.Threading;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using KafeinPortal.Core.Helpers;

namespace KafeinPortal.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<Customer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customer;
        private readonly IRepository<Project> _Project;
        private readonly IMapper _mapper;
        private readonly EfContext _context;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;



        public CustomerController(IJwtAuthenticationManager jwtAuthenticationManager,EfContext context, IUnitOfWork unitOfWork, ILogger<Customer> logger, IRepository<Customer> customer, IRepository<Project> Project, IMapper mapper)
        {
            _jwtAuthenticationManager  = jwtAuthenticationManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customer = customer;
            _Project = Project;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        [Route("LoginApi")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]UserLogin userLogin)
        {
            var values = new List<KeyValuePair<string, string>>
            {
                new("grant_type","password"),
                new("username",userLogin.username),
                new("password",userLogin.password)
            };
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic a2JpX2NsaWVudDprYmlfc2VjcmV0");


                var cts = new CancellationTokenSource(new TimeSpan(0, 0, 5));
                try
                {

                    using (var response = await httpClient.PostAsync("http://161.35.77.124:8080/portal-api/auth-server/oauth/token", new FormUrlEncodedContent(values), cts.Token).ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {

                            var kafeinApiResponse = System.Text.Json.JsonSerializer.Deserialize<KafeinApiResponse>(response.Content.ReadAsStringAsync().Result);
                            
                            var token = _jwtAuthenticationManager.Authenticate(userLogin);
                            
                            

                            return Ok(token);
                        }
                        else
                        {
                            return Unauthorized();
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return BadRequest();
            }

        }

        [Route("GetCustomerWithDetails")]
        [HttpGet]
        public ActionResult<ApiResponse> GetCustomerWithDetails(int id)
        {
            var customers = _customer.Get(s => id == s.Id);
            var customer = _context.Customers.Where(s => id == s.Id).Include(project => project.Projects).ThenInclude(projectDetails => projectDetails.ProjectDetails).FirstOrDefault();
            var customerResponse = _mapper.Map<CustomerDto>(customer);
            ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm müşteriler geldi.", customerResponse);

            return apiResponse;

        }
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            try
            {
                var customers = _customer.GetAll();
                var customerResponse = _mapper.Map<List<CustomerResponse>>(customers);
                _logger.LogInformation("Called Get method CustomerController");
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm müşteriler geldi.", customerResponse);
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
                var projectResponse = _mapper.Map<List<ProjectResponse>>(projects);

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir müşterinin projeleri geldi.", projectResponse);
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
                var item = _customer.Get(s => s.Id == id);

                var customerResponse = _mapper.Map<CustomerResponse>(item);

                _logger.LogDebug("Debug message called GetCustomer method CustomerController");
                _logger.LogInformation("called GetCustomer method CustomerController");

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir müşteri geldi", customerResponse);

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public ActionResult<ApiResponse> AddCustomer(CustomerRequest customerRequest)
        {
            try
            {
                _logger.LogDebug("Debug message called AddCustomer method CustomerController");
                _logger.LogInformation("called AddCustomer method CustomerController");
                var customer = _mapper.Map<Customer>(customerRequest);
                _customer.Add(customer);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri Eklendi.", customerRequest);
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
                var customerResponse = _mapper.Map<CustomerResponse>(deletedProject);
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri  Silindi.", customerResponse);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }

        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateCustomer(CustomerRequest customerRequest)
        {
            try
            {
                _logger.LogDebug("Debug message called UpdateCustomer method CustomerController");
                _logger.LogInformation("called UpdateCustomer method CustomerController");
                var customer = _mapper.Map<Customer>(customerRequest);

                _customer.Update(customer);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri  Güncellendi.", customerRequest);
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
