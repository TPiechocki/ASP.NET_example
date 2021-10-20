namespace Example.WebApi.Contract
{
    public class Satellite
    {
        public int Altitude { get; private set; }

        public string Country { get; private set; }

        public string Name { get; private set; }

        public int SatelliteId { get; private set; }
    }

    public class SatelliteObsolete
    {
        public string Country { get; private set; }

        public string Name { get; private set; }

        public int SatelliteId { get; private set; }
    }
}