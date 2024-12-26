namespace Catan
{
    public class Player
    {
        // resource cards held by each player
        private int[] _resources;

        // development cards held by each player
        private int[] _devCards;

        // ports controlled by each player
        private bool[] _ports; 

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
            _ports = new bool[Enum.GetNames(typeof(Port)).Length];

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

        // ** mainly for testing **
        public int NumDevCards()
        {
            return _devCards.Sum();
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

        // ** adding a port is permanent, so no need for a remove method **
        public void AddPort(Port port)
        {
            _ports[(int)port] = true;
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

        // draw dev card from deck
        public bool DrawDevCard(DevDeck deck)
        {
            // ensure we aren't drawing from an empty deck 
            if (deck.CardsRemaining() == 0) return false;

            // ensure we have sufficient resources
            if (ResourceCount(Resource.Wool) < 1 || ResourceCount(Resource.Grain) < 1 || ResourceCount(Resource.Ore) < 1) return false;

            // consume resources 
            RemoveResource(Resource.Wool, 1);
            RemoveResource(Resource.Grain, 1);
            RemoveResource(Resource.Ore, 1);

            // draw from deck
            DevelopmentCard card = deck.Draw();

            // if card is a VP, add it to VPs
            if (card == DevelopmentCard.VictoryPoint) VictoryPoints++;

            _devCards[(int)card]++;
            return true;
        }

        // trade cards with the bank
        public bool ExchangeWithBank(int[] toGive, int[] toGet)
        {
            // arrays must be correct length
            if (toGive.Length != Enum.GetNames(typeof(Resource)).Length || toGet.Length != Enum.GetNames(typeof(Resource)).Length) return false;

            // ensure we have sufficient resources
            for (int i = 0; i < toGive.Length; i++)
            {
                if (toGive[i] > ResourceCount((Resource)i)) return false;
            }

            int totalRequested = toGet.Sum();
            int totalAvailable = 0;
            for (int i = 0; i < toGive.Length; i++)
            {
                // if we have 0 of this resource, continue
                if (toGive[i] == 0) continue;

                // if we have 1 of this resource, it cannot be exchanged
                if (toGive[i] == 1) return false;

                // if we have an odd number of this resource and no 3:1 port, it cannot be exchanged
                bool oddResources = toGive[i] % 2 == 1;
                bool hasThreeToOnePort = _ports[(int)Port.AnyPort];
                if (oddResources && !hasThreeToOnePort) return false; 

                // establish whether we have 2:1 resource-specific port or not
                bool hasTwoToOnePort = _ports[i];

                // if we have a 2:1 port, find the quotient at a ratio of 2
                if (hasTwoToOnePort)
                {
                    // if toGive[i] is odd, this means we MUST have a 3:1 port as well
                    totalAvailable += toGive[i] / 2;
                }

                // if we have a 3:1 port, find the quotient at a ratio of 3
                else if (hasThreeToOnePort)
                {
                    // if we have 2 or 5 resources, we cannot exchange
                    if (toGive[i] == 2 || toGive[i] == 5) return false;

                    totalAvailable += toGive[i] / 3;
                }

                // standard exchange
                else
                {
                    if (toGive[i] % 4 != 0) return false;
                    totalAvailable += toGive[i] / 4;
                }
            }

            // check if total valid exchanges == requested exchanges
            if (totalAvailable != totalRequested) return false; 

            // make requested exchanges
            for (int i = 0; i < toGive.Length; i++)
            {
                RemoveResource((Resource)i, toGive[i]);
                AddResource((Resource)i, toGet[i]);
            }

            return true;
        }
    }
}
