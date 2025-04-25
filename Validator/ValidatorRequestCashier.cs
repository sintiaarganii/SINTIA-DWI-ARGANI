using FluentValidation;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Validator
{
    public class ValidatorRequestCashier : AbstractValidator<CashierAccessReqDTO>
    {
        public ValidatorRequestCashier()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long.")
                .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
                .Matches("^[a-z]+$").WithMessage("Username hanya boleh huruf kecil semua (a-z)."); ;

            RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                    .MaximumLength(20).WithMessage("Password must not exceed 20 characters.")
                    .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$").WithMessage("Password harus terdiri dari kombinasi huruf dan angka.");
        }
    }
}