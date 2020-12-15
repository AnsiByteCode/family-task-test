using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    /// <summary>
    /// CompleteTask Command Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Domain.Commands.CompleteTaskCommand}" />
    public class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteTaskCommandValidator"/> class.
        /// </summary>
        public CompleteTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
