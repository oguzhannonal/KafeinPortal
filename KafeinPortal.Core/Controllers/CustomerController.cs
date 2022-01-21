using KafeinPortal.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KafeinPortal.Data.Model.Models;
using KafeinPortal.Core.Responses;
using KafeinPortal.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading;
using KafeinPortal.Core.Helpers;
using KafeinPortal.Core.BusinessLogic.Interface;

namespace KafeinPortal.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly ICustomerLogic _customerLogic;



        public CustomerController(ICustomerLogic customerLogic, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _customerLogic = customerLogic;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            
        }
        [HttpPost]
        [Route("LoginApi")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLogin userLogin)
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
                httpClient.DefaultRequestHeaders.Add();


                var cts = new CancellationTokenSource(new TimeSpan(0, 0, 5));
                try
                {

                    using (var response = await httpClient.PostAsync(, new FormUrlEncodedContent(values), cts.Token).ConfigureAwait(false))
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

            return _customerLogic.GetCustomerWithDetails(id);

        }
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            return _customerLogic.Get();

        }
        [Route("GetCustomerProjects")]
        [HttpGet]
        public ActionResult<ApiResponse> GetCustomerProjects(int id)
        {

            return _customerLogic.GetCustomerProjects(id);



        }
        [HttpGet("{id:int}", Name = "GetCustomer")]
        public ActionResult<ApiResponse> GetCustomer(int id)
        {
            return _customerLogic.GetCustomer(id);

        }
        [HttpPost]
        public ActionResult<ApiResponse> AddCustomer(CustomerRequest customerRequest)
        {
            return _customerLogic.AddCustomer(customerRequest);


        }
        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        public ActionResult<ApiResponse> DeleteCustomer(int id)
        {
          return _customerLogic.DeleteCustomer(id);
        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateCustomer(CustomerRequest customerRequest)
        {
            return _customerLogic.UpdateCustomer(customerRequest);
        }


    }
}
