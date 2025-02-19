namespace BaseApi.Models.DataInfo
{
    public class LogInResp
    {
        public string Token { get; set; }
        public string Uuid { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserUuid { get; set; } = null!;
        public string Fullname { get; set; } = null!;

        public string? RolesUuid { get; set; }
    }
}
