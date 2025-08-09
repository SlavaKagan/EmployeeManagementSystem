using EmployeeManagementSystem.Application.DTOs;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                 .Matches("^[A-Za-zא-ת]+$").WithMessage("First name must contain only letters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .Matches("^[A-Za-zא-ת]+$").WithMessage("Last name must contain only letters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be valid");

            RuleFor(x => x.HireDate)
                .NotEmpty()
                .Must(d => d.Date <= DateTime.UtcNow.Date)
                .WithMessage("Hire date cannot be in the future");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Department is required");
        }
    }
}
