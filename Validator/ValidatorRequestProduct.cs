using FluentValidation;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Validator
{
    public class ValidatorRequestProduct : AbstractValidator<ProductDTO>
    {
        public ValidatorRequestProduct()
        {
            RuleFor(x => x.NameProduct)
            .NotEmpty().WithMessage("Nama produk wajib diisi.")
            .Length(3, 20).WithMessage("Nama produk harus 3-20 huruf.")
            .Matches("^[A-Z][a-z]+$").WithMessage("Nama produk harus diawali huruf kapital, sisanya huruf kecil, tanpa angka atau simbol.");

            RuleFor(x => x.Price)
                .InclusiveBetween(1000, 1_000_000_000).WithMessage("Harga harus antara Rp 1.000 - Rp 1.000.000.000.");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 10_000).WithMessage("Stok harus antara 1 - 10.000.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Deskripsi maksimal 200 karakter.")
                .Matches(@"^[a-zA-Z0-9\s.,]*$").WithMessage("Deskripsi hanya boleh berisi huruf, angka, spasi, koma, dan titik.");
        }
    }
}
