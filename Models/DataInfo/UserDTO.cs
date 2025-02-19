using System.Text.Json.Serialization;
using BaseApi.Databases.TM;

namespace BaseApi.Models.DataInfo
{
    public class CategoryUserDTO : BaseDTO
    {
        public string Fullname { get; set; }


        public string Code { get; set; }
    }
    public class UserDTO : CategoryUserDTO
    {
        public sbyte Gender { get; set; }

        public string Email { get; set; }
        public string? Phone { get; set; }

        public virtual InfoCatalogDTO? AccountUU { get; set; }

        public int IsHaveAcc
        {
            get
            {
                return AccountUU == null ? 0 : 1;
            }
        }
        public string? Address { get; set; }

        public sbyte Status { get; set; }

    }
    public class DetailUserDTO : UserDTO
    {
        
        public DateOnly? Birthday { get; set; }

        public virtual InfoCatalogDTO? QH { get; set; }

        public virtual InfoCatalogDTO? TP { get; set; }

        public virtual InfoCatalogDTO? Xa { get; set; }

        public string? Note { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime Created { get; set; }
    }
}
