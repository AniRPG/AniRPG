using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AniRPG.Application.Game.MapSystem.Constants;

namespace AniRPG.Application.Game.MapSystem.UseCases.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(CharacterConstants.NameMaxLength)
                .MinimumLength(CharacterConstants.NameMinLength);
        }
    }
}
