using AniRPG.Application.Game.CharacterSystem.Constants;
using FluentValidation;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Commands.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(CharacterConstants.NameMaxLength)
                .MinimumLength(CharacterConstants.NameMinLength);
        }
    }
}
