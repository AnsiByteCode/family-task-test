using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Task Service
    /// </summary>
    /// <seealso cref="Core.Abstractions.Services.ITaskService" />
    public class TaskService : ITaskService
    {
        /// <summary>
        /// The task repository
        /// </summary>
        private readonly ITaskRepository _taskRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="taskRepository">The task repository.</param>
        public TaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        /// <summary>
        /// Gets all tasks query handler.
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            var tasks = await _taskRepository.Reset().GetAllTasksWithMemberAsync();

            var taskVmList = tasks.Any()
                ? _mapper.Map<List<TaskVm>>(tasks)
                : new List<TaskVm>();

            return new GetAllTasksQueryResult()
            {
                Payload = taskVmList
            };
        }
        /// <summary>
        /// Creates the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">command</exception>
        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = _mapper.Map<Domain.DataModels.Task>(command);
            var createdTask = await _taskRepository.CreateRecordAsync(task);

            TaskVm taskViewModel = _mapper.Map<TaskVm>(createdTask);

            return new CreateTaskCommandResult
            {
                Payload = taskViewModel
            };
        }
        /// <summary>
        /// Completes the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">command</exception>
        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = await _taskRepository.ByIdAsync(command.Id);

            if (task.IsComplete)
            {
                return new CompleteTaskCommandResult()
                {
                    Succeed = true
                };
            }

            task.IsComplete = true;
            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            var succeed = affectedRecordsCount == 1;
            return new CompleteTaskCommandResult
            {
                Succeed = succeed
            };
        }
        /// <summary>
        /// Assigns the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">command</exception>
        public async Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = await _taskRepository.ByIdAsync(command.Id);

            _mapper.Map(command, task);

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            var succeed = affectedRecordsCount == 1;

            return new AssignTaskCommandResult
            {
                Succeed = succeed
            };
        }
    }
}
