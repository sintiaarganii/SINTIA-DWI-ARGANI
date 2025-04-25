using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IOrdering
    {
        void CreateOrder(OrderingDTO orderDto);
        public List<OrderingDTO> GetAllOrders();
        List<OrderingDTO> GetOrderReport();

    }
}
