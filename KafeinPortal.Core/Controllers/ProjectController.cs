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
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<Customer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customer;
        private readonly IRepository<Project> _Project;
        private readonly IRepository<ProjectDetail> _projectDetail;

        public ProjectController(IUnitOfWork unitOfWork, ILogger<Customer> logger,IRepository<Customer> customer,IRepository<Project> Project,IRepository<ProjectDetail> projectDetail)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customer =customer;
            _Project = Project;
            _projectDetail =projectDetail;
        }
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            try
            {

                _logger.LogInformation("Called Get method ProjectController");
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm projeler geldi.",_Project.GetAll().ToList());
                _logger.LogDebug("Debug message called get method ProjectController");

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        [HttpGet("{id:int}", Name = "GetProject")]
        public ActionResult<ApiResponse> GetProject(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called GetProject method ProjectController");
                _logger.LogInformation("called GetProject method ProjectController");
               
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir proje geldi", _Project.Get(s => s.Id == id));
                
                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public ActionResult<ApiResponse> AddProject(Project project)
        {
            try
            {
                
                _logger.LogDebug("Debug message called AddCustomer method ProjectController");
                _logger.LogInformation("called AddCustomer method ProjectController");
                
                _Project.Add(project);
                
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri Eklendi.", project);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }


            }
        [HttpDelete("{id:int}", Name = "DeleteProject")]
        public ActionResult<ApiResponse> DeleteProject(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called DeleteProject method ProjectController");
                _logger.LogInformation("called DeleteProject method ProjectController");
                var deletedProject = _Project.Get(s => s.Id == id);
                
                _Project.Delete(deletedProject);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje  Silindi.", deletedProject);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }

        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateProject(Project project)
        {
            try
            {
                _logger.LogDebug("Debug message called UpdateProject method ProjectController");
                _logger.LogInformation("called UpdateProject method ProjectController");
                _Project.Update(project);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje  Güncellendi.", project);
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
