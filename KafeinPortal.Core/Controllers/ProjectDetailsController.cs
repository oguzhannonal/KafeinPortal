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
    public class ProjectDetailsController : ControllerBase
    {
        private readonly IProjectDetails _projectDetailsLogic;

        public ProjectDetailsController(IProjectDetails projectDetailsLogic)
        {
            _projectDetailsLogic = projectDetailsLogic;
        }
        
        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {

            return _projectDetailsLogic.Get();

        }
        [HttpGet("{id:int}", Name = "GetProjectDetail")]
        public ActionResult<ApiResponse> GetProjectDetail(int id)
        {
           return _projectDetailsLogic.GetProjectDetail(id);
        }
        [HttpPost]
        public ActionResult<ApiResponse> AddProjectDetail(ProjectDetailsRequest projectDetailsRequest)
        {
            return _projectDetailsLogic.AddProjectDetail(projectDetailsRequest);
        }

        [HttpDelete("{id:int}", Name = "DeleteProjectDetails")]
        public ActionResult<ApiResponse> DeleteProjectDetails(int id)
        {
            return _projectDetailsLogic.DeleteProjectDetails(id);            
        }
        [HttpPut]
        public ActionResult<ApiResponse> UpdateProjectDetail(ProjectDetailsRequest projectDetailRequest)
        {
            return _projectDetailsLogic.UpdateProjectDetail(projectDetailRequest);
        }
    }
}
