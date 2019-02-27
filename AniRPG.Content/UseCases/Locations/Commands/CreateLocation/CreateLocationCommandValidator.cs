using FluentValidation;

namespace AniRPG.Content.UseCases.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(LocationConstants.NameMaxLength)
                .MinimumLength(LocationConstants.NameMinLength);
        }
    }
}
