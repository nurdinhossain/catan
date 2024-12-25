namespace Catan
{
    public enum Resource
    {
        Brick,
        Lumber,
        Ore,
        Grain,
        Wool
    }

    public enum Building
    {
        NoBuilding,
        City,
        Settlement
    }

    public enum Port
    {
        NoPort,
        BrickPort,
        LumberPort,
        OrePort,
        GrainPort,
        WoolPort,
        AnyPort
    }

    public class Coordinate
    {
        // port leading into vertex
        public Port Port { get; set; }

        // structure on this vertex
        public Building Building { get; set; }

        // resources and corresponding numbers joined at this coordinate
        public Resource[] Resources { get; set; }
        public int[] Numbers { get; set; }

        // constructor
        public Coordinate(Port port, Building building, Resource[] resources, int[] numbers)
        {
            Port = port; 
            Building = building;
            Resources = resources;
            Numbers = numbers;
        }
    }
}
