namespace BaseApi.Models.Request
{
    public class GetCategoryUserRequest : BaseGetCategoryRequest
    {
        public string? RoleUuid { get; set; }

        public sbyte? Type { get; set; }
    }
}
