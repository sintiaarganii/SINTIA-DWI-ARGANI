using FluentValidation;
using SINTIA_DWI_ARGANI.Models.DTO;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Validator
{
    public class ValidatorRequestCategory : AbstractValidator<CategoryDTO>
    {
        public ValidatorRequestCategory()
        {
            RuleFor(x => x.CategoriName)
                .NotEmpty().WithMessage("Nama kategori wajib diisi.")
                .Length(3, 10).WithMessage("Nama kategori harus antara 3 sampai 10 huruf.")
                .Matches("^[A-Z][a-z]{2,9}$")
                .WithMessage("Huruf pertama harus kapital, sisanya huruf kecil, tanpa angka atau simbol.");

            RuleFor(x => x.StatusCategori)
                .IsInEnum().WithMessage("Status kategori harus 'Published' atau 'Unpublished'.");
        }
    }
}