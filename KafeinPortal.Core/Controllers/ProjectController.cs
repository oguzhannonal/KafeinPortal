using KafeinPortal.Data.Model;
using Microsoft.AspNetCore.Mvc;
using KafeinPortal.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using KafeinPortal.Core.BusinessLogic.Interface;

namespace KafeinPortal.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectController : ControllerBase
    {
        private readonly IProjectLogic _projectLogic;



        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic  = projectLogic;
        }
        [Route("GetProjectWithDetails")]
        [HttpGet]
        public ActionResult<ApiResponse> GetProjectWithDetails(int id)
        {
            return _projectLogic.GetProjectWithDetails(id);

        }

        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {
            return _projectLogic.Get();
        }

        [HttpGet("{id:int}", Name = "GetProject")]
        public ActionResult<ApiResponse> GetProject(int id)
        {
            return _projectLogic.GetProject(id);
        }

        [HttpPost]
        public ActionResult<ApiResponse> AddProject(ProjectRequest projectRequest)
        {
           return _projectLogic.AddProject(projectRequest);
        }

        [Route("GetProjectDetails")]
        [HttpGet]
        public ActionResult<ApiResponse> GetProjectDetails(int id)
        {
            return _projectLogic.GetProjectDetails(id);
        }

        [HttpDelete("{id:int}", Name = "DeleteProject")]
        public ActionResult<ApiResponse> DeleteProject(int id)
        {
           return _projectLogic.DeleteProject(id);
        }

        [HttpPut]
        public ActionResult<ApiResponse> UpdateProject(ProjectRequest projectRequest)
        {
            return _projectLogic.UpdateProject(projectRequest);
        }
    }
}