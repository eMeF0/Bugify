using AutoMapper;
using Bugify.API.Models.Domain;
using Bugify.API.Models.DTO;

namespace Bugify.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Add your mapping configurations here
            // Example:
            // CreateMap<SourceType, DestinationType>();

            CreateMap<CreateTaskDto, AddTask>().ReverseMap();
            CreateMap<AddTask, TaskDto>().ReverseMap();
            CreateMap<UpdateTaskRequestDto, AddTask>().ReverseMap();
            CreateMap<AddTask, TaskDto>().
                ForMember(x => x.ProgressName, 
                    c => c.MapFrom(s => s.Progress.Name));

        }
    }
}
