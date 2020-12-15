using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;

namespace WebApi.AutoMapper
{
    /// <summary>
    /// Task Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TaskProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskProfile"/> class.
        /// </summary>
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Task>();
            CreateMap<AssignTaskCommand, Task>();
            CreateMap<CompleteTaskCommand, Task>();
            CreateMap<Task, TaskVm>().ForMember(taskVm => taskVm.Member, task => task.MapFrom(objectTask => objectTask.AssignedMember));
        }
    }
}
