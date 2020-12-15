using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    /// <summary>
    /// AssignTask Command Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Domain.Commands.AssignTaskCommand}" />
    public class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignTaskCommandValidator"/> class.
        /// </summary>
        public AssignTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.AssignedMemberId).NotNull().NotEmpty();
        }
    }
}
