using System;

namespace Domain.Commands
{
    /// <summary>
    /// CompleteTask Command
    /// </summary>
    public class CompleteTaskCommand
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}
