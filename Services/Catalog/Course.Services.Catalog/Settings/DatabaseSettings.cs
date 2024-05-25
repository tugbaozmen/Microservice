namespace Course.Services.Catalog.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CourseCollectionName { get ; set ; }
        public string CategoryCollectionNmae { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get ; set; }
    }
}
