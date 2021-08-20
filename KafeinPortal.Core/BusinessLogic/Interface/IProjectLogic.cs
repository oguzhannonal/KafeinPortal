using KafeinPortal.Core.Requests;
using KafeinPortal.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace KafeinPortal.Core.BusinessLogic.Interface
{
    public interface IProjectLogic
    {
        public ActionResult<ApiResponse> GetProjectWithDetails(int id);
        public ActionResult<ApiResponse> Get();
        public ActionResult<ApiResponse> GetProject(int id);
        public ActionResult<ApiResponse> AddProject(ProjectRequest projectRequest);
        public ActionResult<ApiResponse> GetProjectDetails(int id);
        public ActionResult<ApiResponse> DeleteProject(int id);
        public ActionResult<ApiResponse> UpdateProject(ProjectRequest projectRequest);

    }
}
