using System.Numerics;

namespace Catan
{
    public class Player
    {
        // game board played by this player
        private Game _game;

        // resource cards held by each player
        private int[] _resources;

        // development cards held by each player
        private bool _devCardDrawn; // ** field ensuring we can only use 1 dev card per turn **
        private List<DevelopmentCard> _devCardsTemp; // ** where dev cards go on current turn **
        private int[] _devCards; // ** where dev cards are transferred on subsequent turns **

        // ports controlled by each player
        private bool[] _ports;

        // random variable for rolling dice
        private static Random _rand = new Random();

        // general flags preventing us from taking further actions
        private bool _diceRolled;
        private bool _robberActivatedFromDice;
        private bool _robberHasBeenMoved;
        private bool _mustDiscardExcessResources;

        // dev card flags 
        private bool _roadBuildingPlayed;
        private bool _yearOfPlentyPlayed;
        private bool _monopolyPlayed;

        // dev card values
        private int _devRoadsAvailable;
        private int _plentyResourcesAvailable; 

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
        public Player(Game game, int id)
        {
            // add game
            _game = game;
            _game.AddPlayer(this);

            ID = id;
            _resources = new int[Enum.GetNames(typeof(Resource)).Length];
            _devCards = new int[Enum.GetNames(typeof(DevelopmentCard)).Length];
            _devCardsTemp = new List<DevelopmentCard>();
            _ports = new bool[Enum.GetNames(typeof(Port)).Length];

            // set all flags to false
            _diceRolled = false;
            _robberActivatedFromDice = false;
            _robberHasBeenMoved = false;
            _mustDiscardExcessResources = false;

            _roadBuildingPlayed = false;
            _yearOfPlentyPlayed = false;
            _monopolyPlayed = false;

            // set dev values to 0
            _devRoadsAvailable = 0;
            _plentyResourcesAvailable = 0;

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
        public int NumTempDevCards()
        {
            return _devCardsTemp.Count();
        }

        // ** mainly for testing **
        public int NumPermanentDevCards()
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

        // add a dev card to temp stack
        public void AddDevCard(DevelopmentCard card)
        {
            _devCardsTemp.Add(card);
        }

        // ** adding a port is permanent, so no need for a remove method **
        public void AddPort(Port port)
        {
            _ports[(int)port] = true;
        }

        // ** for testing purposes **
        public bool GetPort(Port port)
        {
            return _ports[(int)port];
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
        public bool BuildSettlement(int row, int col, Vertex vertex)
        {
            // ensure we have at least one settlement we can build
            if (Settlements < 1) return false; 

            // ensure we have sufficient resources to build settlement
            if (ResourceCount(Resource.Brick) < 1 || ResourceCount(Resource.Grain) < 1 || ResourceCount(Resource.Wool) < 1 || ResourceCount(Resource.Lumber) < 1) return false;

            // ensure building can be built
            if (!_game.CanBuildSettlementAt(this, row, col, vertex)) return false;

            // drain resources
            Bank bank = _game.GetBank();
            bank.Deposit(this, Resource.Brick, 1);
            bank.Deposit(this, Resource.Grain, 1);
            bank.Deposit(this, Resource.Wool, 1);
            bank.Deposit(this, Resource.Lumber, 1);

            // build
            Settlements--;
            _game.BuildBuilding(this, Building.Settlement, row, col, vertex);
            VictoryPoints++;

            return true; 
        }

        public bool BuildCity(int row, int col, Vertex vertex)
        {
            // ensure we have at least one city we can build
            if (Cities < 1) return false;

            // ensure we have sufficient resources to build settlement
            if (ResourceCount(Resource.Grain) < 2 || ResourceCount(Resource.Ore) < 3) return false;

            // ensure city can be built
            if (!_game.CanBuildCityAt(this, row, col, vertex)) return false;

            // drain resources
            Bank bank = _game.GetBank();
            bank.Deposit(this, Resource.Grain, 2);
            bank.Deposit(this, Resource.Ore, 3);

            // build
            Cities--;
            Settlements++;
            _game.BuildBuilding(this, Building.City, row, col, vertex);
            VictoryPoints++;

            return true; 
        }

        public bool BuildRoad(int row, int col, Side side)
        {
            // ensure we have at least one road we can build
            if (Roads < 1) return false;

            // ensure we have sufficient resources to build road
            if (ResourceCount(Resource.Lumber) < 1 || ResourceCount(Resource.Brick) < 1) return false;

            // ensure road can be built
            if (!_game.CanBuildRoadAt(this, row, col, side)) return false;

            // drain resources
            Bank bank = _game.GetBank();
            bank.Deposit(this, Resource.Lumber, 1);
            bank.Deposit(this, Resource.Brick, 1);

            // build
            Roads--;
            _game.BuildRoad(this, row, col, side);

            return true; 
        }

        // draw dev card from deck
        public bool DrawDevCard()
        {
            return _game.DrawDevCard(this);
        }

        // methods for playing dev cards
        public bool PlayDevCard(DevelopmentCard card)
        {
            // if dev card has already been drawn this turn, we cannot play another one
            if (_devCardDrawn) return false;

            // if we do not have any of this card, it is unplayable
            if (_devCards[(int)card] == 0) return false;

            // if dev card is a VP, it is unplayable
            if (card == DevelopmentCard.VictoryPoint) return false;

            // if 7 was rolled and robber has not been moved, we cannot play dev card
            if (_robberActivatedFromDice && !_robberHasBeenMoved) return false; 

            return true;

            if (!_devCardDrawn)
            {
                _devCardDrawn = true;
            }
        }

        // Change state upon ending turn given there are no prohibiting statuses activated
        public void EndTurn()
        {
            // move temp cards into permanent storage
            while (_devCardsTemp.Count() > 0)
            {
                DevelopmentCard card = _devCardsTemp.Last();
                _devCards[(int)card]++;
                _devCardsTemp.RemoveAt(_devCardsTemp.Count() - 1);
            }

            // reset dev card lock
            _devCardDrawn = false;
        }

        // roll dice
        public int RollDice()
        {
            int diceOne = _rand.Next(1, 7);
            int diceTwo = _rand.Next(1, 7);
            return diceOne + diceTwo;
        }

        // trade cards with the bank
        public bool ExchangeWithBank(int[] toGive, int[] toGet)
        {
            // get bank from game 
            Bank bank = _game.GetBank();

            // arrays must be correct length
            if (toGive.Length != Enum.GetNames(typeof(Resource)).Length || toGet.Length != Enum.GetNames(typeof(Resource)).Length) return false;

            // first element of both arrays must be 0
            if (toGive[0] != 0 || toGet[0] != 0) return false;

            // cannot trade nothing
            if (toGive.Sum() == 0 || toGet.Sum() == 0) return false; 

            // ensure we/bank have sufficient resources
            for (int i = 0; i < toGive.Length; i++)
            {
                if (toGive[i] > ResourceCount((Resource)i)) return false;
                if (toGet[i] > bank.ResourceCount((Resource)i)) return false;
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
                bank.Deposit(this, (Resource)i, toGive[i]);
                bank.Withdraw(this, (Resource)i, toGet[i]);
            }

            return true;
        }
    }
}
