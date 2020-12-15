using Domain.ViewModel;

namespace Domain.Commands
{
    /// <summary>
    /// CreateTask Command Result
    /// </summary>
    public class CreateTaskCommandResult
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public TaskVm Payload { get; set; }
    }
}
