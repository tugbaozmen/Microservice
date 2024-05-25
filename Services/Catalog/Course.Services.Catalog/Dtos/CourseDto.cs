namespace Course.Services.Catalog.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
