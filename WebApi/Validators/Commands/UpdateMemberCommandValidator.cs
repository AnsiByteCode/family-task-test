using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    /// <summary>
    /// UpdateMember Command Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Domain.Commands.UpdateMemberCommand}" />
    public class UpdateMemberCommandValidator: AbstractValidator<UpdateMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMemberCommandValidator"/> class.
        /// </summary>
        public UpdateMemberCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
        }
    }
}
