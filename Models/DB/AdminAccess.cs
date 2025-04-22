using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class AdminAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string salt { get; set; }
        public string pwd_hash { get; set; }
        public GeneralStatusData AdminStatus { get; set; }
    }
}
