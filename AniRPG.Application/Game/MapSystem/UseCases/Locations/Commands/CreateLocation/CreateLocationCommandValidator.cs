﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AniRPG.Application.Game.MapSystem.Constants;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands
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
