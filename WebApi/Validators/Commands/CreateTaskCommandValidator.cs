using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    /// <summary>
    /// CreateTask Command Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Domain.Commands.CreateTaskCommand}" />
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTaskCommandValidator"/> class.
        /// </summary>
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Subject).NotNull().NotEmpty();
        }
    }
}
