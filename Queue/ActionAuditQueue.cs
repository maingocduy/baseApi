namespace ESDManagerApi.Queue
{
    public class ActionAuditQueue
    {
        public string UserName { get; set; } = null!;

        public string Method { get; set; } = null!;

        /// <summary>
        /// 1: Insert - 2: Update - 3: Delete
        /// </summary>
        public sbyte ActionId { get; set; }

        public string Request { get; set; } = null!;
        public sbyte State { get; set; } = 1;
    }
}
