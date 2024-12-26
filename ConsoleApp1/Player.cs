namespace Catan
{
    public enum DevelopmentCard
    {
        Knight,
        RoadBuilding,
        YearOfPlenty,
        Monopoly,
        VictoryPoint
    }

    public class Player
    {
        // resource cards held by each player
        private int[] _resources;

        // development cards held by each player
        private int[] _devCards;

        // unique identifier for each player
        public int ID { get; set; }

        // number of structures
        public int Settlements { get; set; }
        public int Cities { get; set; }
        public int Roads { get; set; }

        // army size
        public int Army { get; set; }

        // longest road size
        public int LongestRoad { get; set; }

        // victory points
        public int VictoryPoints { get; set; }

        // basic constructor
        public Player(int id)
        {
            ID = id;
            _resources = new int[Enum.GetNames(typeof(Resource)).Length];
            _devCards = new int[Enum.GetNames(typeof(DevelopmentCard)).Length];

            // default quantities for structures
            Settlements = 5;
            Cities = 4;
            Roads = 15;

            Army = 0;
            LongestRoad = 0;

            VictoryPoints = 0;
        }
        // some info methods
        public int HandSize()
        {
            return _resources.Sum();
        }

        public int ResourceCount(Resource resource)
        {
            return _resources[(int)resource];
        }

        // some adders/removers
        public void AddResource(Resource resource, int count)
        {
            _resources[(int)resource] += count;
        }

        public void RemoveResource(Resource resource, int count)
        {
            _resources[(int)resource] -= count;
        }

        public void AddDevelopmentCard(DevelopmentCard dev)
        {
            _devCards[(int)dev]++;
        }

        public void RemoveDevelopmentCard(DevelopmentCard dev)
        {
            _devCards[(int)dev]--;
        }

        // player interaction
        public bool TradeWithPlayer(Player other, int[] toGive, int[] toGet)
        {
            // cannot trade empty hands
            if (toGive.Sum() == 0 || toGet.Sum() == 0) return false;

            // arrays must be correct length
            if (toGive.Length != Enum.GetNames(typeof(Resource)).Length || toGet.Length != Enum.GetNames(typeof(Resource)).Length) return false;

            // cannot trade like resources
            int otherBenefit = 0;
            int myBenefit = 0; 
            for (int i = 0; i < toGive.Length; i++)
            {
                if (toGive[i] >= toGet[i]) otherBenefit++;
                if (toGive[i] <= toGet[i]) myBenefit++;
            }
            if (otherBenefit == toGive.Length || myBenefit == toGive.Length) return false;

            // cannot trade resources with insufficient amounts
            for (int i = 0; i < toGive.Length; i++)
            {
                if (ResourceCount((Resource)i) < toGive[i]) return false;
                if (other.ResourceCount((Resource)i) < toGet[i]) return false; 
            }

            // successful trade 
            for (int i = 0; i < toGive.Length; i++)
            {
                // I give and take
                RemoveResource((Resource)i, toGive[i]);
                AddResource((Resource)i, toGet[i]);

                // other player gives and takes
                other.RemoveResource((Resource)i, toGet[i]);
                other.AddResource((Resource)i, toGive[i]);
            }

            return true; 
        }

        public void RobPlayer(Player other)
        {
            if (other.HandSize() > 0)
            {
                // pick resource from other player's hand randomly 
                Random rand = new Random();
                int index = rand.Next(other.HandSize());
                int i;
                for (i = 0; i < other._resources.Length; i++)
                {
                    if (other._resources[i] == 0)
                        continue;

                    index -= other._resources[i];
                    if (index <= 0)
                    {
                        break;
                    }
                }
                Resource chosenResource = (Resource)i;

                // take resource
                other.RemoveResource(chosenResource, 1);
                AddResource(chosenResource, 1);
            }
            else
            {
                Console.WriteLine("Silly, this player has no cards!");
            }
        }

        // building
        public bool BuildSettlement(Coordinate coordinate)
        {
            // ensure we have at least one settlement we can build
            if (Settlements < 1) return false; 

            // ensure coordinate does not have pre-existing building
            if (coordinate.Building != Building.NoBuilding) return false;

            // ensure we have sufficient resources to build settlement
            if (ResourceCount(Resource.Brick) < 1 || ResourceCount(Resource.Grain) < 1 || ResourceCount(Resource.Wool) < 1 || ResourceCount(Resource.Lumber) < 1) return false;

            // execute construction
            RemoveResource(Resource.Brick, 1);
            RemoveResource(Resource.Grain, 1);
            RemoveResource(Resource.Wool, 1);
            RemoveResource(Resource.Lumber, 1);
            Settlements--;
            coordinate.Building = Building.Settlement;
            VictoryPoints++;

            return true; 
        }

        public bool BuildCity(Coordinate coordinate)
        {
            // ensure we have at least one city we can build
            if (Cities < 1) return false; 

            // ensure coordinate has pre-existing settlement
            if (coordinate.Building != Building.Settlement) return false;

            // ensure we have sufficient resources to build settlement
            if (ResourceCount(Resource.Grain) < 2 || ResourceCount(Resource.Ore) < 3) return false;

            // execute construction
            RemoveResource(Resource.Grain, 2);
            RemoveResource(Resource.Ore, 3);
            Cities--;
            Settlements++;
            coordinate.Building = Building.City;
            VictoryPoints++;

            return true; 
        }
    }
}
