﻿using Catan;

namespace CatanTest
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void TestCanHarvestTrue()
        {
            Tile tile = new Tile(Resource.Grain, 6);
            Assert.AreEqual(true, tile.CanHarvest(6));
        }

        [TestMethod]
        public void TestCanHarvestFalse()
        {
            Tile tile = new Tile(Resource.Grain, 6);
            Assert.AreEqual(false, tile.CanHarvest(2));
        }

        [TestMethod]
        public void TestHarvest()
        {
            Player p = new Player(0);
            Tile tile = new Tile(Resource.Grain, 6);
            tile.SetBuildingAt(Vertex.TopLeft, Building.Settlement);
            tile.SetPlayerAtVertex(Vertex.TopLeft, p);
            tile.Harvest();

            Assert.AreEqual(1, p.ResourceCount(Resource.Grain));

            tile.SetBuildingAt(Vertex.TopRight, Building.City);
            tile.SetPlayerAtVertex(Vertex.TopRight, p);
            tile.Harvest();

            Assert.AreEqual(4, p.ResourceCount(Resource.Grain));
        }
    }
}
