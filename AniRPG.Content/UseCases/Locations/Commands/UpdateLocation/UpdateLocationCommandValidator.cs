using FluentValidation;

namespace AniRPG.Content.UseCases.Locations.Commands.UpdateLocation
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
