using EmployeeManagementSystem.Application.DTOs;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentDTO>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(100).WithMessage("Department name must be 100 characters or fewer");
        }
    }
}
