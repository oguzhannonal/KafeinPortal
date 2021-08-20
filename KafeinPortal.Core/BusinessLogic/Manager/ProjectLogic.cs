using AutoMapper;
using KafeinPortal.Core.BusinessLogic.Interface;
using KafeinPortal.Core.Requests;
using KafeinPortal.Core.Responses;
using KafeinPortal.Data.Context;
using KafeinPortal.Data.Model;
using KafeinPortal.Data.Model.Dtos;
using KafeinPortal.Data.Model.Models;
using KafeinPortal.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KafeinPortal.Core.BusinessLogic.Manager
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly ILogger<Customer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customer;
        private readonly IRepository<Project> _Project;
        private readonly IRepository<ProjectDetail> _projectDetail;
        private readonly IMapper _mapper;
        private readonly EfContext _context;
        public ProjectLogic(EfContext context, IUnitOfWork unitOfWork, ILogger<Customer> logger, IRepository<Customer> customer,
            IRepository<Project> Project, IMapper mapper, IRepository<ProjectDetail> projectDetail)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customer = customer;
            _Project = Project;
            _projectDetail = projectDetail;
            _context = context;
        }
        public ActionResult<ApiResponse> AddProject(ProjectRequest projectRequest)
        {
            try
            {
                _logger.LogDebug("Debug message called AddCustomer method ProjectController");
                _logger.LogInformation("called AddCustomer method ProjectController");
                var project = _mapper.Map<Project>(projectRequest);
                _Project.Add(project);

                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Müşteri Eklendi.", projectRequest);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public ActionResult<ApiResponse> DeleteProject(int id)
        {
            try
            {
                _logger.LogDebug("Debug message called DeleteProject method ProjectController");
                _logger.LogInformation("called DeleteProject method ProjectController");
                var deletedProject = _Project.Get(s => s.Id == id);
                var projectResponse = _mapper.Map<ProjectResponse>(deletedProject);
                _Project.Delete(deletedProject);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Proje  Silindi.", projectResponse);
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
                _logger.LogInformation("Called Get method ProjectController");
                var projects = _Project.GetAll();
                var projectResponse = _mapper.Map<List<ProjectResponse>>(projects);

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm projeler geldi.", projectResponse);
                _logger.LogDebug("Debug message called get method ProjectController");

                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public ActionResult<ApiResponse> GetProject(int id)
        {
            try
            {
                var project = _Project.Get(s => s.Id == id);
                var projectResponse = _mapper.Map<ProjectResponse>(project);
                _logger.LogDebug("Debug message called GetProject method ProjectController");
                _logger.LogInformation("called GetProject method ProjectController");

                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tek bir proje geldi", projectResponse);

                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public ActionResult<ApiResponse> GetProjectDetails(int id)
        {
            try
            {
                _logger.LogInformation("Called Get method GetProjectDetails");
                var project = _Project.Get(s => s.Id == id);
                var projectDetails = _projectDetail.Get(s => s.ProjectId == project.Id);
                var projectDetailsResponse = _mapper.Map<ProjectDetailsResponse>(projectDetails);
                ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Projenin detayları geldi.",
                    projectDetailsResponse);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public ActionResult<ApiResponse> GetProjectWithDetails(int id)
        {
            var projects = _Project.Get(s => id == s.Id);
            var project = _context.Projects.Where(s => id == s.Id).Include(projectDetails => projectDetails.ProjectDetails).FirstOrDefault();
            var projectResponse = _mapper.Map<ProjectDto>(project);
            ApiResponse apiResponse = new ApiResponse(HttpStatusCode.OK, true, "Tüm projeler detaylariyla geldi.", projectResponse);

            return apiResponse;
        }

        public ActionResult<ApiResponse> UpdateProject(ProjectRequest projectRequest)
        {
            try
            {
                _logger.LogDebug("Debug message called UpdateProject method ProjectController");
                _logger.LogInformation("called UpdateProject method ProjectController");
                var project = _mapper.Map<Project>(projectRequest);
                _Project.Update(project);
                _unitOfWork.SaveChanges();
                ApiResponse apiResponse =
                    new ApiResponse(HttpStatusCode.OK, true, "Proje  Güncellendi.", projectRequest);
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
