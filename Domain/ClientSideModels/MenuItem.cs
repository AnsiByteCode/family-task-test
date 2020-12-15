using System;

namespace Domain.ClientSideModels
{
    /// <summary>
    /// Menu Item
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool isActive {get; set;}
        /// <summary>
        /// Gets or sets the color of the icon.
        /// </summary>
        /// <value>
        /// The color of the icon.
        /// </value>
        public string iconColor { get; set; }
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string label { get; set; }
        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public Guid referenceId { get; set; }
    }
}
