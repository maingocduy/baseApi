namespace ESDManagerApi.Queue
{
    public class ActionQueueManager
    {
        private static readonly List<ActionAuditQueue> QUEUE_MANAGER = new List<ActionAuditQueue>();
        private static SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        public static List<ActionAuditQueue>? dequeue()
        {
            mutex.Wait();

            try
            {
                if (QUEUE_MANAGER.Count > 0)
                {
                    var result = new List<ActionAuditQueue>();

                    while (QUEUE_MANAGER.Count > 0)
                    {
                        var lastItem = QUEUE_MANAGER[0];

                        QUEUE_MANAGER.Remove(lastItem);

                        result.Add(lastItem);
                    }

                    return result;
                }
            }
            finally
            {
                mutex.Release();
            }

            return null;
        }

        public static void enqueue(ActionAuditQueue message)
        {
            mutex.Wait();

            try
            {
                QUEUE_MANAGER.Add(message);
            }
            finally
            {
                mutex.Release();
            }
        }
    }
}
