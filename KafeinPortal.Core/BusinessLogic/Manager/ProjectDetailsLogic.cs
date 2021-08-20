using AutoMapper;
using KafeinPortal.Core.BusinessLogic.Interface;
using KafeinPortal.Core.Controllers;
using KafeinPortal.Core.Requests;
using KafeinPortal.Core.Responses;
using KafeinPortal.Data.Context;
using KafeinPortal.Data.Model;
using KafeinPortal.Data.Model.Models;
using KafeinPortal.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace KafeinPortal.Core.BusinessLogic.Manager
{
    public class ProjectDetailsLogic : IProjectDetails
    {
        private readonly IProjectDetails _ProjectDetailsLogic;
        private readonly ILogger<ProjectDetailsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProjectDetail> _projectDetail;
        private readonly IMapper _mapper; 
        private readonly EfContext _dbContext;
        public ProjectDetailsLogic(IUnitOfWork unitOfWork,IMapper mapper , ILogger<ProjectDetailsController> logger, IRepository<ProjectDetail> projectDetail,EfContext dbcontext)
        {
            _projectDetail = projectDetail;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbContext = dbcontext;
            _mapper = mapper;
        }
        
        public ActionResult<ApiResponse> AddProjectDetail(ProjectDetailsRequest projectDetailsRequest)
        {
           try
            {
                _logger.LogDebug("Debug message called AddProjectdetail method ProjectdetailsController");
                _logger.LogInformation("called AddProjectdetail method ProjectdetailsController");
                var projectDetail = _mapper.Map<ProjectDetail>(projectDetailsRequest);
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

        public ActionResult<ApiResponse> DeleteProjectDetails(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called DeleteProjectDetails method ProjectdetailsController");
                _logger.LogInformation("called DeleteProjectDetails method ProjectdetailsController");
                var deletedProject = _projectDetail.Get(s => s.Id == id);
                var projectDetailsResponse = _mapper.Map<ProjectDetailsResponse>(deletedProject);

                _projectDetail.Delete(deletedProject);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje Detaylari Silindi.", projectDetailsResponse);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public ActionResult<ApiResponse> Get()
        {
            try
            {
                var projectDetails = _projectDetail.GetAll();
                var projectDetailsResponse = _mapper.Map<List<ProjectDetailsResponse>>(projectDetails);
                _logger.LogInformation("Called Get method WeatherForecastController");
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm Projeler detayları geldi.",projectDetailsResponse);
                _logger.LogDebug("Debug message called get method ProjectdetailsController");

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        public ActionResult<ApiResponse> GetProjectDetail(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called getprojectdetail method ProjectdetailsController");
                _logger.LogInformation("called getprojectdetail method ProjectdetailsController");
                var projectDetail =_projectDetail.Get(s => s.Id == id);
                var projectResponse = _mapper.Map<ProjectDetailsResponse>(projectDetail);
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir proje detayı geldi.",projectResponse);

                return apiResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public ActionResult<ApiResponse> UpdateProjectDetail(ProjectDetailsRequest projectDetailRequest)
        {
             try
            {
                _logger.LogDebug("Debug message called UpdateProjectDetail method ProjectdetailsController");
                _logger.LogInformation("called UpdateProjectDetail method ProjectdetailsController");
                var projectDetail = _mapper.Map<ProjectDetail>(projectDetailRequest);
                _projectDetail.Update(projectDetail);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje Detaylari Güncellendi.", projectDetailRequest);
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
