namespace DepartmentAPI.Requests
{
    public record InfrastructureRequest(String OfficeLocation, int NumberOfDesks, int NumberOfComputers, bool HasMeetingRooms, int DepartmentId)
    {

        public String OfficeLocation { get; set; } = OfficeLocation;
        public int NumberOfDesks { get; set; } = NumberOfDesks;
        public int NumberOfComputers { get; set; } = NumberOfComputers;
        public bool HasMeetingRooms { get; set; } = HasMeetingRooms;
        public int DepartmentId { get; set; } = DepartmentId;
        public override string ToString()
        {
            return $"Office Location: {OfficeLocation}, Desks: {NumberOfDesks}, Computers: {NumberOfComputers}, Meeting Rooms: {HasMeetingRooms}, Department ID: {DepartmentId}";
        }
    }
}
