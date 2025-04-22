using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IPdfServices
    {
        public byte[] GenerateOrderReceipt(OrderDTO order);
    }
}
