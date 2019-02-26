using AniRPG.Application.Game.MapSystem.Constants;
using FluentValidation;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(LocationConstants.NameMaxLength)
                .MinimumLength(LocationConstants.NameMinLength);
            RuleFor(x => x.Description)
                .MaximumLength(LocationConstants.DescriptionMaxLength)
                .MinimumLength(LocationConstants.DescriptionMinLength);
        }
    }
}
