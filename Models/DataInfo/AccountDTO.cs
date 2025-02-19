using BaseApi.Models.DataInfo;

namespace BaseApi.Models.DataInfo
{
    public class CategoryAccountDTO : BaseDTO
    {
        public string UserName { get; set; } = null!;
    }
    public class AccountDTO : CategoryAccountDTO
    {
        public InfoCatalogDTO? Role { get; set; }

        public DetailUserDTO? User { get; set; }

        public DateTime? Updated { get; set; }

        /// <summary>
        /// 0-không sử dụng , 1- sử dụng , 2-khóa
        /// </summary>
        public sbyte Status { get; set; }
        public DateTime Created { get; set; }
    }
    public class DetailAccountDTO : AccountDTO
    {
        public DateTime? Updated { get; set; }
    }
}
