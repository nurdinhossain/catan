using Catan;

namespace CatanTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void MakeValidTrade()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);

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
            bool trade = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 0, 1 }, new int[] { 0, 1, 0, 0, 0, 0 });
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
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0 });
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void TradeNothing()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0 });
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void TradeLikeResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);

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
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0,  2, 0, 0}, new int[] { 0, 0, 0, 2, 0, 0 }); // false
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 0, 0 }, new int[] { 0, 0, 0, 1, 0, 0 }); // false
            bool trade3 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 0, 0 }, new int[] { 0, 0, 0, 2, 0, 1 }); // false 
            bool trade5 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 0, 4 }, new int[] { 0, 0, 0, 2, 0, 2 }); // false
            bool trade6 = one.TradeWithPlayer(two, new int[] { 0, 1, 2, 1, 0, 4 }, new int[] { 0, 1, 1, 1, 0, 0 }); // false
            bool trade7 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 0, 0 }, new int[] { 0, 0, 0, 2, 1, 0 }); // false
            bool trade11 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 2, 1 }, new int[] { 0, 1, 0, 2, 2, 1 }); // false
            bool trade13 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 3, 2, 1 }, new int[] { 0, 0, 0, 2, 1, 1 }); // false
            bool trade14 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 1, 2, 1 }, new int[] { 0, 1, 0, 2, 2, 1 }); // false

            bool trade4 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 0, 0 }, new int[] { 0, 1, 0, 1, 0, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 1, 0, 1, 0, 0 }, new int[] { 0, 0, 0, 2, 0, 0 }));
            bool trade8 = one.TradeWithPlayer(two, new int[] { 0, 2, 1, 2, 0, 0 }, new int[] { 0, 1, 0, 0, 1, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 1, 0 }, new int[] { 0, 2, 1, 2, 0, 0 }));
            bool trade9 = one.TradeWithPlayer(two, new int[] { 0, 1, 1, 1, 0, 0 }, new int[] { 0, 0, 0, 1, 1, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 0, 0, 1, 1, 1 }, new int[] { 0, 1, 1, 1, 0, 0 }));
            bool trade10 = one.TradeWithPlayer(two, new int[] { 0, 2, 1, 1, 0, 0 }, new int[] { 0, 2, 1, 0, 1, 0 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 2, 1, 0, 1, 0 }, new int[] { 0, 2, 1, 1, 0, 0 }));
            bool trade12 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 3, 2, 1 }, new int[] { 0, 1, 0, 2, 1, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 1, 0, 2, 1, 1 }, new int[] { 0, 0, 0, 3, 2, 1 }));
            bool trade15 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 2, 2, 1 }, new int[] { 0, 1, 0, 1, 2, 1 }); // true
            Assert.AreEqual(true, one.TradeWithPlayer(two, new int[] { 0, 1, 0, 1, 2, 1 }, new int[] { 0, 0, 0, 2, 2, 1 }));

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
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);

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
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 5, 0, 0 }, new int[] { 0, 0, 2, 0, 0, 0 }); // false
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 1, 0 }, new int[] { 0, 0, 0, 0, 0, 8 }); // false

            // verify 
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void RobSuccess()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);

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
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            Assert.AreEqual(0, two.HandSize());

            // attempt to rob empty hand
            one.RobPlayer(two);

            // assert hand size is still 0 
            Assert.AreEqual(0, two.HandSize());
        }

        [TestMethod]
        public void BuildSettlementSuccess()
        {
            Game game = new Game("standard_map.txt");
            Bank bank = game.GetBank();
            Player one = new Player(game, 0);
            Tile tile = game.TileAt(1, 1);
            tile.SetPortAt(Vertex.TopLeft, Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            // create settlement
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(Building.Settlement, tile.BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(one, tile.PlayerAtVertex(Vertex.TopLeft));
            Assert.AreEqual(4, one.Settlements);
            Assert.AreEqual(1, one.VictoryPoints);
            Assert.AreEqual(true, one.GetPort(Port.AnyPort));

            Assert.AreEqual(12, one.HandSize());
            Assert.AreEqual(3, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(3, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(3, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(3, one.ResourceCount(Resource.Wool));
            Assert.AreEqual(one, tile.PlayerAtVertex(Vertex.TopLeft));

            Assert.AreEqual(20, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Lumber));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Grain));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void BuildSettlementNoneLeft()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            one.Settlements = 0;
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(0, one.Settlements);
        }

        [TestMethod]
        public void BuildSettlementOnBuilding()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            game.BuildBuilding(one, Building.Settlement, 1, 1, Vertex.TopLeft);
            game.BuildRoad(one, 1, 1, Side.TopLeft);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.BuildSettlement(1, 1, Vertex.TopLeft));
            game.BuildBuilding(one, Building.City, 1, 1, Vertex.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(5, one.Settlements);
        }

        [TestMethod]
        public void BuildSettlementInsufficientResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);

            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(5, one.Settlements);
        }

        [TestMethod]
        public void BuildCitySuccess()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Tile tile = game.TileAt(1, 1);
            Bank bank = game.GetBank();

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Wool, 4);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(19, one.HandSize());
            Assert.AreEqual(true, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(15, one.HandSize());

            // create city
            Assert.AreEqual(true, one.BuildCity(1, 1, Vertex.TopLeft));
            Assert.AreEqual(one, tile.PlayerAtVertex(Vertex.TopLeft));
            Assert.AreEqual(Building.City, tile.BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(5, one.Settlements);
            Assert.AreEqual(3, one.Cities);
            Assert.AreEqual(2, one.VictoryPoints);

            Assert.AreEqual(10, one.HandSize());
            Assert.AreEqual(3, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(3, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(0, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(1, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(3, one.ResourceCount(Resource.Wool));
            Assert.AreEqual(one, tile.PlayerAtVertex(Vertex.TopLeft));

            Assert.AreEqual(20, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Lumber));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Wool));
            Assert.AreEqual(22, bank.ResourceCount(Resource.Grain));
            Assert.AreEqual(22, bank.ResourceCount(Resource.Ore));
        }

        [TestMethod]
        public void BuildCityNoneLeft()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.Cities = 0;

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Wool, 4);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(false, one.BuildCity(1, 1, Vertex.TopLeft));
        }

        [TestMethod]
        public void BuiltCityOnWrongBuilding()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Grain, 8);
            one.AddResource(Resource.Ore, 8);
            one.AddResource(Resource.Wool, 8);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildCity(1, 1, Vertex.TopLeft));
            Assert.AreEqual(true, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(true, one.BuildCity(1, 1, Vertex.TopLeft));
            Assert.AreEqual(false, one.BuildCity(1, 1, Vertex.TopLeft));
        }

        [TestMethod]
        public void BuiltCityInsufficientResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 2);
            one.AddResource(Resource.Lumber, 2);
            one.AddResource(Resource.Grain, 2);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 1);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(1, 1, Vertex.TopLeft));
            Assert.AreEqual(false, one.BuildCity(1, 1, Vertex.TopLeft));
        }

        [TestMethod]
        public void BuildRoadSuccess()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            one.AddResource(Resource.Brick, 2);
            one.AddResource(Resource.Lumber, 2);

            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.IsTrue(one.BuildRoad(1, 1, Side.TopRight));
            Assert.AreEqual(1, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(1, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(14, one.Roads);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.TopRight));
        }

        [TestMethod]
        public void DrawDevCardSuccess()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Bank bank = game.GetBank();

            // make player one hand
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 3);

            // draw card
            Assert.AreEqual(true, one.DrawDevCard());
            Assert.AreEqual(1, one.NumTempDevCards());
            Assert.AreEqual(0, one.NumPermanentDevCards());
            Assert.AreEqual(2, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(2, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(2, one.ResourceCount(Resource.Wool));

            Assert.AreEqual(20, bank.ResourceCount(Resource.Grain));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Ore));
            Assert.AreEqual(20, bank.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void DrawDevCardNoCards()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            for (int i = 0; i < 25; i++)
            {
                game.GetDevDeck().Draw();
            }

            // make player one hand
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 3);

            // draw card 
            Assert.AreEqual(false, one.DrawDevCard());
        }

        [TestMethod]
        public void DrawDevCardInsufficientResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Grain, 1);
            one.AddResource(Resource.Ore, 0);
            one.AddResource(Resource.Wool, 2);

            // draw card 
            Assert.AreEqual(false, one.DrawDevCard());
        }

        [TestMethod]
        public void ExchangeBankSuccessNoPorts()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Bank bank = game.GetBank();

            // make player one hand
            one.AddResource(Resource.Brick, 8);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 0, 0, 0, 0 }, new int[] { 0, 0, 2, 0, 0, 0 }));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 2, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0 }));
            Assert.AreEqual(2, one.HandSize());
            Assert.AreEqual(0, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(2, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(27, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(17, bank.ResourceCount(Resource.Lumber));
        }

        [TestMethod]
        public void ExchangeBankSuccessTwoToOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Bank bank = game.GetBank();
            one.AddPort(Port.LumberPort);

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 10);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 10, 0, 0, 0 }, new int[] { 0, 1, 1, 2, 1, 2 }));
            Assert.AreEqual(7, one.HandSize());
            Assert.AreEqual(1, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(1, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(2, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(1, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(2, one.ResourceCount(Resource.Wool));
            Assert.AreEqual(26, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(28, bank.ResourceCount(Resource.Lumber));
            Assert.AreEqual(17, bank.ResourceCount(Resource.Ore));
            Assert.AreEqual(18, bank.ResourceCount(Resource.Grain));
            Assert.AreEqual(17, bank.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void ExchangeBankSuccessThreeToOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 10);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 10, 0, 0, 0 }, new int[] { 0, 0, 1, 2, 0, 2 }));
            Assert.AreEqual(5, one.HandSize());
            Assert.AreEqual(0, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(1, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(2, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(0, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(2, one.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void ExchangeBankSuccessMixedOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);

            // make player one hand
            one.AddResource(Resource.Brick, 11);
            one.AddResource(Resource.Lumber, 10);
            one.AddResource(Resource.Wool, 9);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 11, 10, 0, 0, 9 }, new int[] { 0, 3, 1, 2, 3, 2 }));
            Assert.AreEqual(11, one.HandSize());
            Assert.AreEqual(3, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(1, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(2, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(3, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(2, one.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void ExchangeBankSuccessMixedTwo()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);
            one.AddPort(Port.LumberPort);

            // make player one hand
            one.AddResource(Resource.Brick, 2);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 4);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 2, 3, 0, 0, 4 }, new int[] { 0, 3, 0, 0, 0, 0 }));
            Assert.AreEqual(3, one.HandSize());
            Assert.AreEqual(3, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(0, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(0, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(0, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(0, one.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void ExchangeBankSuccessMixedThree()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.WoolPort);

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 17, 3, 0, 0, 5 }, new int[] { 0, 4, 4, 0, 0, 0 }));
            Assert.AreEqual(8, one.HandSize());
            Assert.AreEqual(4, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(4, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(0, one.ResourceCount(Resource.Ore));
            Assert.AreEqual(0, one.ResourceCount(Resource.Grain));
            Assert.AreEqual(0, one.ResourceCount(Resource.Wool));
        }

        [TestMethod]
        public void ExchangeBankWrongArraySize()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 16, 0, 0, 0 }, new int[] { 0, 4, 0, 0, 0, 0 }));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 16, 0, 0, 0, 0 }, new int[] { 0, 4, 0, 0, 0 }));
        }

        [TestMethod]
        public void ExchangeBankInsufficientResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 20, 0, 0, 0, 0 }, new int[] { 0, 5, 0, 0, 0, 0 }));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 4, 0, 0, 0 }, new int[] { 0, 0, 0, 1, 0, 0 }));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 0, 8, 0, 0 }, new int[] { 0, 0, 0, 0, 2, 0 }));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 6);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 1, 8, 0, 0, 4 }, new int[] { 0, 4, 0, 0, 0, 0 }));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityTwo()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 8, 0, 0, 4 }, new int[] { 0, 4, 0, 3, 0, 0 }));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityThree()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 5);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 5, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 0 }));
        }

        [TestMethod]
        public void ExchangeBankWrongRequestedOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 11);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 11, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 4 }));
        }

        [TestMethod]
        public void ExchangeBankWrongRequestedTwo()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);

            // make player one hand
            one.AddResource(Resource.Brick, 13);
            one.AddResource(Resource.Lumber, 11);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 13, 11, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 4 }));
        }

        [TestMethod]
        public void DevCardPlayedOncePerTurn()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.Knight);
            one.AddPermanentDevCard(DevelopmentCard.Knight);
            Assert.IsTrue(one.PlayDevCard(DevelopmentCard.Knight));
            Assert.IsFalse(one.PlayDevCard(DevelopmentCard.Knight));
        }

        [TestMethod]
        public void DevCardMustHave()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Assert.IsFalse(one.PlayDevCard(DevelopmentCard.Knight));
        }

        [TestMethod]
        public void TestRobOne()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            Player three = new Player(game, 2);
            Player four = new Player(game, 3);
            one.AddPermanentDevCard(DevelopmentCard.Knight);
            one.PlayDevCard(DevelopmentCard.Knight);
            game.BuildBuilding(two, Building.Settlement, 1, 1, Vertex.Top);
            game.BuildBuilding(four, Building.Settlement, 1, 1, Vertex.TopRight);
            two.AddResource(Resource.Brick, 4);
            three.AddResource(Resource.Lumber, 4);
            four.AddResource(Resource.Wool, 4);

            // army size should increase
            Assert.AreEqual(1, one.Army);

            // cannot choose to rob yet 
            Assert.IsFalse(one.ChoosePlayerToRob(two));

            // cannot roll dice
            Assert.IsFalse(one.RollDice());

            // should be able to move robber
            Assert.IsTrue(one.MoveRobber(1, 1));

            // should not be able to rob ourselves
            Assert.IsFalse(one.ChoosePlayerToRob(one));

            // should not be able to rob player not on tile
            Assert.IsFalse(one.ChoosePlayerToRob(three));

            // successfully rob player 
            Assert.IsTrue(one.ChoosePlayerToRob(two));

            // assert they have been robbed
            Assert.AreEqual(1, one.HandSize());
            Assert.AreEqual(3, two.HandSize());
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestRobTwo()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            one.AddPermanentDevCard(DevelopmentCard.Knight);
            one.PlayDevCard(DevelopmentCard.Knight);
            game.BuildBuilding(two, Building.Settlement, 1, 1, Vertex.Top);
            two.AddResource(Resource.Brick, 4);

            // army size should increase
            Assert.AreEqual(1, one.Army);

            // cannot choose to rob yet 
            Assert.IsFalse(one.ChoosePlayerToRob(two));

            // cannot roll dice
            Assert.IsFalse(one.RollDice());

            // should be able to move robber
            Assert.IsTrue(one.MoveRobber(1, 1));

            // assert two has been robbed
            Assert.AreEqual(1, one.HandSize());
            Assert.AreEqual(3, two.HandSize());

            // should not be able to rob anyone
            Assert.IsFalse(one.ChoosePlayerToRob(one));

            // should not be able to rob anyone 
            Assert.IsFalse(one.ChoosePlayerToRob(two));
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestDevRoadNoValidSpots()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.RoadBuilding);
            one.PlayDevCard(DevelopmentCard.RoadBuilding);

            // no valid spots to build road
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestDevRoadOneValidSpot()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            one.AddPermanentDevCard(DevelopmentCard.RoadBuilding);
            game.BuildRoad(one, 0, 1, Side.TopLeft);
            game.BuildRoad(two, 0, 1, Side.TopRight);
            one.PlayDevCard(DevelopmentCard.RoadBuilding);

            // one valid spot to build road
            Assert.IsTrue(one.NormalActionsStalled());

            Assert.IsFalse(one.BuildDevRoad(0, 1, Side.BottomLeft));
            Assert.IsTrue(one.BuildDevRoad(0, 1, Side.Left));
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestDevRoadTwoValidSpots()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.RoadBuilding);
            game.BuildRoad(one, 0, 1, Side.TopLeft);
            one.PlayDevCard(DevelopmentCard.RoadBuilding);

            // one valid spot to build road
            Assert.IsTrue(one.NormalActionsStalled());

            Assert.IsTrue(one.BuildDevRoad(0, 1, Side.TopRight));
            Assert.IsTrue(one.BuildDevRoad(0, 1, Side.Left));
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestDevRoadNoRoads()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.Roads = 0;
            one.AddPermanentDevCard(DevelopmentCard.RoadBuilding);
            game.BuildRoad(one, 0, 1, Side.TopLeft);
            one.PlayDevCard(DevelopmentCard.RoadBuilding);

            // no roads to build
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TakeTwoResources()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.YearOfPlenty);
            one.PlayDevCard(DevelopmentCard.YearOfPlenty);

            Assert.IsTrue(one.TakeFreeResource(Resource.Brick));
            Assert.IsTrue(one.TakeFreeResource(Resource.Lumber));
            Assert.IsFalse(one.TakeFreeResource(Resource.Lumber));
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TakeOneResource()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.YearOfPlenty);
            game.GetBank().Withdraw(one, Resource.Lumber, 19);
            game.GetBank().Withdraw(one, Resource.Brick, 19);
            game.GetBank().Withdraw(one, Resource.Ore, 19);
            game.GetBank().Withdraw(one, Resource.Grain, 19);
            game.GetBank().Withdraw(one, Resource.Wool, 18);
            one.PlayDevCard(DevelopmentCard.YearOfPlenty);

            Assert.IsTrue(one.TakeFreeResource(Resource.Wool));
            Assert.IsFalse(one.TakeFreeResource(Resource.Lumber));
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TakeOneResourceNotAvailable()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.YearOfPlenty);
            game.GetBank().Withdraw(one, Resource.Lumber, 19);
            one.PlayDevCard(DevelopmentCard.YearOfPlenty);

            Assert.IsFalse(one.TakeFreeResource(Resource.Lumber));
            Assert.IsTrue(one.TakeFreeResource(Resource.Wool));
            Assert.IsTrue(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TakeResourceBankEmpty()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            one.AddPermanentDevCard(DevelopmentCard.YearOfPlenty);
            game.GetBank().Withdraw(one, Resource.Lumber, 19);
            game.GetBank().Withdraw(one, Resource.Brick, 19);
            game.GetBank().Withdraw(one, Resource.Ore, 19);
            game.GetBank().Withdraw(one, Resource.Grain, 19);
            game.GetBank().Withdraw(one, Resource.Wool, 19);
            one.PlayDevCard(DevelopmentCard.YearOfPlenty);

            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void TestMonopoly()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Player two = new Player(game, 1);
            Player three = new Player(game, 2);

            one.AddResource(Resource.Brick, 4);
            two.AddResource(Resource.Brick, 3);
            three.AddResource(Resource.Brick, 7);
            one.AddPermanentDevCard(DevelopmentCard.Monopoly);
            one.PlayDevCard(DevelopmentCard.Monopoly);
            Assert.IsTrue(one.NormalActionsStalled());
            Assert.IsTrue(one.MonopolizeResource(Resource.Brick));

            Assert.AreEqual(14, one.HandSize());
            Assert.AreEqual(14, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(0, two.HandSize());
            Assert.AreEqual(0, three.HandSize());
            Assert.IsFalse(one.NormalActionsStalled());
        }

        [TestMethod]
        public void EndTurnTwice()
        {
            Game game = new Game("standard_map.txt");
            Player one = new Player(game, 0);
            Assert.IsTrue(one.EndTurn());
            Assert.IsFalse(one.EndTurn());
        }
    }
}
