﻿namespace Catan
{
    public class Game
    {
        // constants
        public const int MaxHandSize = 7; 

        // array of Tiles present in game board
        private Tile?[,] _tiles;

        // robber tile
        private Tile _robberTile;

        // list of players playing this game
        private List<Player> _players;

        // players holding title of longest road and largest army
        private Player? _longestRoadPlayer;
        private Player? _largestArmyPlayer;

        // bank and deck of development cards
        private readonly Bank _bank;
        private readonly DevDeck _devDeck;

        public Game(string fileName) 
        {
            // load map
            LoadMap(fileName);

            // initialize players list
            _players = new List<Player>();

            // init superlatives
            _longestRoadPlayer = null;
            _largestArmyPlayer = null;

            // initialize bank and dev card deck
            _bank = new Bank();
            _devDeck = new DevDeck();

            // if _tiles is null, raise exception
            if (_tiles == null) throw new Exception("_tiles was not properly initialized.");

            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                bool loopDone = false;
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    Tile? tile = _tiles[i, j];
                    if (tile != null)
                    {
                        if (tile.Resource == Resource.NoResource)
                        {
                            _robberTile = tile;
                            loopDone = true;
                            break;
                        }
                    }
                }

                if (loopDone) break; 
            }

            // if robber tile is still null, this game board is invalid
            if (_robberTile == null) throw new Exception("Game board does not contain desert tile.");
        }

        // getters/setters
        public Tile? TileAt(int row, int col)
        {
            return _tiles[row, col];
        }

        public Tile GetRobberTile()
        {
            return _robberTile;
        }

        public Bank GetBank()
        {
            return _bank;
        }

        public DevDeck GetDevDeck()
        {
            return _devDeck;
        }

        public Player? GetPlayerWithLongestRoad()
        {
            return _longestRoadPlayer;
        }

        public Player? GetPlayerWithLargestArmy()
        {
            return _largestArmyPlayer;
        }

        public void SetLongestRoadPlayer(Player p)
        {
            _longestRoadPlayer = p;
        }

        public void SetLargestArmyPlayer(Player p)
        {
            _largestArmyPlayer = p;
        }

        public (int, int) GetNeighborIndices(int row, int col, Side side)
        {
            if (side == Side.Left)
            {
                // if we're on the left side of the board, there's no left pieces
                if (col == 0) return (-1, -1);
                return (row, col - 1);
            }

            else if (side == Side.Right)
            {
                if (col == _tiles.GetLength(1) - 1) return (-1, -1);
                return (row, col + 1);
            }

            else if (side == Side.TopRight)
            {
                if (row == 0) return (-1, -1);
                if (col == _tiles.GetLength(1) - 1 && row % 2 == 1) return (-1, -1);
                if (row % 2 == 0) return (row - 1, col);

                return (row - 1, col + 1);
            }
            else if (side == Side.TopLeft)
            {
                if (row == 0) return (-1, -1);
                if (col == 0 && row % 2 == 0) return (-1, -1);
                if (row % 2 == 1) return (row - 1, col);

                return (row - 1, col - 1);
            }

            else if (side == Side.BottomRight)
            {
                if (row == _tiles.GetLength(0) - 1) return (-1, -1);
                if (col == _tiles.GetLength(1) - 1 && row % 2 == 1) return (-1, -1);
                if (row % 2 == 0) return (row + 1, col);

                return (row + 1, col + 1);
            }

            else if (side == Side.BottomLeft)
            {
                if (row == _tiles.GetLength(0) - 1) return (-1, -1);
                if (col == 0 && row % 2 == 0) return (-1, -1);
                if (row % 2 == 1) return (row + 1, col);

                return (row + 1, col - 1);
            }

            return (-1, -1);
        }

        public Tile? GetNeighbor(int row, int col, Side side)
        {
            (int, int) indices = GetNeighborIndices(row, col, side);
            if (indices.Item1 == -1) return null;
            return _tiles[indices.Item1, indices.Item2];
        }

        // add player to game (no player conditions/restrictions at the moment, will evaluate this later...)
        public bool AddPlayer(Player player)
        {
            if (_players.Contains(player)) return false;

            _players.Add(player);

            return true;
        }

        // check if tile borders a water tile
        public bool BordersWater(int row, int col, Side side)
        {
            return GetNeighbor(row, col, side) == null;
        }

        // check if any player must discard resources
        public bool PlayersMustDiscard()
        {
            foreach (Player player in _players)
            {
                if (player.MustDiscard()) return true; 
            }

            return false;
        }

        // take a particular resource type away from every player and return the total resources collected
        public int CollectResourceFromAllPlayers(Resource resource)
        {
            int total = 0;
            foreach (Player player in _players)
            {
                int amount = player.ResourceCount(resource);
                total += amount;
                player.RemoveResource(resource, amount);
            }

            return total;
        }

        // special methods for building 
        private bool RoadsMeetAtVertex(Player player, int row, int col, Vertex vertex)
        {
            // get tile (assumed not null)
            Tile? tile = _tiles[row, col];

            // check proximity to other structures
            bool hasRoad;
            Tile? topLeftNeighbor = GetNeighbor(row, col, Side.TopLeft);
            Tile? topRightNeighbor = GetNeighbor(row, col, Side.TopRight);
            Tile? rightNeighbor = GetNeighbor(row, col, Side.Right);
            Tile? bottomRightNeighbor = GetNeighbor(row, col, Side.BottomRight);
            Tile? bottomLeftNeighbor = GetNeighbor(row, col, Side.BottomLeft);
            Tile? leftNeighbor = GetNeighbor(row, col, Side.Left);

            switch (vertex)
            {
                case Vertex.Top:
                    hasRoad = (tile.RoadAt(Side.TopLeft) == Road.Road && tile.PlayerAtSide(Side.TopLeft) == player) || (tile.RoadAt(Side.TopRight) == Road.Road && tile.PlayerAtSide(Side.TopRight) == player);
                    if (topLeftNeighbor != null)
                    {
                        hasRoad = hasRoad || (topLeftNeighbor.RoadAt(Side.Right) == Road.Road && topLeftNeighbor.PlayerAtSide(Side.Right) == player);
                    }
                    if (topRightNeighbor != null)
                    {
                        hasRoad = hasRoad || (topRightNeighbor.RoadAt(Side.Left) == Road.Road && topRightNeighbor.PlayerAtSide(Side.Left) == player);
                    }
                    break;
                case Vertex.TopRight:
                    hasRoad = (tile.RoadAt(Side.TopRight) == Road.Road && tile.PlayerAtSide(Side.TopRight) == player) || (tile.RoadAt(Side.Right) == Road.Road && tile.PlayerAtSide(Side.Right) == player);
                    if (topRightNeighbor != null)
                    {
                        hasRoad = hasRoad || (topRightNeighbor.RoadAt(Side.BottomRight) == Road.Road && topRightNeighbor.PlayerAtSide(Side.BottomRight) == player);
                    }
                    if (rightNeighbor != null)
                    {
                        hasRoad = hasRoad || (rightNeighbor.RoadAt(Side.TopLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.TopLeft) == player);
                    }
                    break;
                case Vertex.BottomRight:
                    hasRoad = (tile.RoadAt(Side.Right) == Road.Road && tile.PlayerAtSide(Side.Right) == player) || (tile.RoadAt(Side.BottomRight) == Road.Road && tile.PlayerAtSide(Side.BottomRight) == player);
                    if (rightNeighbor != null)
                    {
                        hasRoad = hasRoad || (rightNeighbor.RoadAt(Side.BottomLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.BottomLeft) == player);
                    }
                    if (bottomRightNeighbor != null)
                    {
                        hasRoad = hasRoad || (bottomRightNeighbor.RoadAt(Side.TopRight) == Road.Road && bottomRightNeighbor.PlayerAtSide(Side.TopRight) == player);
                    }
                    break;
                case Vertex.Bottom:
                    hasRoad = (tile.RoadAt(Side.BottomLeft) == Road.Road && tile.PlayerAtSide(Side.BottomLeft) == player) || (tile.RoadAt(Side.BottomRight) == Road.Road && tile.PlayerAtSide(Side.BottomRight) == player);
                    if (bottomRightNeighbor != null)
                    {
                        hasRoad = hasRoad || (bottomRightNeighbor.RoadAt(Side.Left) == Road.Road && bottomRightNeighbor.PlayerAtSide(Side.Left) == player);
                    }
                    if (bottomLeftNeighbor != null)
                    {
                        hasRoad = hasRoad || (bottomLeftNeighbor.RoadAt(Side.Right) == Road.Road && bottomLeftNeighbor.PlayerAtSide(Side.Right) == player);
                    }
                    break;
                case Vertex.BottomLeft:
                    hasRoad = (tile.RoadAt(Side.Left) == Road.Road && tile.PlayerAtSide(Side.Left) == player) || (tile.RoadAt(Side.BottomLeft) == Road.Road && tile.PlayerAtSide(Side.BottomLeft) == player);
                    if (bottomLeftNeighbor != null)
                    {
                        hasRoad = hasRoad || (bottomLeftNeighbor.RoadAt(Side.TopLeft) == Road.Road && bottomLeftNeighbor.PlayerAtSide(Side.TopLeft) == player);
                    }
                    if (leftNeighbor != null)
                    {
                        hasRoad = hasRoad || (leftNeighbor.RoadAt(Side.BottomRight) == Road.Road && leftNeighbor.PlayerAtSide(Side.BottomRight) == player);
                    }
                    break;
                default:
                    hasRoad = (tile.RoadAt(Side.Left) == Road.Road && tile.PlayerAtSide(Side.Left) == player) || (tile.RoadAt(Side.TopLeft) == Road.Road && tile.PlayerAtSide(Side.TopLeft) == player);
                    if (leftNeighbor != null)
                    {
                        hasRoad = hasRoad || (leftNeighbor.RoadAt(Side.TopRight) == Road.Road && leftNeighbor.PlayerAtSide(Side.TopRight) == player);
                    }
                    if (topLeftNeighbor != null)
                    {
                        hasRoad = hasRoad || (topLeftNeighbor.RoadAt(Side.BottomLeft) == Road.Road && topLeftNeighbor.PlayerAtSide(Side.BottomLeft) == player);
                    }
                    break;
            }

            return hasRoad;
        }

        private bool BuildingProximityValid(int row, int col, Vertex vertex)
        {
            // get tile (assumed not null)
            Tile? tile = _tiles[row, col];

            // check proximity to other structures
            bool valid;
            Tile? topLeftNeighbor = GetNeighbor(row, col, Side.TopLeft);
            Tile? topRightNeighbor = GetNeighbor(row, col, Side.TopRight);
            Tile? rightNeighbor = GetNeighbor(row, col, Side.Right);
            Tile? bottomRightNeighbor = GetNeighbor(row, col, Side.BottomRight);
            Tile? bottomLeftNeighbor = GetNeighbor(row, col, Side.BottomLeft);
            Tile? leftNeighbor = GetNeighbor(row, col, Side.Left);

            switch (vertex)
            {
                case Vertex.Top:
                    valid = tile.BuildingAt(Vertex.TopLeft) == Building.NoBuilding && tile.BuildingAt(Vertex.TopRight) == Building.NoBuilding;
                    if (topLeftNeighbor != null)
                    {
                        valid = valid && topLeftNeighbor.BuildingAt(Vertex.TopRight) == Building.NoBuilding;
                    }
                    if (topRightNeighbor != null)
                    {
                        valid = valid && topRightNeighbor.BuildingAt(Vertex.TopLeft) == Building.NoBuilding;
                    }
                    break;
                case Vertex.TopRight:
                    valid = tile.BuildingAt(Vertex.Top) == Building.NoBuilding && tile.BuildingAt(Vertex.BottomRight) == Building.NoBuilding;
                    if (topRightNeighbor != null)
                    {
                        valid = valid && topRightNeighbor.BuildingAt(Vertex.BottomRight) == Building.NoBuilding;
                    }
                    if (rightNeighbor != null)
                    {
                        valid = valid && rightNeighbor.BuildingAt(Vertex.Top) == Building.NoBuilding;
                    }
                    break;
                case Vertex.BottomRight:
                    valid = tile.BuildingAt(Vertex.TopRight) == Building.NoBuilding && tile.BuildingAt(Vertex.Bottom) == Building.NoBuilding;
                    if (rightNeighbor != null)
                    {
                        valid = valid && rightNeighbor.BuildingAt(Vertex.Bottom) == Building.NoBuilding;
                    }
                    if (bottomRightNeighbor != null)
                    {
                        valid = valid && bottomRightNeighbor.BuildingAt(Vertex.TopRight) == Building.NoBuilding;
                    }
                    break;
                case Vertex.Bottom:
                    valid = tile.BuildingAt(Vertex.BottomRight) == Building.NoBuilding && tile.BuildingAt(Vertex.BottomLeft) == Building.NoBuilding;
                    if (bottomRightNeighbor != null)
                    {
                        valid = valid && bottomRightNeighbor.BuildingAt(Vertex.BottomLeft) == Building.NoBuilding;
                    }
                    if (bottomLeftNeighbor != null)
                    {
                        valid = valid && bottomLeftNeighbor.BuildingAt(Vertex.BottomRight) == Building.NoBuilding;
                    }
                    break;
                case Vertex.BottomLeft:
                    valid = tile.BuildingAt(Vertex.TopLeft) == Building.NoBuilding && tile.BuildingAt(Vertex.Bottom) == Building.NoBuilding;
                    if (bottomLeftNeighbor != null)
                    {
                        valid = valid && bottomLeftNeighbor.BuildingAt(Vertex.TopLeft) == Building.NoBuilding;
                    }
                    if (leftNeighbor != null)
                    {
                        valid = valid && leftNeighbor.BuildingAt(Vertex.Bottom) == Building.NoBuilding;
                    }
                    break;
                default:
                    valid = tile.BuildingAt(Vertex.BottomLeft) == Building.NoBuilding && tile.BuildingAt(Vertex.Top) == Building.NoBuilding;
                    if (leftNeighbor != null) 
                    {
                        valid = valid && leftNeighbor.BuildingAt(Vertex.Top) == Building.NoBuilding;
                    }
                    if (topLeftNeighbor != null)
                    {
                        valid = valid && topLeftNeighbor.BuildingAt(Vertex.BottomLeft) == Building.NoBuilding;
                    }
                    break;
            }

            return valid;
        }

        public bool CanBuildSettlementAt(Player player, int row, int col, Vertex vertex)
        {
            // get tile
            Tile? tile = _tiles[row, col];

            // if tile is null, its unbuildable
            if (tile == null) return false;

            // if vertex has settlement/city, its unbuildable
            if (tile.BuildingAt(vertex) != Building.NoBuilding) return false;

            // check if both building and road conditions are met
            return BuildingProximityValid(row, col, vertex) && RoadsMeetAtVertex(player, row, col, vertex);
        }

        public bool CanBuildCityAt(Player player, int row, int col, Vertex vertex)
        {
            // get tile
            Tile? tile = _tiles[row, col];

            // if tile is null, its unbuildable
            if (tile == null) return false;

            // can only build on top of an ally settlement
            return tile.BuildingAt(vertex) == Building.Settlement && tile.PlayerAtVertex(vertex) == player;
        }

        public bool CanBuildRoadAt(Player player, int row, int col, Side side)
        {
            // get tile
            Tile? tile = _tiles[row, col];

            // if tile is null, road is unbuildable     
            if (tile == null) return false;

            // if side has road already, new road is unbuildable
            if (tile.RoadAt(side) != Road.NoRoad) return false;

            // boolean helpers
            bool topHasBuilding = tile.BuildingAt(Vertex.Top) != Building.NoBuilding;
            bool topBuildingIsFriendly = tile.PlayerAtVertex(Vertex.Top) == player;
            bool topRightHasBuilding = tile.BuildingAt(Vertex.TopRight) != Building.NoBuilding;
            bool topRightBuildingIsFriendly = tile.PlayerAtVertex(Vertex.TopRight) == player;
            bool bottomRightHasBuilding = tile.BuildingAt(Vertex.BottomRight) != Building.NoBuilding;
            bool bottomRightBuildingIsFriendly = tile.PlayerAtVertex(Vertex.BottomRight) == player;
            bool bottomHasBuilding = tile.BuildingAt(Vertex.Bottom) != Building.NoBuilding;
            bool bottomBuildingIsFriendly = tile.PlayerAtVertex(Vertex.Bottom) == player;
            bool bottomLeftHasBuilding = tile.BuildingAt(Vertex.BottomLeft) != Building.NoBuilding;
            bool bottomLeftBuildingIsFriendly = tile.PlayerAtVertex(Vertex.BottomLeft) == player;
            bool topLeftHasBuilding = tile.BuildingAt(Vertex.TopLeft) != Building.NoBuilding;
            bool topLeftBuildingIsFriendly = tile.PlayerAtVertex(Vertex.TopLeft) == player;

            // now check case-by-case
            switch (side)
            {
                case Side.TopRight:
                    // check for ally buildings first
                    if ( (topHasBuilding && topBuildingIsFriendly) || (topRightHasBuilding && topRightBuildingIsFriendly) )
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.Top) && !topHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.TopRight) && !topRightHasBuilding) return true;
                    break;
                case Side.Right:
                    // check for ally buildings first
                    if ((topRightHasBuilding && topRightBuildingIsFriendly) || (bottomRightHasBuilding && bottomRightBuildingIsFriendly))
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.TopRight) && !topRightHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.BottomRight) && !bottomRightHasBuilding) return true;
                    break;
                case Side.BottomRight:
                    // check for ally buildings first
                    if ((bottomRightHasBuilding && bottomRightBuildingIsFriendly) || (bottomHasBuilding && bottomBuildingIsFriendly))
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.BottomRight) && !bottomRightHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.Bottom) && !bottomHasBuilding) return true;
                    break;
                case Side.BottomLeft:
                    // check for ally buildings first
                    if ((bottomHasBuilding && bottomBuildingIsFriendly) || (bottomLeftHasBuilding && bottomLeftBuildingIsFriendly))
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.Bottom) && !bottomHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.BottomLeft) && !bottomLeftHasBuilding) return true;
                    break;
                case Side.Left:
                    // check for ally buildings first
                    if ((bottomLeftHasBuilding && bottomLeftBuildingIsFriendly) || (topLeftHasBuilding && topLeftBuildingIsFriendly))
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.BottomLeft) && !bottomLeftHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.TopLeft) && !topLeftHasBuilding) return true;
                    break;
                default:
                    // check for ally buildings first
                    if ((topLeftHasBuilding && topLeftBuildingIsFriendly) || (topHasBuilding && topBuildingIsFriendly))
                    {
                        return true;
                    }

                    // check for roads and blockers
                    if (RoadsMeetAtVertex(player, row, col, Vertex.TopLeft) && !topLeftHasBuilding) return true;
                    if (RoadsMeetAtVertex(player, row, col, Vertex.Top) && !topHasBuilding) return true;
                    break;
            }

            return false;
        }

        public int EligibleRoadSpots(Player player)
        {
            int landlockedSides = 0;
            int sidesBorderingWater = 0;

            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    // check each side
                    for (int side = 0; side < Enum.GetNames(typeof(Side)).Length; side++) 
                    {
                        bool canBuild = CanBuildRoadAt(player, i, j, (Side)side);
                        if (canBuild)
                        {
                            if (BordersWater(i, j, (Side)side)) sidesBorderingWater++;
                            else landlockedSides++;
                        }
                    }
                }
            }

            // divide landlocked sides by 2 because they will be counted twice
            return (landlockedSides / 2) + sidesBorderingWater;
        }

        public void BuildBuilding(Player player, Building building, int row, int col, Vertex vertex)
        {
            // assume tile is not null
            Tile? tile = _tiles[row, col];

            // build building on main vertex
            tile.SetBuildingAt(vertex, building);
            tile.SetPlayerAtVertex(vertex, player);

            // neighboring tiles
            Tile? topLeftNeighbor = GetNeighbor(row, col, Side.TopLeft);
            Tile? topRightNeighbor = GetNeighbor(row, col, Side.TopRight);
            Tile? rightNeighbor = GetNeighbor(row, col, Side.Right);
            Tile? bottomRightNeighbor = GetNeighbor(row, col, Side.BottomRight);
            Tile? bottomLeftNeighbor = GetNeighbor(row, col, Side.BottomLeft);
            Tile? leftNeighbor = GetNeighbor(row, col, Side.Left);

            // build building on neighboring tiles
            switch (vertex)
            {
                case Vertex.Top:
                    if (topLeftNeighbor != null)
                    {
                        topLeftNeighbor.SetBuildingAt(Vertex.BottomRight, building);
                        topLeftNeighbor.SetPlayerAtVertex(Vertex.BottomRight, player);
                    }
                    if (topRightNeighbor != null)
                    {
                        topRightNeighbor.SetBuildingAt(Vertex.BottomLeft, building);
                        topRightNeighbor.SetPlayerAtVertex(Vertex.BottomLeft, player);
                    }
                    break;
                case Vertex.TopRight:
                    if (topRightNeighbor != null)
                    {
                        topRightNeighbor.SetBuildingAt(Vertex.Bottom, building);
                        topRightNeighbor.SetPlayerAtVertex(Vertex.Bottom, player);
                    }
                    if (rightNeighbor != null)
                    {
                        rightNeighbor.SetBuildingAt(Vertex.TopLeft, building);
                        rightNeighbor.SetPlayerAtVertex(Vertex.TopLeft, player);
                    }
                    break;
                case Vertex.BottomRight:
                    if (rightNeighbor != null)
                    {
                        rightNeighbor.SetBuildingAt(Vertex.BottomLeft, building);
                        rightNeighbor.SetPlayerAtVertex(Vertex.BottomLeft, player);
                    }
                    if (bottomRightNeighbor != null)
                    {
                        bottomRightNeighbor.SetBuildingAt(Vertex.Top, building);
                        bottomRightNeighbor.SetPlayerAtVertex(Vertex.Top, player);
                    }
                    break;
                case Vertex.Bottom:
                    if (bottomRightNeighbor != null)
                    {
                        bottomRightNeighbor.SetBuildingAt(Vertex.TopLeft, building);
                        bottomRightNeighbor.SetPlayerAtVertex(Vertex.TopLeft, player);
                    }
                    if (bottomLeftNeighbor != null)
                    {
                        bottomLeftNeighbor.SetBuildingAt(Vertex.TopRight, building);
                        bottomLeftNeighbor.SetPlayerAtVertex(Vertex.TopRight, player);
                    }
                    break;
                case Vertex.BottomLeft:
                    if (bottomLeftNeighbor != null)
                    {
                        bottomLeftNeighbor.SetBuildingAt(Vertex.Top, building);
                        bottomLeftNeighbor.SetPlayerAtVertex(Vertex.Top, player);
                    }
                    if (leftNeighbor != null)
                    {
                        leftNeighbor.SetBuildingAt(Vertex.BottomRight, building);
                        leftNeighbor.SetPlayerAtVertex(Vertex.BottomRight, player);
                    }
                    break;
                default:
                    if (leftNeighbor != null)
                    {
                        leftNeighbor.SetBuildingAt(Vertex.TopRight, building);
                        leftNeighbor.SetPlayerAtVertex(Vertex.TopRight, player);
                    }
                    if (topLeftNeighbor != null)
                    {
                        topLeftNeighbor.SetBuildingAt(Vertex.Bottom, building);
                        topLeftNeighbor.SetPlayerAtVertex(Vertex.Bottom, player);
                    }
                    break;
            }

            // add port
            player.AddPort(tile.PortAt(vertex));
        }

        public void BuildRoad(Player player, int row, int col, Side side)
        {
            // assume tile is not null
            Tile? tile = _tiles[row, col];

            // build road on main side
            tile.SetRoadAt(side, Road.Road);
            tile.SetPlayerAtSide(side, player);

            // build road on neighboring tiles
            switch (side)
            {
                case Side.TopRight:
                    Tile? topRightNeighbor = GetNeighbor(row, col, Side.TopRight);
                    if (topRightNeighbor != null)
                    {
                        topRightNeighbor.SetRoadAt(Side.BottomLeft, Road.Road);
                        topRightNeighbor.SetPlayerAtSide(Side.BottomLeft, player);
                    }
                    break;
                case Side.Right:
                    Tile? rightNeighbor = GetNeighbor(row, col, Side.Right);
                    if (rightNeighbor != null)
                    {
                        rightNeighbor.SetRoadAt(Side.Left, Road.Road);
                        rightNeighbor.SetPlayerAtSide(Side.Left, player);
                    }
                    break;
                case Side.BottomRight:
                    Tile? bottomRightNeighbor = GetNeighbor(row, col, Side.BottomRight);
                    if (bottomRightNeighbor != null)
                    {
                        bottomRightNeighbor.SetRoadAt(Side.TopLeft, Road.Road);
                        bottomRightNeighbor.SetPlayerAtSide(Side.TopLeft, player);
                    }
                    break;
                case Side.BottomLeft:
                    Tile? bottomLeftNeighbor = GetNeighbor(row, col, Side.BottomLeft);
                    if (bottomLeftNeighbor != null)
                    {
                        bottomLeftNeighbor.SetRoadAt(Side.TopRight, Road.Road);
                        bottomLeftNeighbor.SetPlayerAtSide(Side.TopRight, player);
                    }
                    break;
                case Side.Left:
                    Tile? leftNeighbor = GetNeighbor(row, col, Side.Left);
                    if (leftNeighbor != null)
                    {
                        leftNeighbor.SetRoadAt(Side.Right, Road.Road);
                        leftNeighbor.SetPlayerAtSide(Side.Right, player);
                    }
                    break;
                default:
                    Tile? topLeftNeighbor = GetNeighbor(row, col, Side.TopLeft);
                    if (topLeftNeighbor != null)
                    {
                        topLeftNeighbor.SetRoadAt(Side.BottomRight, Road.Road);
                        topLeftNeighbor.SetPlayerAtSide(Side.BottomRight, player);
                    }
                    break;
            }
        }

        // method for moving robber
        public bool MoveRobber(int row, int col)
        {
            // if new tile is null, return false
            Tile? newTile = _tiles[row, col];
            if (newTile == null) return false;

            // if new tile is equal to old tile, return false
            if (newTile == _robberTile) return false;

            // otherwise, set new robber tile
            _robberTile = newTile;

            return true; 
        }

        // game activity
        public void RespondToRoll(int roll)
        {
            // if roll is a 7, greedy players must discard
            if (roll == 7)
            {
                // set discard flags for greedy players 
                foreach (Player p in _players)
                {
                    if (p.HandSize() > MaxHandSize)
                    {
                        p.SetDiscard(true);
                    }
                }
            }
            
            // otherwise, harvest resources
            else
            {
                for (int i = 0; i < _tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < _tiles.GetLength(1); j++)
                    {
                        Tile? tile = _tiles[i, j];

                        // if tile is not water and is harvestable given this roll, then harvest from it
                        if (tile != null && tile.CanHarvest(roll))
                        {
                            tile.Harvest(_bank);
                        }
                    }
                }
            }
        }

        // LONGEST ROAD CODE

        // adds a full road pair instead of just the singular road on one side
        public void AddRoadPair(int row, int col, Side side, int[,,] visited)
        {
            // add initial road
            visited[row, col, (int)side] = 1;

            (int, int) neighbor = GetNeighborIndices(row, col, side);
            if (neighbor.Item1 != -1)
            {
                switch (side)
                {
                    case Side.TopRight:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.BottomLeft] = 1;
                        break;
                    case Side.Right:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.Left] = 1;
                        break;
                    case Side.BottomRight:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.TopLeft] = 1;
                        break;
                    case Side.BottomLeft:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.TopRight] = 1;
                        break;
                    case Side.Left:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.Right] = 1;
                        break;
                    case Side.TopLeft:
                        visited[neighbor.Item1, neighbor.Item2, (int)Side.BottomRight] = 1;
                        break;
                }
            }
        }

        /*private void UpdateLongestRoad(int row, int col, Side side)
        {
            // array of visited roads
            int[,,] visitedSides = new int[_tiles.GetLength(0), _tiles.GetLength(1), Enum.GetNames(typeof(Side)).Length];

            // current tile/side we're on
            Tile currentTile = _game.TileAt(row, col);
            Side currentSide = side;

            // add current tile/side combo to dictionary
            AddRoadPair(row, col, side, visitedSides);
            int roadLength = 1;

            while (true)
            {

            }
        }

        // update valid visitable neighbors of a side
        public void UpdateVisitableNeighbors(int row, int col, Side side, Player player, List<(Tile, Side)> neighbors, Dictionary<Tile, Side> visited)
        {
            // get tile
            Tile tile = TileAt(row, col);

            // all neighbors
            Tile? topRightNeighbor = GetNeighbor(row, col, Side.TopRight);
            Tile? rightNeighbor = GetNeighbor(row, col, Side.Right);
            Tile? bottomRightNeighbor = GetNeighbor(row, col, Side.BottomRight);
            Tile? bottomLeftNeighbor = GetNeighbor(row, col, Side.BottomLeft);
            Tile? leftNeighbor = GetNeighbor(row, col, Side.Left);
            Tile? topLeftNeighbor = GetNeighbor(row, col, Side.TopLeft);

            // check neighbors case-by-case
            Side sideLookup;
            switch (side)
            {
                case Side.TopRight:
                    // must check top-left, top-right, and right neighbor
                    if (tile.RoadAt(Side.TopLeft) == Road.Road && tile.PlayerAtSide(Side.TopLeft) == player)
                    {
                        // only mark as candidate if it hasn't been visited before
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.TopLeft));
                        }
                        else if (sideLookup != Side.TopLeft)
                        {
                            neighbors.Add((tile, Side.TopLeft));
                        }
                    }

                    if (tile.RoadAt(Side.Right) == Road.Road && tile.PlayerAtSide(Side.Right) == this)
                    {
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.Right));
                        }
                        else if (sideLookup != Side.Right)
                        {
                            neighbors.Add((tile, Side.Right));
                        }
                    }

                    if (topLeftNeighbor != null)
                    {
                        if (topLeftNeighbor.RoadAt(Side.Right) == Road.Road && topLeftNeighbor.PlayerAtSide(Side.Right) == this)
                        {
                            if (!visited.TryGetValue(topLeftNeighbor, out sideLookup))
                            {
                                neighbors.Add((topLeftNeighbor, Side.Right));
                            }
                            else if (sideLookup != Side.Right)
                            {
                                neighbors.Add((topLeftNeighbor, Side.Right));
                            }
                        }
                    }
                    else if (topRightNeighbor != null)
                    {
                        if (topRightNeighbor.RoadAt(Side.Left) == Road.Road && topRightNeighbor.PlayerAtSide(Side.Left) == this)
                        {
                            if (!visited.TryGetValue(topRightNeighbor, out sideLookup))
                            {
                                neighbors.Add((topRightNeighbor, Side.Left));
                            }
                            else if (sideLookup != Side.Left)
                            {
                                neighbors.Add((topRightNeighbor, Side.Left));
                            }
                        }
                    }

                    if (topRightNeighbor != null)
                    {
                        if (topRightNeighbor.RoadAt(Side.BottomRight) == Road.Road && topRightNeighbor.PlayerAtSide(Side.BottomRight) == this)
                        {
                            if (!visited.TryGetValue(topRightNeighbor, out sideLookup))
                            {
                                neighbors.Add((topRightNeighbor, Side.BottomRight));
                            }
                            else if (sideLookup != Side.BottomRight)
                            {
                                neighbors.Add((topRightNeighbor, Side.BottomRight));
                            }
                        }
                    }
                    else if (rightNeighbor != null)
                    {
                        if (rightNeighbor.RoadAt(Side.TopLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.TopLeft) == this)
                        {
                            if (!visited.TryGetValue(rightNeighbor, out sideLookup))
                            {
                                neighbors.Add((rightNeighbor, Side.TopLeft));
                            }
                            else if (sideLookup != Side.TopLeft)
                            {
                                neighbors.Add((rightNeighbor, Side.TopLeft));
                            }
                        }
                    }
                    break;
                case Side.Right:
                    if (tile.RoadAt(Side.TopRight) == Road.Road && tile.PlayerAtSide(Side.TopRight) == this)
                    {
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.TopRight));
                        }
                        else if (sideLookup != Side.TopRight)
                        {
                            neighbors.Add((tile, Side.TopRight));
                        }
                    }

                    if (tile.RoadAt(Side.BottomRight) == Road.Road && tile.PlayerAtSide(Side.BottomRight) == this)
                    {
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.BottomRight));
                        }
                        else if (sideLookup != Side.BottomRight)
                        {
                            neighbors.Add((tile, Side.BottomRight));
                        }
                    }

                    if (topRightNeighbor != null)
                    {
                        if (topRightNeighbor.RoadAt(Side.BottomRight) == Road.Road && topRightNeighbor.PlayerAtSide(Side.BottomRight) == this)
                        {
                            if (!visited.TryGetValue(topRightNeighbor, out sideLookup))
                            {
                                neighbors.Add((topRightNeighbor, Side.BottomRight));
                            }
                            else if (sideLookup != Side.BottomRight)
                            {
                                neighbors.Add((topRightNeighbor, Side.BottomRight));
                            }
                        }
                    }
                    else if (rightNeighbor != null)
                    {
                        if (rightNeighbor.RoadAt(Side.TopLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.TopLeft) == this)
                        {
                            if (!visited.TryGetValue(rightNeighbor, out sideLookup))
                            {
                                neighbors.Add((rightNeighbor, Side.TopLeft));
                            }
                            else if (sideLookup != Side.TopLeft)
                            {
                                neighbors.Add((rightNeighbor, Side.TopLeft));
                            }
                        }
                    }

                    if (rightNeighbor != null)
                    {
                        if (rightNeighbor.RoadAt(Side.BottomLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.BottomLeft) == this)
                        {
                            if (!visited.TryGetValue(rightNeighbor, out sideLookup))
                            {
                                neighbors.Add((rightNeighbor, Side.BottomLeft));
                            }
                            else if (sideLookup != Side.BottomLeft)
                            {
                                neighbors.Add((rightNeighbor, Side.BottomLeft));
                            }
                        }
                    }
                    else if (bottomRightNeighbor != null)
                    {
                        if (bottomRightNeighbor.RoadAt(Side.TopRight) == Road.Road && bottomRightNeighbor.PlayerAtSide(Side.TopRight) == this)
                        {
                            if (!visited.TryGetValue(bottomRightNeighbor, out sideLookup))
                            {
                                neighbors.Add((bottomRightNeighbor, Side.TopRight));
                            }
                            else if (sideLookup != Side.TopRight)
                            {
                                neighbors.Add((bottomRightNeighbor, Side.TopRight));
                            }
                        }
                    }
                    break;
                case Side.BottomRight:
                    if (tile.RoadAt(Side.Right) == Road.Road && tile.PlayerAtSide(Side.Right) == this)
                    {
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.Right));
                        }
                        else if (sideLookup != Side.Right)
                        {
                            neighbors.Add((tile, Side.Right));
                        }
                    }

                    if (tile.RoadAt(Side.BottomLeft) == Road.Road && tile.PlayerAtSide(Side.BottomLeft) == this)
                    {
                        if (!visited.TryGetValue(tile, out sideLookup))
                        {
                            neighbors.Add((tile, Side.BottomLeft));
                        }
                        else if (sideLookup != Side.BottomLeft)
                        {
                            neighbors.Add((tile, Side.BottomLeft));
                        }
                    }

                    if (rightNeighbor != null)
                    {
                        if (rightNeighbor.RoadAt(Side.BottomLeft) == Road.Road && rightNeighbor.PlayerAtSide(Side.BottomLeft) == this)
                        {
                            if (!visited.TryGetValue(rightNeighbor, out sideLookup))
                            {
                                neighbors.Add((rightNeighbor, Side.BottomLeft));
                            }
                            else if (sideLookup != Side.BottomLeft)
                            {
                                neighbors.Add((rightNeighbor, Side.BottomLeft));
                            }
                        }
                    }
                    else if (bottomRightNeighbor != null)
                    {
                        if (bottomRightNeighbor.RoadAt(Side.TopRight) == Road.Road && bottomRightNeighbor.PlayerAtSide(Side.TopRight) == this)
                        {
                            if (!visited.TryGetValue(bottomRightNeighbor, out sideLookup))
                            {
                                neighbors.Add((bottomRightNeighbor, Side.TopRight));
                            }
                            else if (sideLookup != Side.TopRight)
                            {
                                neighbors.Add((bottomRightNeighbor, Side.TopRight));
                            }
                        }
                    }


                    break;
            }
        }*/

        public void LoadMap(string fileName)
        {
            // split data into lines
            string data = File.ReadAllText(fileName);
            string[] lines = data.Split('\n');

            // first two lines are dimensions
            int rows = Convert.ToInt32(lines[0]);
            int cols = Convert.ToInt32(lines[1]);
            _tiles = new Tile[rows, cols];

            // parse remaining lines
            for (int line = 2; line < lines.Length; line++)
            {
                string[] tiles = lines[line].Split(',');
                int row = line - 2;

                for (int col = 0; col < tiles.Length; col++)
                {
                    string tile = tiles[col];

                    // if water tile, set value to null
                    if (tile == "-")
                    {
                        _tiles[row, col] = null;
                    }
                    // otherwise, analyze the tile deeper
                    else
                    {
                        // split tile into chunks
                        string[] tileChunks = tile.Split(" ");
                        Resource resource;
                        int diceRoll = -1;

                        // first arg is resource
                        switch (tileChunks[0])
                        {
                            case "n":
                                resource = Resource.NoResource;
                                break;
                            case "b":
                                resource = Resource.Brick;
                                break;
                            case "l":
                                resource = Resource.Lumber;
                                break;
                            case "o":
                                resource = Resource.Ore;
                                break;
                            case "g":
                                resource = Resource.Grain;
                                break;
                            default:
                                resource = Resource.Wool;
                                break;
                        }

                        // last arg is dice roll (only retrieve if resource is not none)
                        if (resource != Resource.NoResource)
                        {
                            diceRoll = Convert.ToInt32(tileChunks[3]);
                        }

                        // create tile
                        Tile newTile = new Tile(resource, diceRoll);

                        // set ports
                        string[] ports = tileChunks[1].Trim('[').Trim(']').Split("|");
                        string[] portLocations = tileChunks[2].Trim('[').Trim(']').Split("|");
                        if (Array.IndexOf(ports, "n") == -1)
                        {
                            for (int i = 0; i < ports.Length; i++)
                            {
                                Port resourcePort;
                                Vertex location;

                                // find specific port type
                                switch (ports[i])
                                {
                                    case "n":
                                        resourcePort = Port.NoPort;
                                        break;
                                    case "b":
                                        resourcePort = Port.BrickPort;
                                        break;
                                    case "l":
                                        resourcePort = Port.LumberPort;
                                        break;
                                    case "o":
                                        resourcePort = Port.OrePort;
                                        break;
                                    case "g":
                                        resourcePort = Port.GrainPort;
                                        break;
                                    case "w":
                                        resourcePort = Port.WoolPort;
                                        break;
                                    default:
                                        resourcePort = Port.AnyPort;
                                        break;
                                }

                                // find specific port location
                                switch (portLocations[i])
                                {
                                    case "Top":
                                        location = Vertex.Top;
                                        break;
                                    case "TopRight":
                                        location = Vertex.TopRight;
                                        break;
                                    case "BottomRight":
                                        location = Vertex.BottomRight;
                                        break;
                                    case "Bottom":
                                        location = Vertex.Bottom;
                                        break;
                                    case "BottomLeft":
                                        location = Vertex.BottomLeft;
                                        break;
                                    default:
                                        location = Vertex.TopLeft;
                                        break;
                                }

                                // set tile
                                newTile.SetPortAt(location, resourcePort);
                            }
                        }

                        // add tile to grid
                        _tiles[row, col] = newTile;
                    }
                }
            }
        }

        public static void CreateMap()
        {
            // get rows and cols
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the CATAN Map Builder!");
            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("To get started, how many rows would you like in your CATAN board?:");
            int rows = 0;
            while (rows < 1)
            {
                int.TryParse(Console.ReadLine(), out rows);
            }

            Console.WriteLine("How many columns?");
            int cols = 0;
            while (cols < 1)
            {
                int.TryParse(Console.ReadLine(), out cols);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Great! Just to read those back to you, you want a " + rows + " x " + cols + " game.");
            Console.WriteLine("┌∩┐(◣_◢)┌∩┐");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("This is what your map looks like blank:");
            for (int i = 0; i < rows; i++)
            {
                string row = "";
                if (i % 2 == 1) row += "   ";
                for (int j = 0; j < cols; j++)
                {
                    row += "-----   ";
                }
                Console.WriteLine(row);
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That's not very fun now, is it! Let's add some life to this map, row by row!");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Alright, here are the codes you should know.");
            Console.WriteLine();

            Console.WriteLine("Resources:");
            Console.WriteLine("- 'n' = No resource");
            Console.WriteLine("- 'b' = Brick");
            Console.WriteLine("- 'l' = Lumber");
            Console.WriteLine("- 'o' = Ore");
            Console.WriteLine("- 'g' = Grain");
            Console.WriteLine("- 'w' = Wool");
            Console.WriteLine();

            Console.WriteLine("Ports:");
            Console.WriteLine("- 'n' = No port");
            Console.WriteLine("- 'b' = Brick 2:1 port");
            Console.WriteLine("- 'l' = Lumber 2:1 port");
            Console.WriteLine("- 'o' = Ore 2:1 port");
            Console.WriteLine("- 'g' = Grain 2:1 port");
            Console.WriteLine("- 'w' = Wool 2:1 port");
            Console.WriteLine("- 'a' = 3:1 port");
            Console.WriteLine();

            Console.WriteLine("Water Tile: '-'");
            Console.WriteLine("Vertices: Top, TopRight, BottomRight, Bottom, BottomLeft, TopLeft");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I'm gonna prompt you to draw out the map line-by-line. Each entry in the line must follow this format:");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine("[RESOURCE] [[PORTS]] [[PORT_LOCATIONS]] [NUMBER],[RESOURCE] [PORT] [PORT_LOCATION] [NUMBER],[RESOURCE] [PORT] [PORT_LOCATION] [NUMBER],...");
            Console.WriteLine();
            Console.WriteLine("e.g. 'b [b|b] [BottomLeft|BottomRight] 12,-,o [n] ??? 8,n [b|b] [TopLeft|TopRight] ???'");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--Note, if anything is marked as none, the corresponding arg will not be read (e.g. port location or resource number), so it doesn't matter what you put for it.--");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            string output = rows + "\n" + cols + "\n";
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("Row " + (i + 1) + ":");
                string line = Console.ReadLine();

                // split line by comma
                string[] chunks = line.Split(',');

                // if there are less than cols chunks, re-do
                if (chunks.Length < cols)
                {
                    Console.WriteLine("Number of tiles is insufficient.");
                    i--;
                    continue;
                }

                int currentI = i;
                for (int k = 0; k < chunks.Length; k++)
                {
                    // retrieve chunk
                    string chunk = chunks[k];

                    // check if chunk is water tile
                    if (chunk != "-")
                    {
                        string[] splitChunk = chunk.Split(' ');

                        // check length of chunk 
                        if (splitChunk.Length != 4)
                        {
                            Console.WriteLine("Each chunk must have 4 args.");
                            i--;
                            break;
                        }

                        // check resource 
                        string resource = splitChunk[0];
                        if (resource != "n" && resource != "b" && resource != "l" && resource != "o" && resource != "g" && resource != "w")
                        {
                            Console.WriteLine("Invalid code for resource.");
                            i--;
                            break;
                        }

                        // check validity of ports
                        string port = splitChunk[1];
                        if (port[0] != '[' || port[port.Length-1] != ']')
                        {
                            Console.WriteLine("Port must be enclosed in square brackets []");
                            i--;
                            break;
                        }

                        port = port.Trim('[');
                        port = port.Trim(']');
                        string[] ports = port.Split("|");
                        bool allPortsValid = true;
                        foreach (string p in ports)
                        {
                            if (p != "n" && p != "b" && p != "l" && p != "o" && p != "g" && p != "w" && p != "a")
                            {
                                allPortsValid = false;
                                break;
                            }
                        }
                        if (!allPortsValid)
                        {
                            Console.WriteLine("Invalid code for port.");
                            i--;
                            break;
                        }

                        // only check validity of port location if corresponding port is not none
                        string portLocation = splitChunk[2];
                        if (portLocation[0] != '[' || portLocation[portLocation.Length - 1] != ']')
                        {
                            Console.WriteLine("Port locations must be enclosed in square brackets []");
                            i--;
                            break;
                        }

                        portLocation = portLocation.Trim('[');
                        portLocation = portLocation.Trim(']');
                        string[] portLocations = portLocation.Split("|");
                        if (portLocations.Length != ports.Length)
                        {
                            Console.WriteLine("Number of port locations must match number of ports.");
                            i--;
                            break; 
                        }

                        bool allPortLocationsValid = true;
                        for (int j = 0; j < portLocations.Length; j++)
                        {
                            string pl = portLocations[j];
                            if (ports[j] != "n" && pl != "Top" && pl != "TopRight" && pl != "BottomRight" && pl != "Bottom" && pl != "BottomLeft" && pl != "TopLeft")
                            {
                                allPortLocationsValid = false;
                                break;
                            }
                        }
                        if (!allPortLocationsValid)
                        {
                            Console.WriteLine("Invalid location for port.");
                            i--;
                            break;
                        }

                        // only check validity of number if resource is not none
                        if (resource != "n")
                        {
                            string number = splitChunk[3];
                            int n;
                            int.TryParse(number, out n);
                            if (n < 2 || n > 12)
                            {
                                Console.WriteLine("Dice roll is not a valid number.");
                                i--;
                                break;
                            }
                        }
                    }
                }

                // if i has not been changed, this line is good and can be added
                if (i == currentI)
                {
                    if (i != rows - 1)
                        output += line + "\n";
                }
            }

            // write output string to txt file
            Console.WriteLine("Great, this is what we'll write to file:");
            Console.WriteLine(output);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ok, give me a file name to store this in (just the name... I'll add on the .txt at the end):");
            string fileName = Console.ReadLine();
            File.WriteAllText(fileName + ".txt", output);
            Console.WriteLine("Done! Come back anytime! :D");
        }
    }
}
