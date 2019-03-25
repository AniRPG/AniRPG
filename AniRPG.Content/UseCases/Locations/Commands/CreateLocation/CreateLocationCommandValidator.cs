using FluentValidation;

namespace AniRPG.Content.UseCases.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(LocationConstants.NameMinLength)
                .MaximumLength(LocationConstants.NameMaxLength);
        }
    }
}
