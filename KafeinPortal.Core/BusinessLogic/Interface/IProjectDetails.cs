using KafeinPortal.Core.Requests;
using KafeinPortal.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace KafeinPortal.Core.BusinessLogic.Interface
{
    public interface IProjectDetails
    {
        public ActionResult<ApiResponse> Get();
        public ActionResult<ApiResponse> GetProjectDetail(int id);
        public ActionResult<ApiResponse> AddProjectDetail(ProjectDetailsRequest projectDetailsRequest);
        public ActionResult<ApiResponse> DeleteProjectDetails(int id);
        public ActionResult<ApiResponse> UpdateProjectDetail(ProjectDetailsRequest projectDetailRequest);

    }
}
