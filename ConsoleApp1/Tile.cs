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
        private Player[] _players; 

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
            _players = new Player[Enum.GetNames(typeof(Vertex)).Length];
            _ports = new Port[Enum.GetNames(typeof(Vertex)).Length];
            _roads = new Road[Enum.GetNames(typeof(Side)).Length];
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

        public Player PlayerAt(Vertex vertex)
        {
            return _players[(int)vertex];
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

        public void SetPlayerAt(Vertex vertex, Player player)
        {
            _players[(int)vertex] = player;
        }

        public void SetRoadAt(Vertex vertex, Road road)
        {
            _roads[(int)vertex] = road;
        }

        // method for harvesting from dice roll
        public bool CanHarvest(int roll)
        {
            return roll == _number;
        }

        // method for harvesting resources
        public void Harvest()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                // harvest 1 resource if settlement
                if (_buildings[i] == Building.Settlement)
                {
                    _players[i].AddResource(_resource, 1);
                }

                // harvest 2 resources if city
                else if (_buildings[i] == Building.City)
                {
                    _players[i].AddResource(_resource, 2);
                }
            }
        }
    }
}
