using Catan;

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
    }
}
