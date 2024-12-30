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
    }
}
