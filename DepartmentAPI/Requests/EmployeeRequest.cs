namespace DepartmentAPI.Requests
{
    public record EmployeeRequest(string Name, string Position, int EmployeeId, int ProjectId, int DepartmentId)
    {
        public string Name { get; set; } = Name;
        public string Position { get; set; } = Position;
        public int ProjectId { get; set; } = ProjectId;
        public int DepartmentId { get; set; } = DepartmentId;
    }

}
