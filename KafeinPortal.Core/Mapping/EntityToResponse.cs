using AutoMapper;
using KafeinPortal.Core.Requests;
using KafeinPortal.Core.Responses;
using KafeinPortal.Data.Model.Models;

namespace KafeinPortal.Core.Mapping
{
    public class EntityToResponse : Profile
    {
        public EntityToResponse()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Project, ProjectResponse>();
            CreateMap<ProjectDetail, ProjectDetailsResponse>();
            CreateMap<CustomerRequest, Customer>();
            CreateMap<ProjectRequest, Project>();
            CreateMap<ProjectDetailsRequest, ProjectDetail>();
            

        }
    }
}