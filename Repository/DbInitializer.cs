namespace Repository
{
    public static class DbInitializer
    {
        public static void Initialize(QueueContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
