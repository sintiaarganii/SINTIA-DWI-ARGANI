using SINTIA_DWI_ARGANI.Models.DB;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class DailyDashboardDTO
    {
        // Data Hari Ini
        public int TodaysOrderCount { get; set; }
        public decimal TodaysTotalRevenue { get; set; }
        public int TodaysProductsSold { get; set; }

        // Data Kemarin (Untuk Perbandingan)
        public int YesterdaysOrderCount { get; set; }
        public decimal YesterdaysTotalRevenue { get; set; }
        public int YesterdaysProductsSold { get; set; }

        // Daftar Transaksi Hari Ini (Opsional)
        public List<Ordering> TodaysOrders { get; set; } = new List<Ordering>();

        // Persentase Perubahan
        public decimal RevenueChangePercentage => CalculatePercentage(TodaysTotalRevenue, YesterdaysTotalRevenue);
        public decimal OrdersChangePercentage => CalculatePercentage(TodaysOrderCount, YesterdaysOrderCount);
        public decimal SalesChangePercentage => CalculatePercentage(TodaysProductsSold, YesterdaysProductsSold);

        private decimal CalculatePercentage(decimal current, decimal previous)
        {
            if (previous == 0) return current > 0 ? 100 : 0;
            return ((current - previous) / previous) * 100;
        }
    }
}