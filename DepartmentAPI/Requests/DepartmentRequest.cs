namespace DepartmentAPI.Requests
{
    public record DepartmentRequest(string Name, string Location)
    {
        public string Name { get; set; } = Name;
        public string Location { get; set; } = Location;
    }

}
