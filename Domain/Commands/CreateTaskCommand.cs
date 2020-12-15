using System;

namespace Domain.Commands
{
    /// <summary>
    /// CreateTask Command
    /// </summary>
    public class CreateTaskCommand
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the assigned member identifier.
        /// </summary>
        /// <value>
        /// The assigned member identifier.
        /// </value>
        public Guid? AssignedMemberId { get; set; }
    }
}
