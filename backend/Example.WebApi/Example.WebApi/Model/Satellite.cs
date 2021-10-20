namespace Example.WebApi.Model
{
    internal class Satellite
    {
        public Satellite(int altitude, string country, string name)
        {
            Altitude = altitude;
            Country = country;
            Name = name;
        }

        public int Altitude { get; private set; }

        public string Country { get; private set; }

        public string Name { get; private set; }

        public int SatelliteId { get; private set; }
    }
}