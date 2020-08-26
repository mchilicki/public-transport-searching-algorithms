namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class ConnectionStrings
    {
        public string CurrentDatabase
        {
            get
            {
                return DatabaseType switch
                {
                    DatabaseType.Poznan => PoznanDatabase,
                    DatabaseType.Lahti => LahtiDatabase,
                    DatabaseType.Stavanger => StavangerDatabase,
                    _ => string.Empty,
                };
            }
        }

        public string PoznanDatabase { get; set; }
        public string LahtiDatabase { get; set; }
        public string StavangerDatabase { get; set; }
        public DatabaseType DatabaseType { get; set; }
    }
}
