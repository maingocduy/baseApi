using System.ComponentModel;

namespace BaseApi.Enums
{
    public class EnumDatabase
    {
        public enum edAccountType
        {
            USER = 1,
            IDOL,
            ADMIN,
            MANAGER,
        }

        public enum edAccountState
        {
            [Description("Bị khóa")]
            LOCK = 0,
            [Description("Đang hoạt động")]
            ACTIVE,
        }

        public enum edIsEnable
        {
            [Description("Đã xóa")]
            FALSE,
            [Description("Đang tồn tại")]
            TRUE,
        }

        public enum edActionAuditType
        {
            INSERT = 1,
            UPDATE,
            DELETE
        }
    }
}
