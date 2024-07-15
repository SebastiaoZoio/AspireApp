namespace AspireApp.ApiService.Utils
{
    public class FilterDescriptor
    {
        public string? PropertyName { get; set; }
        public string? Operator { get; set; } // Example: "Contains", "Equals", etc.
        public object? Value { get; set; }
    }
}
