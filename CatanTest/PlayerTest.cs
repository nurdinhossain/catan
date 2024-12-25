using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;

namespace CatanTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void MakeValidTrade()
        {
            Player one = new Player(0);
            Player two = new Player(1);

            // make player one hand
            one.AddResource(Resource.Wool, 2);
            one.AddResource(Resource.Ore, 3);
            Assert.AreEqual(5, one.HandSize());
            Assert.AreEqual(2, one.ResourceCount(Resource.Wool));
            Assert.AreEqual(3, one.ResourceCount(Resource.Ore));

            // make player two hand
            two.AddResource(Resource.Brick, 3);
            two.AddResource(Resource.Lumber, 3);
            Assert.AreEqual(6, two.HandSize());
            Assert.AreEqual(3, two.ResourceCount(Resource.Brick));
            Assert.AreEqual(3, two.ResourceCount(Resource.Lumber));

            // make a valid trade
            bool trade = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 1 }, new int[] { 1, 0, 0, 0, 0 });
            Assert.AreEqual(true, trade);

            // verify post conditions
            Assert.AreEqual(5, one.HandSize());
            Assert.AreEqual(1, one.ResourceCount(Resource.Wool));
            Assert.AreEqual(3, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(1, one.ResourceCount(Resource.Brick));

            Assert.AreEqual(6, two.HandSize());
            Assert.AreEqual(2, two.ResourceCount(Resource.Brick));
            Assert.AreEqual(3, two.ResourceCount(Resource.Lumber));
            Assert.AreEqual(1, two.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void TradeIncorrectArraySizes()
        {
            Player one = new Player(0);
            Player two = new Player(1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 1, 0, 0, 0 }, new int[] { 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 1, 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 });
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void TradeNothing()
        {
            Player one = new Player(0);
            Player two = new Player(1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 1, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 });
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void TradeLikeResources()
        {
            Player one = new Player(0);
            Player two = new Player(1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            // make player two hand
            two.AddResource(Resource.Brick, 4);
            two.AddResource(Resource.Lumber, 4);
            two.AddResource(Resource.Ore, 4);
            two.AddResource(Resource.Grain, 4);
            two.AddResource(Resource.Wool, 4);

            // trade like resources
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0,  2, 0, 0}, new int[] { 0, 0, 2, 0, 0 }); // false
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 0, 0 }, new int[] { 0, 0, 1, 0, 0 }); // false
            bool trade3 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 0, 0 }, new int[] { 0, 0, 2, 0, 1 }); // false 
            bool trade5 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 0, 4 }, new int[] { 0, 0, 2, 0, 2 }); // false
            bool trade6 = one.TradeWithPlayer(two, new int[] { 1, 2, 1, 0, 4 }, new int[] { 1, 1, 1, 0, 0 }); // false
            bool trade7 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 0, 0 }, new int[] { 0, 0, 2, 1, 0 }); // false
            bool trade11 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 2, 1 }, new int[] { 1, 0, 2, 2, 1 }); // false
            bool trade13 = one.TradeWithPlayer(two, new int[] { 1, 0, 3, 2, 1 }, new int[] { 0, 0, 2, 1, 1 }); // false
            bool trade14 = one.TradeWithPlayer(two, new int[] { 0, 0, 1, 2, 1 }, new int[] { 1, 0, 2, 2, 1 }); // false

            bool trade4 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 0, 0 }, new int[] { 1, 0, 1, 0, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 1, 0, 1, 0, 0 }, new int[] { 0, 0, 2, 0, 0 }));
            bool trade8 = one.TradeWithPlayer(two, new int[] { 2, 1, 2, 0, 0 }, new int[] { 1, 0, 0, 1, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 1, 0, 0, 1, 0 }, new int[] { 2, 1, 2, 0, 0 }));
            bool trade9 = one.TradeWithPlayer(two, new int[] { 1, 1, 1, 0, 0 }, new int[] { 0, 0, 1, 1, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 0, 1, 1, 1 }, new int[] { 1, 1, 1, 0, 0 }));
            bool trade10 = one.TradeWithPlayer(two, new int[] { 2, 1, 1, 0, 0 }, new int[] { 2, 1, 0, 1, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 2, 1, 0, 1, 0 }, new int[] { 2, 1, 1, 0, 0 }));
            bool trade12 = one.TradeWithPlayer(two, new int[] { 0, 0, 3, 2, 1 }, new int[] { 1, 0, 2, 1, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 1, 0, 2, 1, 1 }, new int[] { 0, 0, 3, 2, 1 }));
            bool trade15 = one.TradeWithPlayer(two, new int[] { 0, 0, 2, 2, 1 }, new int[] { 1, 0, 1, 2, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 1, 0, 1, 2, 1 }, new int[] { 0, 0, 2, 2, 1 }));

            // verify
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
            Assert.AreEqual(false, trade3);
            Assert.AreEqual(true, trade4);
            Assert.AreEqual(false, trade5);
            Assert.AreEqual(false, trade6);
            Assert.AreEqual(false, trade7);
            Assert.AreEqual(true, trade8);
            Assert.AreEqual(true, trade9);
            Assert.AreEqual(true, trade10);
            Assert.AreEqual(false, trade11);
            Assert.AreEqual(true, trade12);
            Assert.AreEqual(false, trade13);
            Assert.AreEqual(false, trade14);
            Assert.AreEqual(true, trade15);
        }

        [TestMethod]
        public void TradeInsufficientResources()
        {
            Player one = new Player(0);
            Player two = new Player(1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            // make player two hand
            two.AddResource(Resource.Brick, 4);
            two.AddResource(Resource.Lumber, 4);
            two.AddResource(Resource.Ore, 4);
            two.AddResource(Resource.Grain, 4);
            two.AddResource(Resource.Wool, 4);

            // trade w/ insufficient resources
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 5, 0, 0 }, new int[] { 0, 2, 0, 0, 0 }); // false
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 1, 0 }, new int[] { 0, 0, 0, 0, 8 }); // false

            // verify 
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void RobSuccess()
        {
            Player one = new Player(0);
            Player two = new Player(1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);

            // make player two hand
            two.AddResource(Resource.Grain, 4);
            two.AddResource(Resource.Wool, 4);

            for (int k = 0; k < 10000; k++)
            {
                // player one robs player two
                one.RobPlayer(two);

                // assert new hand sizes
                Assert.AreEqual(9, one.HandSize());
                Assert.AreEqual(7, two.HandSize());

                // ensure there are no negative resources
                for (int i = 0; i < Enum.GetNames(typeof(Resource)).Length; i++)
                {
                    Assert.IsTrue(two.ResourceCount((Resource)i) >= 0);
                }

                // player two robs player one
                two.RobPlayer(one);

                // assert new hand sizes
                Assert.AreEqual(8, one.HandSize());
                Assert.AreEqual(8, two.HandSize());

                // ensure there are no negative resources
                for (int i = 0; i < Enum.GetNames(typeof(Resource)).Length; i++)
                {
                    Assert.IsTrue(one.ResourceCount((Resource)i) >= 0);
                }
            }
        }

        [TestMethod]
        public void RobFail()
        {
            Player one = new Player(0);
            Player two = new Player(1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            Assert.AreEqual(0, two.HandSize());

            // attempt to rob empty hand
            one.RobPlayer(two);

            // assert hand size is still 0 
            Assert.AreEqual(0, two.HandSize());
        }
    }
}
