namespace Catan
{
    public class Game
    {
        // array of Tiles present in game board
        private Tile?[,] _tiles; 

        public Game(string fileName) 
        {
            LoadMap(fileName);
        }

        // getters
        public Tile? TileAt(int row, int col)
        {
            return _tiles[row, col];
        }

        public Tile? GetNeighbor(int row, int col, Side side)
        {
            if (side == Side.Left)
            {
                // if we're on the left side of the board, there's no left pieces
                if (col == 0) return null;
                return _tiles[row, col-1];
            }

            else if (side == Side.Right)
            {
                if (col == _tiles.GetLength(1)-1) return null;
                return _tiles[row, col+1];
            }

            else if (side == Side.TopRight)
            {
                if (row == 0) return null;
                if (col == _tiles.GetLength(1) - 1 && row % 2 == 1) return null; 
                if (row % 2 == 0) return _tiles[row - 1, col];
                
                return _tiles[row - 1, col + 1];
            }
            else if (side == Side.TopLeft)
            {
                if (row == 0) return null;
                if (col == 0 && row % 2 == 0) return null;
                if (row % 2 == 1) return _tiles[row - 1, col];

                return _tiles[row - 1, col - 1];
            }

            else if (side == Side.BottomRight)
            {
                if (row == _tiles.GetLength(0) - 1) return null;
                if (col == _tiles.GetLength(1) - 1 && row % 2 == 1) return null;
                if (row % 2 == 0) return _tiles[row + 1, col];

                return _tiles[row + 1, col + 1];
            }

            else if (side == Side.BottomLeft)
            {
                if (row == _tiles.GetLength(0) - 1) return null;
                if (col == 0 && row % 2 == 0) return null;
                if (row % 2 == 1) return _tiles[row + 1, col];

                return _tiles[row + 1, col - 1];
            }

            return null;
        }
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
