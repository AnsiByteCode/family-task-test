using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// NotFoundException Exception
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <seealso cref="System.Exception" />
    public class NotFoundException<TIdentifier> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException{TIdentifier}"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="id">The identifier.</param>
        public NotFoundException(string objectType, TIdentifier id)
        {
            this.ObjectType = objectType;
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        public string ObjectType { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public TIdentifier Id { get; set; }
    }
}
