using System;

namespace Domain.Commands
{
    /// <summary>
    /// AssignTask Command
    /// </summary>
    public class AssignTaskCommand
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the assigned member identifier.
        /// </summary>
        /// <value>
        /// The assigned member identifier.
        /// </value>
        public Guid AssignedMemberId { get; set; }
    }
}
