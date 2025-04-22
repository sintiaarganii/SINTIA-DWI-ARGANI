using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class CashierAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string salt { get; set; }
        public string pwd_hash { get; set; }
        public DateTime AccessDate { get; set; }
        public GeneralStatusData Status { get; set; }
    }
}
