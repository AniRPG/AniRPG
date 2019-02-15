using AniRPG.Application.Game.MapSystem.Constants;
using FluentValidation;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(LocationConstants.NameMaxLength)
                .MinimumLength(LocationConstants.NameMinLength);
        }
    }
}
