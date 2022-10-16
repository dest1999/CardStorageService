namespace ConsSecureSettings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cs1 = new ConnectionString()
            {
                DBName = "dbname1",
                UserName = "un1",
                Host = "host1",
                Password = "pass1"
            };

            var cs2 = new ConnectionString()
            {
                DBName = "dbname2",
                UserName = "un2",
                Host = "host2",
                Password = "pass2"
            };

            var strings = new List<ConnectionString>() { cs1, cs2 };

            var cacheProvider = new CacheProvider();

            cacheProvider.CaheConnections(strings);
            var newStrings = cacheProvider.GetConnectionsFromCache();

            foreach (var item in newStrings)
            {
                Console.WriteLine(item.DBName);
                Console.WriteLine(item.Host);
                Console.WriteLine(item.UserName);
                Console.WriteLine(item.Password);
            }

            Console.ReadKey();
        }
    }
}