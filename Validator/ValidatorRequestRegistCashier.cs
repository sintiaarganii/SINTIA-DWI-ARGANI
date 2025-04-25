using FluentValidation;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Validator
{
    public class ValidatorRequestRegistCashier : AbstractValidator<RegistCashierDTO>
    {
        public ValidatorRequestRegistCashier()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nama wajib diisi.")
            .Length(2, 30).WithMessage("Nama harus 2-30 karakter.")
            .Matches(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$").WithMessage("Nama harus diawali huruf kapital dan hanya huruf serta spasi.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username wajib diisi.")
                .Length(5, 20).WithMessage("Username minimal 5 huruf.")
                .Matches("^[a-z]+$").WithMessage("Username hanya boleh huruf kecil, tanpa angka atau simbol.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password wajib diisi.")
                .MinimumLength(3).WithMessage("Password minimal 3 karakter.")
                .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$").WithMessage("Password harus kombinasi huruf dan angka, tanpa spasi.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Konfirmasi password tidak cocok.");
        }
    }
}
