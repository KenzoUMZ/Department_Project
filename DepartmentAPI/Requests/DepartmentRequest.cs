using Department.Shared.Model;

namespace DepartmentAPI.Requests
{
    public record DepartmentRequest(int Id, string Name, string Location)
    {
        public int Id { get; set; } = Id;
        public string Name { get; set; } = Name;
        public string Location { get; set; } = Location;
        public Infrastructure Infrastructure { get; set; } = new Infrastructure(officeLocation: "Default Location", numberOfDesks: 10, numberOfComputers: 5, hasMeetingRooms: true);
    }

}
