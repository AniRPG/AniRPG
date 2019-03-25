 using FluentValidation;

namespace AniRPG.Content.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(LocationConstants.NameMinLength)
                .MaximumLength(LocationConstants.NameMaxLength);

            RuleFor(x => x.Description)
                .MinimumLength(LocationConstants.DescriptionMinLength)
                .MaximumLength(LocationConstants.DescriptionMaxLength);
        }
    }
}
