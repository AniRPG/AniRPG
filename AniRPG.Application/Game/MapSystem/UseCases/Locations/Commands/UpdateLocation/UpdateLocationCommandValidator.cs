using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AniRPG.Application.Game.MapSystem.Constants;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        UpdateLocationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(LocationConstants.NameMaxLength)
                .MinimumLength(LocationConstants.NameMinLength);
            RuleFor(x => x.Description).MaximumLength(LocationConstants.DescriptionMaxLength)
                .MinimumLength(LocationConstants.DescriptionMinLength);
        }
    }
}
