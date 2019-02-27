using AniRPG.Game.Systems.CharacterSystem.Constants;
using FluentValidation;

namespace AniRPG.Game.Systems.CharacterSystem.UseCases.Commands.CreateCharacter
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
