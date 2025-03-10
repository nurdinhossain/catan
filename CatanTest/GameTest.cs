using Catan;

namespace CatanTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void StandardMapWater()
        {
            Game game = new Game("standard_map.txt");

            // check water tiles
            Assert.AreEqual(null, game.TileAt(0, 0));
            Assert.AreEqual(null, game.TileAt(0, 4));
            Assert.AreEqual(null, game.TileAt(1, 4));
            Assert.AreEqual(null, game.TileAt(3, 4));
            Assert.AreEqual(null, game.TileAt(4, 0));
            Assert.AreEqual(null, game.TileAt(4, 4));
        }

        [TestMethod]
        public void StandardMapFirstRow()
        {
            Game game = new Game("standard_map.txt");

            // check tiles in first row
            Tile tileOne = game.TileAt(0, 1);
            Tile tileTwo = game.TileAt(0, 2);
            Tile tileThree = game.TileAt(0, 3);

            Assert.AreEqual(Resource.Ore, tileOne.Resource);
            Assert.AreEqual(10, tileOne.Number);
            Assert.AreEqual(Port.AnyPort, tileOne.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.AnyPort, tileOne.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Wool, tileTwo.Resource);
            Assert.AreEqual(2, tileTwo.Number);
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.GrainPort, tileTwo.PortAt(Vertex.Top));
            Assert.AreEqual(Port.GrainPort, tileTwo.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Lumber, tileThree.Resource);
            Assert.AreEqual(9, tileThree.Number);
            Assert.AreEqual(Port.GrainPort, tileThree.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.OrePort, tileThree.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomLeft));
        }

        [TestMethod]
        public void StandardMapSecondRow()
        {
            Game game = new Game("standard_map.txt");

            // check tiles in second row
            Tile tileOne = game.TileAt(1, 0);
            Tile tileTwo = game.TileAt(1, 1);
            Tile tileThree = game.TileAt(1, 2);
            Tile tileFour = game.TileAt(1, 3);

            Assert.AreEqual(Resource.Grain, tileOne.Resource);
            Assert.AreEqual(12, tileOne.Number);
            Assert.AreEqual(Port.LumberPort, tileOne.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.LumberPort, tileOne.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Brick, tileTwo.Resource);
            Assert.AreEqual(6, tileTwo.Number);
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Wool, tileThree.Resource);
            Assert.AreEqual(4, tileThree.Number);
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Brick, tileFour.Resource);
            Assert.AreEqual(10, tileFour.Number);
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.OrePort, tileFour.PortAt(Vertex.Top));
            Assert.AreEqual(Port.OrePort, tileFour.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.BottomLeft));
        }

        [TestMethod]
        public void StandardMapThirdRow()
        {
            Game game = new Game("standard_map.txt");

            // check tiles in second row
            Tile tileOne = game.TileAt(2, 0);
            Tile tileTwo = game.TileAt(2, 1);
            Tile tileThree = game.TileAt(2, 2);
            Tile tileFour = game.TileAt(2, 3);
            Tile tileFive = game.TileAt(2, 4);

            Assert.AreEqual(Resource.Grain, tileOne.Resource);
            Assert.AreEqual(9, tileOne.Number);
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.LumberPort, tileOne.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.BrickPort, tileOne.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Lumber, tileTwo.Resource);
            Assert.AreEqual(11, tileTwo.Number);
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.NoResource, tileThree.Resource);
            Assert.AreEqual(-1, tileThree.Number);
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Lumber, tileFour.Resource);
            Assert.AreEqual(3, tileFour.Number);
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Ore, tileFive.Resource);
            Assert.AreEqual(8, tileFive.Number);
            Assert.AreEqual(Port.NoPort, tileFive.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileFive.PortAt(Vertex.Top));
            Assert.AreEqual(Port.AnyPort, tileFive.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.AnyPort, tileFive.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileFive.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileFive.PortAt(Vertex.BottomLeft));
        }

        [TestMethod]
        public void StandardMapFourthRow()
        {
            Game game = new Game("standard_map.txt");

            // check tiles in second row
            Tile tileOne = game.TileAt(3, 0);
            Tile tileTwo = game.TileAt(3, 1);
            Tile tileThree = game.TileAt(3, 2);
            Tile tileFour = game.TileAt(3, 3);

            Assert.AreEqual(Resource.Lumber, tileOne.Resource);
            Assert.AreEqual(8, tileOne.Number);
            Assert.AreEqual(Port.BrickPort, tileOne.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.BrickPort, tileOne.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Ore, tileTwo.Resource);
            Assert.AreEqual(3, tileTwo.Number);
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Grain, tileThree.Resource);
            Assert.AreEqual(4, tileThree.Number);
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Wool, tileFour.Resource);
            Assert.AreEqual(5, tileFour.Number);
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.WoolPort, tileFour.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.WoolPort, tileFour.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileFour.PortAt(Vertex.BottomLeft));
        }

        [TestMethod]
        public void StandardMapFifthRow()
        {
            Game game = new Game("standard_map.txt");

            // check tiles in first row
            Tile tileOne = game.TileAt(4, 1);
            Tile tileTwo = game.TileAt(4, 2);
            Tile tileThree = game.TileAt(4, 3);

            Assert.AreEqual(Resource.Brick, tileOne.Resource);
            Assert.AreEqual(5, tileOne.Number);
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileOne.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.AnyPort, tileOne.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.AnyPort, tileOne.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Grain, tileTwo.Resource);
            Assert.AreEqual(6, tileTwo.Number);
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.Top));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.AnyPort, tileTwo.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.AnyPort, tileTwo.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.NoPort, tileTwo.PortAt(Vertex.BottomLeft));

            Assert.AreEqual(Resource.Wool, tileThree.Resource);
            Assert.AreEqual(11, tileThree.Number);
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.TopLeft));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Top));
            Assert.AreEqual(Port.WoolPort, tileThree.PortAt(Vertex.TopRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.BottomRight));
            Assert.AreEqual(Port.NoPort, tileThree.PortAt(Vertex.Bottom));
            Assert.AreEqual(Port.AnyPort, tileThree.PortAt(Vertex.BottomLeft));
        }

        [TestMethod]
        public void StandardMapRobberTileInitialized()
        {
            Game game = new Game("standard_map.txt");

            Assert.AreEqual(game.TileAt(2, 2), game.GetRobberTile());
        }

        [TestMethod]
        public void GetNeighborTestEdgeOne()
        {
            Game game = new Game("standard_map.txt");

            Assert.AreEqual(Resource.Grain, game.GetNeighbor(2, 0, Side.TopRight).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(2, 0, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(2, 0, Side.Right).Resource);
            Assert.AreEqual(null, game.GetNeighbor(2, 0, Side.TopLeft));
            Assert.AreEqual(null, game.GetNeighbor(2, 0, Side.Left));
            Assert.AreEqual(null, game.GetNeighbor(2, 0, Side.BottomLeft));

            Assert.AreEqual(Resource.Ore, game.GetNeighbor(1, 0, Side.TopRight).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(1, 0, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(1, 0, Side.Right).Resource);
            Assert.AreEqual(null, game.GetNeighbor(1, 0, Side.TopLeft));
            Assert.AreEqual(null, game.GetNeighbor(1, 0, Side.Left));
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(1, 0, Side.BottomLeft).Resource);

            Assert.AreEqual(null, game.GetNeighbor(0, 1, Side.TopRight));
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(0, 1, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(0, 1, Side.Right).Resource);
            Assert.AreEqual(null, game.GetNeighbor(0, 1, Side.TopLeft));
            Assert.AreEqual(null, game.GetNeighbor(0, 1, Side.Left));
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(0, 1, Side.BottomLeft).Resource);

            Assert.AreEqual(null, game.GetNeighbor(0, 2, Side.TopRight));
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(0, 2, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(0, 2, Side.Right).Resource);
            Assert.AreEqual(null, game.GetNeighbor(0, 2, Side.TopLeft));
            Assert.AreEqual(Resource.Ore, game.GetNeighbor(0, 2, Side.Left).Resource);
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(0, 2, Side.BottomLeft).Resource);

            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(3, 0, Side.TopRight).Resource);
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(3, 0, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Ore, game.GetNeighbor(3, 0, Side.Right).Resource);
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(3, 0, Side.TopLeft).Resource);
            Assert.AreEqual(null, game.GetNeighbor(3, 0, Side.Left));
            Assert.AreEqual(null, game.GetNeighbor(3, 0, Side.BottomLeft));

            Assert.AreEqual(Resource.Ore, game.GetNeighbor(4, 1, Side.TopRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 1, Side.BottomRight));
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(4, 1, Side.Right).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(4, 1, Side.TopLeft).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 1, Side.Left));
            Assert.AreEqual(null, game.GetNeighbor(4, 1, Side.BottomLeft));
        }

        [TestMethod]
        public void GetNeighborTestEdgeTwo()
        {
            Game game = new Game("standard_map.txt");

            Assert.AreEqual(null, game.GetNeighbor(0, 3, Side.TopRight));
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(0, 3, Side.BottomRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(0, 3, Side.Right));
            Assert.AreEqual(null, game.GetNeighbor(0, 3, Side.TopLeft));
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(0, 3, Side.Left).Resource);
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(0, 3, Side.BottomLeft).Resource);

            Assert.AreEqual(null, game.GetNeighbor(1, 3, Side.TopRight));
            Assert.AreEqual(Resource.Ore, game.GetNeighbor(1, 3, Side.BottomRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(1, 3, Side.Right));
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(1, 3, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(1, 3, Side.Left).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(1, 3, Side.BottomLeft).Resource);

            Assert.AreEqual(null, game.GetNeighbor(2, 4, Side.TopRight));
            Assert.AreEqual(null, game.GetNeighbor(2, 4, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(2, 4, Side.Right));
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(2, 4, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(2, 4, Side.Left).Resource);
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(2, 4, Side.BottomLeft).Resource);

            Assert.AreEqual(Resource.Ore, game.GetNeighbor(3, 3, Side.TopRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(3, 3, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(3, 3, Side.Right));
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(3, 3, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(3, 3, Side.Left).Resource);
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(3, 3, Side.BottomLeft).Resource);

            Assert.AreEqual(Resource.Wool, game.GetNeighbor(4, 3, Side.TopRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 3, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(4, 3, Side.Right));
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(4, 3, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(4, 3, Side.Left).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 3, Side.BottomLeft));

            Assert.AreEqual(Resource.Grain, game.GetNeighbor(4, 2, Side.TopRight).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 2, Side.BottomRight));
            Assert.AreEqual(Resource.Wool, game.GetNeighbor(4, 2, Side.Right).Resource);
            Assert.AreEqual(Resource.Ore, game.GetNeighbor(4, 2, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(4, 2, Side.Left).Resource);
            Assert.AreEqual(null, game.GetNeighbor(4, 2, Side.BottomLeft));
        }

        [TestMethod]
        public void GetNeighborTestMiddle()
        {
            Game game = new Game("standard_map.txt");

            Assert.AreEqual(Resource.Wool, game.GetNeighbor(2, 2, Side.TopRight).Resource);
            Assert.AreEqual(Resource.Grain, game.GetNeighbor(2, 2, Side.BottomRight).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(2, 2, Side.Right).Resource);
            Assert.AreEqual(Resource.Brick, game.GetNeighbor(2, 2, Side.TopLeft).Resource);
            Assert.AreEqual(Resource.Lumber, game.GetNeighbor(2, 2, Side.Left).Resource);
            Assert.AreEqual(Resource.Ore, game.GetNeighbor(2, 2, Side.BottomLeft).Resource);
        }

        [TestMethod]
        public void GetNeighborTestBoundary()
        {
            Game game = new Game("standard_map.txt");

            Assert.AreEqual(null, game.GetNeighbor(0, 0, Side.TopRight));
            Assert.AreEqual(null, game.GetNeighbor(0, 0, Side.TopLeft));
            Assert.AreEqual(null, game.GetNeighbor(0, 0, Side.Left));

            Assert.AreEqual(null, game.GetNeighbor(0, 1, Side.TopRight));
            Assert.AreEqual(null, game.GetNeighbor(0, 1, Side.TopLeft));

            Assert.AreEqual(null, game.GetNeighbor(0, 4, Side.TopRight));
            Assert.AreEqual(null, game.GetNeighbor(0, 4, Side.TopLeft));
            Assert.AreEqual(null, game.GetNeighbor(0, 4, Side.Right));

            Assert.AreEqual(null, game.GetNeighbor(1, 4, Side.TopRight));
            Assert.AreEqual(null, game.GetNeighbor(1, 4, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(1, 4, Side.Right));

            Assert.AreEqual(null, game.GetNeighbor(4, 4, Side.BottomLeft));
            Assert.AreEqual(null, game.GetNeighbor(4, 4, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(4, 4, Side.Right));

            Assert.AreEqual(null, game.GetNeighbor(4, 3, Side.BottomLeft));
            Assert.AreEqual(null, game.GetNeighbor(4, 3, Side.BottomRight));

            Assert.AreEqual(null, game.GetNeighbor(4, 0, Side.BottomLeft));
            Assert.AreEqual(null, game.GetNeighbor(4, 0, Side.BottomRight));
            Assert.AreEqual(null, game.GetNeighbor(4, 0, Side.Left));

            Assert.AreEqual(null, game.GetNeighbor(3, 0, Side.Left));
        }

        [TestMethod]
        public void CanBuildTrueTypeOne()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // type 1 
            game.TileAt(1, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(1, 0).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(1, 0).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(1, 0).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(0, 1).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.Top, Building.Settlement);

            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 0, Vertex.TopRight));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 0, 1, Vertex.Bottom));
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 0, Vertex.TopRight));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 0, 1, Vertex.Bottom));
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 1, 0, Vertex.TopRight));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 0, 1, Vertex.Bottom));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildTrueTypeTwo()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // type 2
            game.TileAt(3, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(3, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(3, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);

            game.TileAt(3, 0).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(3, 0).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(3, 0).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);

            game.TileAt(4, 1).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(4, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(4, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(3, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(3, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(3, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(3, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 4, 1, Vertex.Top));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 1, Vertex.BottomLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(3, 1).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(3, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(4, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(3, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(4, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 4, 1, Vertex.Top));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 1, Vertex.BottomLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(4, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(3, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(4, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(3, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(4, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildSettlementAt(player, 4, 1, Vertex.Top));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 1, Vertex.BottomLeft));
            Assert.IsTrue(game.CanBuildSettlementAt(player, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(4, 1).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildFalseTypeOneWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            // type 1 
            game.TileAt(1, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(1, 0).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(1, 0).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(1, 0).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(0, 1).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.Top, Building.Settlement);

            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 1, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 0, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 0, 1, Vertex.Bottom));
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 1, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 0, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 0, 1, Vertex.Bottom));
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 1, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 1, 0, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 0, 1, Vertex.Bottom));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildFalseTypeTwoWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            // type 2
            game.TileAt(3, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(3, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(3, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);

            game.TileAt(3, 0).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(3, 0).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(3, 0).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);

            game.TileAt(4, 1).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(4, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(4, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);

            game.TileAt(3, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(3, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(3, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(3, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 4, 1, Vertex.Top));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(3, 1).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(3, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(4, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(3, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(4, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 4, 1, Vertex.Top));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(4, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(3, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(4, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(3, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(4, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 4, 1, Vertex.Top));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(playerTwo, 3, 0, Vertex.BottomRight));
            game.TileAt(3, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(4, 1).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]  
        public void CanBuildFalseNull()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 0, 0, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 4, 4, Vertex.BottomRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 4, Vertex.TopRight));
        }

        [TestMethod]
        public void CanBuildFalseHasBuilding()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            game.TileAt(1, 1).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopLeft));
        }

        [TestMethod]
        public void CanBuildTypeOneCaseOneFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 1
            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopRight, player);
            game.TileAt(0, 2).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 2, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 0, 2, Vertex.Bottom));

        }

        [TestMethod]
        public void CanBuildTypeOneCaseTwoFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 2
            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.TopLeft, player);
            game.TileAt(0, 2).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(1, 2).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(0, 3).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 2, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 0, 2, Vertex.Bottom));
        }

        [TestMethod]
        public void CanBuildTypeOneCaseThreeFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 3
            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.Left, player);
            game.TileAt(1, 2).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(2, 2).SetBuildingAt(Vertex.Top, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.TopRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 2, Vertex.TopLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 0, 2, Vertex.Bottom));
        }

        [TestMethod]
        public void CanBuildTypeTwoCaseOneFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 1
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            game.TileAt(1, 0).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(1, 1).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(0, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 0, Vertex.BottomRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 2, 1, Vertex.Top));
        }

        [TestMethod]
        public void CanBuildTypeTwoCaseTwoFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 2
            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopLeft, player);
            game.TileAt(1, 1).SetBuildingAt(Vertex.Bottom, Building.Settlement);
            game.TileAt(2, 1).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(2, 2).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 0, Vertex.BottomRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 2, 1, Vertex.Top));
        }

        [TestMethod]
        public void CanBuildTypeTwoCaseThreeFalse()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // case 3
            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopRight, player);
            game.TileAt(1, 0).SetBuildingAt(Vertex.Bottom, Building.Settlement);
            game.TileAt(2, 0).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(2, 1).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 0, Vertex.BottomRight));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 1, 1, Vertex.BottomLeft));
            Assert.IsFalse(game.CanBuildSettlementAt(player, 2, 1, Vertex.Top));
        }

        [TestMethod]
        public void CanBuildRoadFromBuilding()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            // type 1
            game.TileAt(0, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.Top, player);
            game.TileAt(0, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.BottomRight, player);
            game.TileAt(0, 1).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.BottomLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.TopRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.TopLeft));

            // type 2
            game.TileAt(0, 3).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.TopRight, player);
            game.TileAt(0, 3).SetBuildingAt(Vertex.Bottom, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.Bottom, player);
            game.TileAt(0, 3).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.TopRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 3, Side.TopLeft));
        }

        [TestMethod]
        public void CanBuildRoadTopRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(0, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 2, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopRight));
            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 2, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopRight));
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 2, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopRight));
            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 2, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopRight));
            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 2, Side.Left));
            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 2, Side.Left));
            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 2, Side.Left));
            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Right));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 2, Side.Left));
            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadBottomRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 2, Side.TopLeft));
            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 2, Side.TopLeft));
            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 2, Side.TopLeft));
            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(2, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomRight));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 2, Side.TopLeft));
            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadBottomLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 1, Side.TopRight));
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 1, Side.TopRight));
            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 1, Side.TopRight));
            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(2, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.BottomLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 2, 1, Side.TopRight));
            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 0, Side.Right));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 0, Side.Right));
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 0, Side.Right));
            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.Left));
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 0, Side.Right));
            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadTopLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomRight));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(0, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomRight));
            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomRight));
            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsTrue(game.CanBuildRoadAt(player, 1, 1, Side.TopLeft));
            Assert.IsTrue(game.CanBuildRoadAt(player, 0, 1, Side.BottomRight));
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadTopRightWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(0, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 2, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopRight));
            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 2, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopRight));
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 2, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopRight));
            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 2, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopRight));
            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadRightWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 2, Side.Left));
            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 2, Side.Left));
            game.TileAt(0, 2).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 2, Side.Left));
            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 2, Side.Left));
            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadBottomRightWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 2, Side.TopLeft));
            game.TileAt(1, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 2, Side.TopLeft));
            game.TileAt(1, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 2, Side.TopLeft));
            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(2, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 2, Side.TopLeft));
            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadBottomLeftWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 1, Side.TopRight));
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 1, Side.TopRight));
            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 1, Side.TopRight));
            game.TileAt(1, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(2, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(2, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 2, 1, Side.TopRight));
            game.TileAt(2, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(2, 2).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadLeftWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 0, Side.Right));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 0, Side.Right));
            game.TileAt(0, 1).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.BottomRight, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 0, Side.Right));
            game.TileAt(1, 0).SetRoadAt(Side.BottomRight, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopLeft, Road.NoRoad);

            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(2, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 0, Side.Right));
            game.TileAt(1, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(2, 1).SetRoadAt(Side.TopRight, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadTopLeftWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 0).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomRight));
            game.TileAt(0, 1).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(0, 1).SetPlayerAtSide(Side.Right, player);
            game.TileAt(0, 2).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomRight));
            game.TileAt(0, 1).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(0, 2).SetRoadAt(Side.Left, Road.NoRoad);

            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.Road);
            game.TileAt(0, 2).SetPlayerAtSide(Side.BottomLeft, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopRight, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomRight));
            game.TileAt(0, 2).SetRoadAt(Side.BottomLeft, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.TopRight, Road.NoRoad);

            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.Road);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.Road);
            game.TileAt(1, 0).SetPlayerAtSide(Side.Right, player);
            game.TileAt(1, 1).SetPlayerAtSide(Side.Left, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 1, 1, Side.TopLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomRight));
            game.TileAt(1, 0).SetRoadAt(Side.Right, Road.NoRoad);
            game.TileAt(1, 1).SetRoadAt(Side.Left, Road.NoRoad);
        }

        [TestMethod]
        public void CanBuildRoadFromBuildingWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            // type 1
            game.TileAt(0, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.Top, player);
            game.TileAt(0, 1).SetBuildingAt(Vertex.BottomRight, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.BottomRight, player);
            game.TileAt(0, 1).SetBuildingAt(Vertex.BottomLeft, Building.Settlement);
            game.TileAt(0, 1).SetPlayerAtVertex(Vertex.BottomLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.TopRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 1, Side.TopLeft));

            // type 2
            game.TileAt(0, 3).SetBuildingAt(Vertex.TopRight, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.TopRight, player);
            game.TileAt(0, 3).SetBuildingAt(Vertex.Bottom, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.Bottom, player);
            game.TileAt(0, 3).SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            game.TileAt(0, 3).SetPlayerAtVertex(Vertex.TopLeft, player);
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.TopRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.Right));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.BottomRight));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.BottomLeft));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.Left));
            Assert.IsFalse(game.CanBuildRoadAt(playerTwo, 0, 3, Side.TopLeft));
        }

        // not thoroughly tested but whatever -__-
        [TestMethod]
        public void CanBuildRoadEnemyBuildingBlock()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            Player playerTwo = new Player(game, 1);

            game.TileAt(1, 1).SetRoadAt(Side.TopLeft, Road.Road);
            game.TileAt(1, 1).SetPlayerAtSide(Side.TopLeft, player);
            game.TileAt(1, 1).SetBuildingAt(Vertex.Top, Building.Settlement);
            game.TileAt(1, 1).SetPlayerAtVertex(Vertex.Top, playerTwo);
            Assert.IsFalse(game.CanBuildRoadAt(player, 1, 1, Side.TopRight));
        }

        [TestMethod]
        public void CanBuildRoadNull()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            Assert.IsFalse(game.CanBuildRoadAt(player, 0, 0, Side.TopRight));
        }

        [TestMethod]
        public void CanBuildRoadExistingRoad()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.TileAt(1, 0).SetRoadAt(Side.TopRight, Road.Road);
            Assert.IsFalse(game.CanBuildRoadAt(player, 1, 0, Side.TopRight));
        }

        [TestMethod]
        public void BuildBuildingTop()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.Top);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.Top));
            Assert.AreEqual(Building.Settlement, game.TileAt(0, 1).BuildingAt(Vertex.BottomRight));
            Assert.AreEqual(Building.Settlement, game.TileAt(0, 2).BuildingAt(Vertex.BottomLeft));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.Top));
            Assert.AreEqual(player, game.TileAt(0, 1).PlayerAtVertex(Vertex.BottomRight));
            Assert.AreEqual(player, game.TileAt(0, 2).PlayerAtVertex(Vertex.BottomLeft));
        }

        [TestMethod]
        public void BuildBuildingTopRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.TopRight);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.TopRight));
            Assert.AreEqual(Building.Settlement, game.TileAt(0, 2).BuildingAt(Vertex.Bottom));
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 2).BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.TopRight));
            Assert.AreEqual(player, game.TileAt(0, 2).PlayerAtVertex(Vertex.Bottom));
            Assert.AreEqual(player, game.TileAt(1, 2).PlayerAtVertex(Vertex.TopLeft));
        }

        [TestMethod]
        public void BuildBuildingBottomRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.BottomRight);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.BottomRight));
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 2).BuildingAt(Vertex.BottomLeft));
            Assert.AreEqual(Building.Settlement, game.TileAt(2, 2).BuildingAt(Vertex.Top));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.BottomRight));
            Assert.AreEqual(player, game.TileAt(1, 2).PlayerAtVertex(Vertex.BottomLeft));
            Assert.AreEqual(player, game.TileAt(2, 2).PlayerAtVertex(Vertex.Top));
        }

        [TestMethod]
        public void BuildBuildingBottom()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.Bottom);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.Bottom));
            Assert.AreEqual(Building.Settlement, game.TileAt(2, 2).BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(Building.Settlement, game.TileAt(2, 1).BuildingAt(Vertex.TopRight));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.Bottom));
            Assert.AreEqual(player, game.TileAt(2, 2).PlayerAtVertex(Vertex.TopLeft));
            Assert.AreEqual(player, game.TileAt(2, 1).PlayerAtVertex(Vertex.TopRight));
        }

        [TestMethod]
        public void BuildBuildingBottomLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.BottomLeft);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.BottomLeft));
            Assert.AreEqual(Building.Settlement, game.TileAt(2, 1).BuildingAt(Vertex.Top));
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 0).BuildingAt(Vertex.BottomRight));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.BottomLeft));
            Assert.AreEqual(player, game.TileAt(2, 1).PlayerAtVertex(Vertex.Top));
            Assert.AreEqual(player, game.TileAt(1, 0).PlayerAtVertex(Vertex.BottomRight));
        }

        [TestMethod]
        public void BuildBuildingTopLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.TopLeft);
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 1).BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(Building.Settlement, game.TileAt(1, 0).BuildingAt(Vertex.TopRight));
            Assert.AreEqual(Building.Settlement, game.TileAt(0, 1).BuildingAt(Vertex.Bottom));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtVertex(Vertex.TopLeft));
            Assert.AreEqual(player, game.TileAt(1, 0).PlayerAtVertex(Vertex.TopRight));
            Assert.AreEqual(player, game.TileAt(0, 1).PlayerAtVertex(Vertex.Bottom));
        }

        [TestMethod]
        public void BuildRoadTopRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.TopRight);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.TopRight));
            Assert.AreEqual(Road.Road, game.TileAt(0, 2).RoadAt(Side.BottomLeft));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.TopRight));
            Assert.AreEqual(player, game.TileAt(0, 2).PlayerAtSide(Side.BottomLeft));
        }

        [TestMethod]
        public void BuildRoadRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.Right);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.Right));
            Assert.AreEqual(Road.Road, game.TileAt(1, 2).RoadAt(Side.Left));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.Right));
            Assert.AreEqual(player, game.TileAt(1, 2).PlayerAtSide(Side.Left));
        }

        [TestMethod]
        public void BuildRoadBottomRight()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.BottomRight);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.BottomRight));
            Assert.AreEqual(Road.Road, game.TileAt(2, 2).RoadAt(Side.TopLeft));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.BottomRight));
            Assert.AreEqual(player, game.TileAt(2, 2).PlayerAtSide(Side.TopLeft));
        }

        [TestMethod]
        public void BuildRoadBottomLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.BottomLeft);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.BottomLeft));
            Assert.AreEqual(Road.Road, game.TileAt(2, 1).RoadAt(Side.TopRight));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.BottomLeft));
            Assert.AreEqual(player, game.TileAt(2, 1).PlayerAtSide(Side.TopRight));
        }

        [TestMethod]
        public void BuildRoadLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.Left);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.Left));
            Assert.AreEqual(Road.Road, game.TileAt(1, 0).RoadAt(Side.Right));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.Left));
            Assert.AreEqual(player, game.TileAt(1, 0).PlayerAtSide(Side.Right));
        }

        [TestMethod]
        public void BuildRoadTopLeft()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);

            game.BuildRoad(player, 1, 1, Side.TopLeft);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.TopLeft));
            Assert.AreEqual(Road.Road, game.TileAt(0, 1).RoadAt(Side.BottomRight));
            Assert.AreEqual(player, game.TileAt(1, 1).PlayerAtSide(Side.TopLeft));
            Assert.AreEqual(player, game.TileAt(0, 1).PlayerAtSide(Side.BottomRight));
        }

        [TestMethod]
        public void MoveRobberSuccess()
        {
            Game game = new Game("standard_map.txt");

            Assert.IsTrue(game.MoveRobber(0, 1));
        }

        [TestMethod]
        public void MoveRobberFailNull()
        {
            Game game = new Game("standard_map.txt");

            Assert.IsFalse(game.MoveRobber(0, 0));
        }

        [TestMethod]
        public void MoveRobberFailSameTile()
        {
            Game game = new Game("standard_map.txt");

            Assert.IsFalse(game.MoveRobber(2, 2));
        }

        [TestMethod]
        public void TestEligibleRoadSpotsOne()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            game.BuildRoad(player, 0, 1, Side.TopRight);
            Assert.AreEqual(3, game.EligibleRoadSpots(player));
        }

        [TestMethod]
        public void TestEligibleRoadSpotsTwo()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            game.BuildRoad(player, 0, 1, Side.TopRight);
            game.BuildBuilding(player, Building.Settlement, 1, 0, Vertex.BottomLeft);
            Assert.AreEqual(6, game.EligibleRoadSpots(player));
        }

        [TestMethod]
        public void TestEligibleRoadSpotsThree()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            game.BuildRoad(player, 2, 4, Side.TopRight);
            game.BuildRoad(player, 2, 4, Side.Right);
            game.BuildRoad(player, 2, 4, Side.BottomRight);
            game.BuildRoad(player, 2, 2, Side.BottomLeft);
            game.BuildBuilding(player, Building.Settlement, 1, 0, Vertex.BottomLeft);
            Assert.AreEqual(11, game.EligibleRoadSpots(player));
        }

        [TestMethod]
        public void TestRollNotSeven()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            game.BuildBuilding(player, Building.Settlement, 1, 1, Vertex.TopRight);
            game.BuildBuilding(player, Building.City, 4, 2, Vertex.Top);
            game.BuildBuilding(player, Building.City, 2, 2, Vertex.Bottom);
            game.RespondToRoll(6);
            Assert.AreEqual(3, player.HandSize());
            Assert.AreEqual(1, player.ResourceCount(Resource.Brick));
            Assert.AreEqual(2, player.ResourceCount(Resource.Grain));
        }

        [TestMethod]
        public void TestRollSeven()
        {
            Game game = new Game("standard_map.txt");
            Player player = new Player(game, 0);
            player.AddResource(Resource.Grain, 6);
            player.AddResource(Resource.Brick, 3);
            game.RespondToRoll(7);
            Assert.IsTrue(game.PlayersMustDiscard());
        }

        [TestMethod]
        public void TestAddRoadPair()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            game.AddRoadPair(2, 2, Side.TopRight, visited);
            game.AddRoadPair(2, 2, Side.Right, visited);
            game.AddRoadPair(2, 2, Side.BottomRight, visited);
            game.AddRoadPair(2, 2, Side.BottomLeft, visited);
            game.AddRoadPair(2, 2, Side.Left, visited);
            game.AddRoadPair(2, 2, Side.TopLeft, visited);

            Assert.AreEqual(1, visited[2, 2, (int)Side.TopRight]);
            Assert.AreEqual(1, visited[2, 2, (int)Side.Right]);
            Assert.AreEqual(1, visited[2, 2, (int)Side.BottomRight]);
            Assert.AreEqual(1, visited[2, 2, (int)Side.BottomLeft]);
            Assert.AreEqual(1, visited[2, 2, (int)Side.Left]);
            Assert.AreEqual(1, visited[2, 2, (int)Side.TopLeft]);

            Assert.AreEqual(1, visited[1, 2, (int)Side.BottomLeft]);
            Assert.AreEqual(1, visited[2, 3, (int)Side.Left]);
            Assert.AreEqual(1, visited[3, 2, (int)Side.TopLeft]);
            Assert.AreEqual(1, visited[3, 1, (int)Side.TopRight]);
            Assert.AreEqual(1, visited[2, 1, (int)Side.Right]);
            Assert.AreEqual(1, visited[1, 1, (int)Side.BottomRight]);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsOne()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 2, 2, Side.TopLeft);
            game.BuildRoad(player, 2, 2, Side.Right);
            game.BuildRoad(player, 1, 2, Side.Left);
            game.BuildRoad(player, 1, 2, Side.BottomRight);
            game.BuildRoad(player, 2, 2, Side.TopRight);
            game.UpdateVisitableNeighbors(2, 2, Side.TopRight, player, neighbors, visited);

            Assert.AreEqual(4, neighbors.Count);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsTwo()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 2, 2, Side.Left);
            game.BuildRoad(player, 2, 2, Side.TopRight);
            game.BuildRoad(player, 1, 1, Side.BottomLeft);
            game.BuildRoad(player, 1, 1, Side.Right);
            game.BuildRoad(player, 2, 2, Side.TopLeft);
            game.UpdateVisitableNeighbors(2, 2, Side.TopLeft, player, neighbors, visited);

            Assert.AreEqual(4, neighbors.Count);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsThree()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 2, 2, Side.Left);
            game.BuildRoad(player, 2, 2, Side.BottomRight);
            game.BuildRoad(player, 3, 1, Side.TopLeft);
            game.BuildRoad(player, 3, 1, Side.Right);
            game.BuildRoad(player, 2, 2, Side.BottomLeft);
            game.UpdateVisitableNeighbors(2, 2, Side.BottomLeft, player, neighbors, visited);

            Assert.AreEqual(4, neighbors.Count);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsWrongPlayer()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            Player player2 = new Player(game, 1);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 2, 2, Side.Left);
            game.BuildRoad(player, 2, 2, Side.BottomRight);
            game.BuildRoad(player, 3, 1, Side.TopLeft);
            game.BuildRoad(player, 3, 1, Side.Right);
            game.BuildRoad(player, 2, 2, Side.BottomLeft);
            game.UpdateVisitableNeighbors(2, 2, Side.BottomLeft, player2, neighbors, visited);

            Assert.AreEqual(0, neighbors.Count);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsVisited()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 2, 2, Side.Left);
            game.BuildRoad(player, 2, 2, Side.BottomRight);
            game.BuildRoad(player, 3, 1, Side.TopLeft);
            game.BuildRoad(player, 3, 1, Side.Right);
            game.AddRoadPair(2, 2, Side.Left, visited);
            game.AddRoadPair(3, 1, Side.Right, visited);
            game.BuildRoad(player, 2, 2, Side.BottomLeft);
            game.UpdateVisitableNeighbors(2, 2, Side.BottomLeft, player, neighbors, visited);

            Assert.AreEqual(2, neighbors.Count);
        }

        [TestMethod]
        public void TestUpdateVistableNeighborsEdge()
        {
            Game game = new Game("standard_map.txt");
            int[,,] visited = new int[5, 5, 6];
            Player player = new Player(game, 0);
            List<(int, int, Side)> neighbors = new List<(int, int, Side)>();

            game.BuildRoad(player, 1, 0, Side.Left);
            game.BuildRoad(player, 1, 0, Side.TopRight);
            game.BuildRoad(player, 0, 1, Side.Left);
            game.BuildRoad(player, 1, 0, Side.TopLeft);
            game.UpdateVisitableNeighbors(1, 0, Side.TopLeft, player, neighbors, visited);

            Assert.AreEqual(3, neighbors.Count);
        }
    }
}
