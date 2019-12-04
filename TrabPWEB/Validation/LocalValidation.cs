using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrabPWEB.DAL;
using TrabPWEB.Models;

namespace TrabPWEB.Validation
{
    public class LocalValidation : CompareAttribute
    {
        public LocalValidation(string other_property) : base(other_property) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (base.IsValid(value, validationContext) == ValidationResult.Success)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}