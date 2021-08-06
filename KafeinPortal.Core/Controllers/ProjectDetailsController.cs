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
using KafeinPortal.Data.Context;

namespace KafeinPortal.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectDetailsController : ControllerBase
    {
        private readonly ILogger<ProjectDetailsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProjectDetail> _projectDetail;
        
        private readonly EfContext _dbContext;

        public ProjectDetailsController(IUnitOfWork unitOfWork, ILogger<ProjectDetailsController> logger, IRepository<ProjectDetail> projectDetail,EfContext dbcontext)
        {
            _projectDetail = projectDetail;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbContext = dbcontext;
        }
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            try
            {
                _logger.LogInformation("Called Get method WeatherForecastController");
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm Projeler detayları geldi.", _projectDetail.GetAll().ToList());
                _logger.LogDebug("Debug message called get method ProjectdetailsController");

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        [HttpGet("{id:int}", Name = "GetProjectDetail")]
        public ActionResult<ApiResponse> GetProjectDetail(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called getprojectdetail method ProjectdetailsController");
                _logger.LogInformation("called getprojectdetail method ProjectdetailsController");

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir proje detayı geldi.", _projectDetail.Get(s => s.Id == id));

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public ActionResult<ApiResponse> AddProjectDetail(ProjectDetail projectDetail)
        {



            try
            {
                _logger.LogDebug("Debug message called AddProjectdetail method ProjectdetailsController");
                _logger.LogInformation("called AddProjectdetail method ProjectdetailsController");
                _projectDetail.Add(projectDetail);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje Detaylari Eklendi.", projectDetail);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }


        }

        [HttpDelete("{id:int}", Name = "DeleteProjectDetails")]
        public ActionResult<ApiResponse> DeleteProjectDetails(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called DeleteProjectDetails method ProjectdetailsController");
                _logger.LogInformation("called DeleteProjectDetails method ProjectdetailsController");
                var deletedProject = _projectDetail.Get(s => s.Id == id);

                _projectDetail.Delete(deletedProject);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje Detaylari Silindi.", deletedProject);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }

        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateProjectDetail(ProjectDetail projectDetail)
        {
            try
            {
                _logger.LogDebug("Debug message called UpdateProjectDetail method ProjectdetailsController");
                _logger.LogInformation("called UpdateProjectDetail method ProjectdetailsController");
                _projectDetail.Update(projectDetail);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje Detaylari Güncellendi.", projectDetail);
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
