using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    /// <summary>
    /// CreateMember Command Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Domain.Commands.CreateMemberCommand}" />
    public class CreateMemberCommandValidator: AbstractValidator<CreateMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMemberCommandValidator"/> class.
        /// </summary>
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
        }
    }
}
