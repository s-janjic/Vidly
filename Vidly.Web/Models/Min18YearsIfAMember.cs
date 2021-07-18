using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Web.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // as ObjectInstance is an object, we need to cast to Customer type
            var customer = validationContext.ObjectInstance as Customer;

            if (customer != null && (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1))
                return ValidationResult.Success;

            // birthdate is required, if null return error message
            if (customer.BirthDateTime == null)
                return new ValidationResult("Birthdate is required.");

            // checking if age is greater than 18
            var age = DateTime.Today.Year - customer.BirthDateTime.Value.Year;
            return age > 18 ? 
                ValidationResult.Success : 
                new ValidationResult("Customer should be at least 18 years old.");
        }
    }
}