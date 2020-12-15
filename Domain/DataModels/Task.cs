using System;

namespace Domain.DataModels
{
    /// <summary>
    /// Task Entity
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </value>
        public bool IsComplete { get; set; }
        /// <summary>
        /// Gets or sets the assigned member identifier.
        /// </summary>
        /// <value>
        /// The assigned member identifier.
        /// </value>
        public Guid? AssignedMemberId { get; set; }
        /// <summary>
        /// Gets or sets the assigned member.
        /// </summary>
        /// <value>
        /// The assigned member.
        /// </value>
        public virtual Member AssignedMember { get; set; }
    }
}
