using System.Collections.Generic;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetAllOrder(); 
        OrderDTO GetOrderById(int id);
        bool AddOrder(OrderDTO order);
        bool UpdateOrder(OrderDTO order);
        bool DeletedOrder(int id);
        public ReportDTO GenerateReport(DateTime startDate, DateTime endDate);
        public Task<List<OrderDTO>> GetAllHistory();
        Cashier GetCashierByName(string name);

        public List<OrderDTO> GetAllOrders();
    }
}