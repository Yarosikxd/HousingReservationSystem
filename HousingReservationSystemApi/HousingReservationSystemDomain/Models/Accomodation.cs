namespace HousingReservationSystemDomain.Models
{
    public class Accommodation
    {
        private Accommodation(Guid id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Location { get; private set; }

        public static Accommodation Create(Guid id, string name, string location)
        {
            return new Accommodation(id, name, location);
        }
    }
}
