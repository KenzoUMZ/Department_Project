namespace DepartmentAPI.Requests
{
    public record ProjectRequest(string Title, string Description, DateTime StartDate, DateTime EndDate)
    {
        public string Title { get; set; } = Title;
        public string Description { get; set; } = Description;
        public DateTime StartDate { get; set; } = StartDate;
        public DateTime EndDate { get; set; } = EndDate;
    }

}
