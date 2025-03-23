namespace Catan
{
    public enum Vertex
    {
        Top,
        TopRight,
        BottomRight,
        Bottom,
        BottomLeft,
        TopLeft
    }

    public enum Side
    {
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
        Left,
        TopLeft
    }

    public enum Road
    {
        NoRoad,
        Road
    }

    public enum Resource
    {
        NoResource,
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

    public class Tile
    {
        // resource granted by this tile
        private Resource _resource;
        public Resource Resource { get => _resource; }

        // dice roll sum needed to harvest this tile's resource
        private int _number;
        public int Number { get => _number; }

        // buildings on vertices
        private Building[] _buildings;

        // players controlling each building
        private Player?[] _vertexPlayers;

        // players controlling each road
        private Player?[] _sidePlayers; 

        // ports on vertices
        private Port[] _ports;

        // roads on sides
        private Road[] _roads;

        // constructor
        public Tile(Resource resource, int number)
        {
            _resource = resource;
            _number = number;

            _buildings = new Building[Enum.GetNames(typeof(Vertex)).Length];
            _vertexPlayers = new Player[Enum.GetNames(typeof(Vertex)).Length];
            _sidePlayers = new Player[Enum.GetNames(typeof(Side)).Length];
            _ports = new Port[Enum.GetNames(typeof(Vertex)).Length];
            _roads = new Road[Enum.GetNames(typeof(Side)).Length];
        }

        // basic setters and getters
        public void SetResource(Resource r)
        {
            _resource = r;
        }

        public void SetNumber(int n)
        {
            _number = n;
        }

        // array getters
        public Building BuildingAt(Vertex vertex)
        {
            return _buildings[(int)vertex];
        }

        public Port PortAt(Vertex vertex)
        {
            return _ports[(int)vertex];
        }

        public Player PlayerAtVertex(Vertex vertex)
        {
            return _vertexPlayers[(int)vertex];
        }

        public Player PlayerAtSide(Side side)
        {
            return _sidePlayers[(int)side];
        }

        public Road RoadAt(Side side)
        {
            return _roads[(int)side];
        }

        // array setters
        public void SetBuildingAt(Vertex vertex, Building building)
        {
            _buildings[(int)vertex] = building;
        }

        public void SetPortAt(Vertex vertex, Port port)
        {
            _ports[(int)vertex] = port;
        }

        public void SetPlayerAtVertex(Vertex vertex, Player player)
        {
            _vertexPlayers[(int)vertex] = player;
        }

        public void SetPlayerAtSide(Side side, Player player)
        {
            _sidePlayers[(int)side] = player;
        }

        public void SetRoadAt(Side side, Road road)
        {
            _roads[(int)side] = road;
        }

        // method for harvesting from dice roll
        public bool CanHarvest(int roll)
        {
            return roll == _number;
        }
    }
}
