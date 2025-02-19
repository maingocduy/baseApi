namespace BaseApi.Models.Request
{
    public class FindUserPageRequest : BaseKeywordPageRequest
    {
        public sbyte? Status { get; set; }


        public sbyte? IsHaveAcc {  get; set; }
    }
}
