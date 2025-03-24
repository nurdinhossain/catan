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
        private bool _devCardPlayed; // ** field ensuring we can only use 1 dev card per turn **
        private List<DevelopmentCard> _devCardsTemp; // ** where dev cards go on current turn **
        private int[] _devCards; // ** where dev cards are transferred on subsequent turns **

        // ports controlled by each player
        private bool[] _ports;

        // random variable for rolling dice
        private static Random _rand = new Random();

        // general flags preventing us from taking further actions
        private bool _diceRolled = true; // ** default value set to true for testing **
        private bool _robberActivated = false;
        private bool _mustRobPlayer = false;
        private bool _mustDiscardExcessResources = false;

        // dev card flags 
        private bool _monopolyPlayed = false;

        // dev card values
        private int _devRoadsAvailable = 0;
        private int _plentyResourcesAvailable = 0;

        // initial placement
        private (int row, int col, Vertex vertex) _firstSettlement;
        private (int row, int col, Side side) _firstRoad; 
        private (int row, int col, Vertex vertex) _secondSettlement;
        private (int row, int col, Side side) _secondRoad;

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

            // initial placement
            _firstSettlement = (-1, -1, Vertex.Top);
            _firstRoad = (-1, -1, Side.TopRight);
            _secondSettlement = (-1, -1, Vertex.Top);
            _secondRoad = (-1, -1, Side.TopRight);

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

        public void AddPermanentDevCard(DevelopmentCard card)
        {
            _devCards[(int)card]++;
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

        public bool MustDiscard()
        {
            return _mustDiscardExcessResources;
        }

        public void SetDiscard(bool discard)
        {
            _mustDiscardExcessResources = discard;
        }

        public bool NormalActionsStalled()
        {
            bool notTurn = _game.GetTurn() != this;
            bool diceNotRolled = !_diceRolled;
            bool mustDiscard = _game.PlayersMustDiscard();
            bool robberShenanigans = _robberActivated || _mustRobPlayer;
            bool devCardStall = _devRoadsAvailable > 0 || _plentyResourcesAvailable > 0 || _monopolyPlayed;
            return notTurn || diceNotRolled || mustDiscard || robberShenanigans || devCardStall;
        }

        // player interaction
        public bool TradeWithPlayer(Player other, int[] toGive, int[] toGet)
        {
            if (NormalActionsStalled()) return false;

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
            if (NormalActionsStalled()) return false;

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
            if (NormalActionsStalled()) return false;

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
            if (NormalActionsStalled()) return false;

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
            if (NormalActionsStalled()) return false;

            // get bank and dev deck
            Bank bank = _game.GetBank();
            DevDeck devDeck = _game.GetDevDeck();

            // ensure we aren't drawing from an empty deck 
            if (devDeck.CardsRemaining() == 0) return false;

            // ensure we have sufficient resources
            if (ResourceCount(Resource.Wool) < 1 || ResourceCount(Resource.Grain) < 1 || ResourceCount(Resource.Ore) < 1) return false;

            // add resources to bank
            bank.Deposit(this, Resource.Wool, 1);
            bank.Deposit(this, Resource.Grain, 1);
            bank.Deposit(this, Resource.Ore, 1);

            // draw from deck
            DevelopmentCard card = devDeck.Draw();

            // if card is a VP, add it to VPs
            if (card == DevelopmentCard.VictoryPoint) VictoryPoints++;

            // add to temp storage since it is unusable at the moment 
            _devCardsTemp.Add(card);
            return true;
        }

        // methods for playing dev cards
        public bool PlayDevCard(DevelopmentCard card)
        {
            // if its not our turn, we can't play dev card
            if (_game.GetTurn() != this) return false; 

            // if players need to discard, we cannot play dev card just yet
            if (_game.PlayersMustDiscard()) return false;

            // if we've rolled a 7 and haven't moved the knight yet, we cannot play the dev card
            if (_robberActivated) return false;

            // if we've moved the knight but haven't robbed anyone, we cannot play the dev card
            if (_mustRobPlayer) return false; 

            // if dev card has already been played this turn, we cannot play another one
            if (_devCardPlayed) return false;

            // if we do not have any of this card, it is unplayable
            if (_devCards[(int)card] == 0) return false;

            // if dev card is a VP, it is unplayable
            if (card == DevelopmentCard.VictoryPoint) return false;

            // play dev card
            _devCardPlayed = true;
            _devCards[(int)card]--;
            switch (card)
            {
                case DevelopmentCard.Knight:
                    _robberActivated = true;
                    Army++;

                    // largest army
                    Player? currentLargestArmy = _game.GetPlayerWithLargestArmy();
                    
                    // if no one has taken largest army and we are the first to reach 3 knights, we get largest army card
                    if (currentLargestArmy == null)
                    {
                        if (Army == 3)
                        {
                            VictoryPoints += 2;
                            _game.SetLargestArmyPlayer(this);
                        }
                    }

                    // otherwise, if our army is greater than the current largest army, take the card from them
                    else if (Army > currentLargestArmy.Army)
                    {
                        VictoryPoints += 2;
                        currentLargestArmy.VictoryPoints -= 2;
                        _game.SetLargestArmyPlayer(this);
                    }

                    break;
                case DevelopmentCard.RoadBuilding:
                    // players should strive to build 2 roads if possible
                    // if they only have 1 road left, that sets an upper bound
                    // if they only have 1 valid spot to build a road, that sets an upper bound
                    _devRoadsAvailable = Math.Min(2, _game.EligibleRoadSpots(this));
                    _devRoadsAvailable = Math.Min(_devRoadsAvailable, Roads);
                    break;
                case DevelopmentCard.YearOfPlenty:
                    // players should strive to harvest 2 resources if possible
                    // the quantity of resources left in the bank sets the upper bound of this card if its less than 2
                    _plentyResourcesAvailable = Math.Min(2, _game.GetBank().TotalResources());
                    break;
                case DevelopmentCard.Monopoly:
                    _monopolyPlayed = true;
                    break;
            }

            return true;
        }

        public bool MoveRobber(int row, int col)
        {
            // if robber hasn't been activated, we can't move it
            if (!_robberActivated) return false;

            // if players must discard first, we can't move robber
            if (_game.PlayersMustDiscard()) return false;

            bool result = _game.MoveRobber(row, col);

            if (result)
            {
                // no longer need to move robber
                _robberActivated = false;

                // only set _mustRobPlayer to true if there is more than one player to rob that isn't us with a hand size greater than 0
                Tile robberTile = _game.GetRobberTile();
                int playersToRob = 0;
                Player lastPlayer = this; // a little hack to make the compiler happy 
                for (int i = 0; i < Enum.GetNames(typeof(Vertex)).Length; i++)
                {
                    Player? playerAtVertex = robberTile.PlayerAtVertex((Vertex)i);
                    if (playerAtVertex != null && playerAtVertex != this && playerAtVertex.HandSize() > 0)
                    {
                        playersToRob++; // could make an optimization to break once playersToRob > 1, but that seems unnecessary
                        lastPlayer = playerAtVertex;
                    }
                }

                // if playersToRob is greater than 0, set _mustRobPlayer to true because we must choose who to rob
                if (playersToRob > 0) _mustRobPlayer = true;

                // if playersToRob is equal to 1, rob the last player because they are the only option
                if (playersToRob == 1)
                {
                    ChoosePlayerToRob(lastPlayer);
                }
            }

            return result;
        }

        public bool ChoosePlayerToRob(Player player)
        {
            // if _mustRobPlayer is false, return false
            if (!_mustRobPlayer) return false;

            // if the player we are trying to rob is ourselves, return false
            if (player == this) return false;

            // if player is not on this tile, return false
            bool tileHasPlayer = false;
            Tile robberTile = _game.GetRobberTile();
            for (int i = 0; i < Enum.GetNames(typeof(Vertex)).Length; i++)
            {
                if (robberTile.PlayerAtVertex((Vertex)i) == player)
                {
                    tileHasPlayer = true;
                    break;
                }
            }
            if (!tileHasPlayer) return false;

            // if all conditions are met, rob player and set _mustRob to false
            RobPlayer(player);
            _mustRobPlayer = false;

            return true; 
        }

        public bool BuildDevRoad(int row, int col, Side side)
        {
            // if we have no dev roads to build, return false
            if (_devRoadsAvailable == 0) return false;

            // if we can't build a road at the requested location, return false
            if (!_game.CanBuildRoadAt(this, row, col, side)) return false;

            // otherwise, build the road
            _game.BuildRoad(this, row, col, side);
            _devRoadsAvailable--;
            Roads--;

            return true;
        }

        public bool TakeFreeResource(Resource resource)
        {
            // if we have no year of plenty resources to take, return false
            if (_plentyResourcesAvailable == 0) return false;

            // if bank does not have requested resource, return false
            Bank bank = _game.GetBank();
            if (bank.ResourceCount(resource) == 0) return false;

            // otherwise, take resource
            bank.Withdraw(this, resource, 1);
            _plentyResourcesAvailable--;

            return true;
        }

        public bool MonopolizeResource(Resource resource)
        {
            // if monopoly card has not been played, this method is invalid
            if (!_monopolyPlayed) return false;

            // otherwise, take all of this resource from every player
            int collectedResources = _game.CollectResourceFromAllPlayers(resource);
            AddResource(resource, collectedResources);
            _monopolyPlayed = false; 

            return true;
        }

        // discard excess cards if needed
        public bool Discard(int[] toDiscard)
        {
            // if the discard flag is not set, we are not allowed to discard anything
            if (!_mustDiscardExcessResources) return false;

            // if length of toDiscard is wrong, return false
            if (toDiscard.Length != Enum.GetNames(typeof(Resource)).Length) return false;

            // if NoResource != 0, return false 
            if (toDiscard[0] != 0) return false;

            // if sum of discarded cards is not equal to half of hand size rounded down, return false
            if (toDiscard.Sum() != (HandSize() / 2)) return false;

            // if any discarded resources exceed resources we have, return false
            for (int i = 1; i < toDiscard.Length; i++)
            {
                if (toDiscard[i] > ResourceCount((Resource)i)) return false;
            }

            // otherwise, discard resources
            for (int i = 1; i < toDiscard.Length; i++)
            {
                _game.GetBank().Deposit(this, (Resource)i, toDiscard[i]);
            }

            // set discard flag to false
            _mustDiscardExcessResources = false;

            return true; 
        }

        // Change state upon ending turn given there are no prohibiting statuses activated
        public bool EndTurn()
        {
            // cannot end turn if any stalling events remain
            if (NormalActionsStalled()) return false; 

            // move temp cards into permanent storage
            while (_devCardsTemp.Count() > 0)
            {
                DevelopmentCard card = _devCardsTemp.Last();
                _devCards[(int)card]++;
                _devCardsTemp.RemoveAt(_devCardsTemp.Count() - 1);
            }

            // un-roll the dice
            _diceRolled = false;

            // change turn within game
            _game.ChangeTurn();

            return true;
        }

        // roll dice
        public bool RollDice()
        {
            // if its not our turn, we can't roll dice
            if (_game.GetTurn() != this) return false;

            // if dice has already been rolled, we cannot roll it again
            if (_diceRolled) return false;

            // if dev card has been played, we cannot roll until the action has been fulfilled
            if (_robberActivated || _mustRobPlayer || _devRoadsAvailable > 0 || _plentyResourcesAvailable > 0 || _monopolyPlayed) return false; 

            int diceOne = _rand.Next(1, 7);
            int diceTwo = _rand.Next(1, 7);
            int sum = diceOne + diceTwo;

            // if sum is 7, activate robber
            if (sum == 7)
            {
                _robberActivated = true; 
            }

            // send roll to game
            _game.RespondToRoll(sum);

            // set _diceRolled to true
            _diceRolled = true;

            return true; 
        }

        // trade cards with the bank
        public bool ExchangeWithBank(int[] toGive, int[] toGet)
        {
            if (NormalActionsStalled()) return false; 

            // get bank from game 
            Bank bank = _game.GetBank();

            // arrays must be correct length
            if (toGive.Length != Enum.GetNames(typeof(Resource)).Length || toGet.Length != Enum.GetNames(typeof(Resource)).Length) return false;

            // first element of both arrays must be 0
            if (toGive[0] != 0 || toGet[0] != 0) return false;

            // cannot trade nothing
            if (toGive.Sum() == 0 || toGet.Sum() == 0) return false; 

            // ensure we/bank have sufficient resources
            for (int i = 1; i < toGive.Length; i++)
            {
                if (toGive[i] > ResourceCount((Resource)i)) return false;
                if (toGet[i] > bank.ResourceCount((Resource)i)) return false;
            }

            int totalRequested = toGet.Sum();
            int totalAvailable = 0;
            for (int i = 1; i < toGive.Length; i++)
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
            for (int i = 1; i < toGive.Length; i++)
            {
                bank.Deposit(this, (Resource)i, toGive[i]);
                bank.Withdraw(this, (Resource)i, toGet[i]);
            }

            return true;
        }

        // methods for initial road/settlement placement
        private void HarvestInitialResources()
        {
            Tile? topLeftNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.TopLeft);
            Tile? topRightNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.TopRight);
            Tile? rightNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.Right);
            Tile? bottomRightNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.BottomRight);
            Tile? bottomLeftNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.BottomLeft);
            Tile? leftNeighbor = _game.GetNeighbor(_secondSettlement.row, _secondSettlement.col, Side.Left);
            Bank bank = _game.GetBank();

            switch (_secondSettlement.vertex)
            {
                case Vertex.Top:
                    if (topLeftNeighbor != null)
                    {
                        bank.Withdraw(this, topLeftNeighbor.Resource, 1);
                    }

                    if (topRightNeighbor != null)
                    {
                        bank.Withdraw(this, topRightNeighbor.Resource, 1);
                    }
                    break;
                case Vertex.TopRight:
                    if (topRightNeighbor != null)
                    {
                        bank.Withdraw(this, topRightNeighbor.Resource, 1);
                    }
                    if (rightNeighbor != null)
                    {
                        bank.Withdraw(this, rightNeighbor.Resource, 1);
                    }
                    break;
                case Vertex.BottomRight:
                    if (rightNeighbor != null)
                    {
                        bank.Withdraw(this, rightNeighbor.Resource, 1);
                    }
                    if (bottomRightNeighbor != null)
                    {
                        bank.Withdraw(this, bottomRightNeighbor.Resource, 1);
                    }
                    break;
                case Vertex.Bottom:
                    if (bottomRightNeighbor != null)
                    {
                        bank.Withdraw(this, bottomRightNeighbor.Resource, 1);
                    }
                    if (bottomLeftNeighbor != null)
                    {
                        bank.Withdraw(this, bottomLeftNeighbor.Resource, 1);
                    }
                    break;
                case Vertex.BottomLeft:
                    if (bottomLeftNeighbor != null)
                    {
                        bank.Withdraw(this, bottomLeftNeighbor.Resource, 1);
                    }
                    if (leftNeighbor != null)
                    {
                        bank.Withdraw(this, leftNeighbor.Resource, 1);
                    }
                    break;
                case Vertex.TopLeft:
                    if (leftNeighbor != null)
                    {
                        bank.Withdraw(this, leftNeighbor.Resource, 1);
                    }
                    if (topLeftNeighbor != null)
                    {
                        bank.Withdraw(this, topLeftNeighbor.Resource, 1);
                    }
                    break;
            }

            bank.Withdraw(this, _game.TileAt(_secondSettlement.row, _secondSettlement.col).Resource, 1);
        }

        private bool EdgeNextToCorrectSettlement(int row, int col, Side side)
        {
            // vaid scenarios: placing first road next to first settlement and second road next to second settlement
            if (_firstRoad.row == -1)
            {
                return _game.CanBuildRoadAt(this, row, col, side);
            }
            else
            {
                // pseudo "place" road down
                _game.TileAt(row, col).SetRoadAt(side, Road.Road);
                _game.TileAt(row, col).SetPlayerAtSide(side, this);

                // see if roads meet at second settlement vertex
                bool valid = _game.RoadsMeetAtVertex(this, _secondSettlement.row, _secondSettlement.col, _secondSettlement.vertex);

                // pseudo "remove" road
                _game.TileAt(row, col).SetRoadAt(side, Road.NoRoad);
                _game.TileAt(row, col).SetPlayerAtSide(side, null);

                return valid; 
            }
        }

        public bool PlaceInitialBuilding(int row, int col, Vertex vertex)
        {
            // cannot execute this method if second initial building has already been placed
            if (_secondSettlement.row != -1) return false;

            // cannot execute this method if first building has been placed but no road has been placed
            if (_firstSettlement.row != -1 && _firstRoad.row == -1) return false;

            // cannot execute this method if it is not our turn
            if (_game.GetTurn() != this) return false;

            // method fails if we attempt to place building on null tile
            Tile? tile = _game.TileAt(row, col);
            if (tile == null) return false;

            // method fails if vertex already has building on it
            if (tile.BuildingAt(vertex) != Building.NoBuilding) return false;

            // method fails if vertex violates building -> building distance rule
            if (!_game.BuildingProximityValid(row, col, vertex)) return false;

            // build
            Settlements--;
            _game.BuildBuilding(this, Building.Settlement, row, col, vertex);
            VictoryPoints++;

            // if first building, then set _firstSettlement
            if (_firstSettlement.row == -1)
            {
                _firstSettlement.row = row;
                _firstSettlement.col = col;
                _firstSettlement.vertex = vertex;
            }

            // if second settlement, then set _secondSettlement and harvest resources
            else
            {
                _secondSettlement.row = row;
                _secondSettlement.col = col;
                _secondSettlement.vertex = vertex;
                HarvestInitialResources();
            }

            return true; 
        }

        public bool PlaceInitialRoad(int row, int col, Side side)
        {
            // cannot execute this method if second road has been placed
            if (_secondRoad.row != -1) return false;

            // cannot execute this method if first road has been placed but no second settlement
            if (_firstRoad.row != -1 && _secondSettlement.row == -1) return false;

            // cannot execute this method if no settlement has been placed period
            if (_firstSettlement.row == -1) return false;

            // cannot execute this method if it is not our turn 
            if (_game.GetTurn() != this) return false;

            // method fails if we attempt to build road on null tile
            Tile? tile = _game.TileAt(row, col);
            if (tile == null) return false;

            // method fails if road is not placed next to corresponding settlement
            if (!EdgeNextToCorrectSettlement(row, col, side)) return false;

            // build
            Roads--;
            _game.BuildRoad(this, row, col, side);

            // if first road, then set _firstRoad
            if (_firstRoad.row == -1)
            {
                _firstRoad.row = row;
                _firstRoad.col = col;
                _firstRoad.side = side;
            }

            // if second road, then set _secondRoad
            else
            {
                _secondRoad.row = row;
                _secondRoad.col = col;
                _secondRoad.side = side;
            }

            return true; 
        }
    }
}
