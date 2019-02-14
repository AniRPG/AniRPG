using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AniRPG.Application.Game.MapSystem.Constants;

namespace AniRPG.Application.Game.MapSystem.UseCases.Location.Commands
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(LocationConstants.LocationNameMaxLength).MinimumLength(LocationConstants.LocationNameMinLength);
        }
    }
}
