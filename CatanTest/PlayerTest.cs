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
            Player one = new Player(0);
            Player two = new Player(1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0 });
            Assert.AreEqual(false, trade1);
            Assert.AreEqual(false, trade2);
        }

        [TestMethod]
        public void TradeNothing()
        {
            Player one = new Player(0);
            Player two = new Player(1);
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 0, 0 }, new int[] { 0, 1, 0, 0, 0, 0 });
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 1, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0 });
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
            bool trade1 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 5, 0, 0 }, new int[] { 0, 0, 2, 0, 0, 0 }); // false
            bool trade2 = one.TradeWithPlayer(two, new int[] { 0, 0, 0, 0, 1, 0 }, new int[] { 0, 0, 0, 0, 0, 8 }); // false

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

        [TestMethod]
        public void BuildSettlementSuccess()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            Game game = new Game("standard_map.txt");
            Tile tile = game.TileAt(1, 1);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            // create settlement
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(Building.Settlement, tile.BuildingAt(Vertex.TopLeft));
            Assert.AreEqual(one, tile.PlayerAtVertex(Vertex.TopLeft));
            Assert.AreEqual(4, one.Settlements);
            Assert.AreEqual(1, one.VictoryPoints);

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
            Player one = new Player(0);
            Bank bank = new Bank();
            Game game = new Game("standard_map.txt");

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            one.Settlements = 0;
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(0, one.Settlements);
        }

        [TestMethod]
        public void BuildSettlementOnBuilding()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            Game game = new Game("standard_map.txt");
            game.BuildBuilding(one, Building.Settlement, 1, 1, Vertex.TopLeft);
            game.BuildRoad(one, 1, 1, Side.TopLeft);

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            game.BuildBuilding(one, Building.City, 1, 1, Vertex.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(5, one.Settlements);
        }

        [TestMethod]
        public void BuildSettlementInsufficientResources()
        {
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 4);

            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(5, one.Settlements);
        }

        [TestMethod]
        public void BuildCitySuccess()
        {
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Tile tile = game.TileAt(1, 1);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Wool, 4);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));

            // create city
            Assert.AreEqual(true, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
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
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Bank bank = new Bank();
            one.Cities = 0;

            // make player one hand
            one.AddResource(Resource.Brick, 4);
            one.AddResource(Resource.Lumber, 4);
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 4);
            one.AddResource(Resource.Wool, 4);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(false, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
        }

        [TestMethod]
        public void BuiltCityOnWrongBuilding()
        {
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Grain, 8);
            one.AddResource(Resource.Ore, 8);
            one.AddResource(Resource.Wool, 8);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(false, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(true, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(true, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(false, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
        }

        [TestMethod]
        public void BuiltCityInsufficientResources()
        {
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 1);
            one.AddResource(Resource.Lumber, 1);
            one.AddResource(Resource.Grain, 2);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 1);
            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.AreEqual(true, one.BuildSettlement(game, 1, 1, Vertex.TopLeft, bank));
            Assert.AreEqual(false, one.BuildCity(game, 1, 1, Vertex.TopLeft, bank));
        }

        [TestMethod]
        public void BuildRoadSuccess()
        {
            Player one = new Player(0);
            Game game = new Game("standard_map.txt");
            Bank bank = new Bank();

            one.AddResource(Resource.Brick, 1);
            one.AddResource(Resource.Lumber, 1);

            game.BuildRoad(one, 1, 1, Side.TopLeft);
            Assert.IsTrue(one.BuildRoad(game, 1, 1, Side.TopRight, bank));
            Assert.AreEqual(0, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(0, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(14, one.Roads);
            Assert.AreEqual(Road.Road, game.TileAt(1, 1).RoadAt(Side.TopRight));
        }

        [TestMethod]
        public void DrawDevCardSuccess()
        {
            Player one = new Player(0);
            DevDeck deck = new DevDeck();
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 3);

            // draw card
            Assert.AreEqual(true, one.DrawDevCard(deck, bank));
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
            Player one = new Player(0);
            DevDeck deck = new DevDeck();
            Bank bank = new Bank();
            for (int i = 0; i < 25; i++)
            {
                deck.Draw();
            }

            // make player one hand
            one.AddResource(Resource.Grain, 3);
            one.AddResource(Resource.Ore, 3);
            one.AddResource(Resource.Wool, 3);

            // draw card 
            Assert.AreEqual(false, one.DrawDevCard(deck, bank));
        }

        [TestMethod]
        public void DrawDevCardInsufficientResources()
        {
            Player one = new Player(0);
            DevDeck deck = new DevDeck();
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Grain, 1);
            one.AddResource(Resource.Ore, 0);
            one.AddResource(Resource.Wool, 2);

            // draw card 
            Assert.AreEqual(false, one.DrawDevCard(deck, bank));
        }

        [TestMethod]
        public void DiceRollValid()
        {
            Player one = new Player(0);
            int[] rolls = new int[13];
            
            for (int i = 0; i < 100000; i++)
            {
                int roll = one.RollDice();
                Assert.IsTrue(roll >= 2);
                Assert.IsTrue(roll <= 12);
                rolls[roll]++;
            }

            // verify distribution of rolls
            int total = rolls.Sum();
            Assert.AreEqual(0, rolls[0]);
            Assert.AreEqual(0, rolls[1]);
            Assert.AreEqual(0.03, (double)rolls[2] / total, 0.02);
            Assert.AreEqual(0.06, (double)rolls[3] / total, 0.02);
            Assert.AreEqual(0.08, (double)rolls[4] / total, 0.02);
            Assert.AreEqual(0.11, (double)rolls[5] / total, 0.02);
            Assert.AreEqual(0.14, (double)rolls[6] / total, 0.02);
            Assert.AreEqual(0.17, (double)rolls[7] / total, 0.02);
            Assert.AreEqual(0.14, (double)rolls[8] / total, 0.02);
            Assert.AreEqual(0.11, (double)rolls[9] / total, 0.02);
            Assert.AreEqual(0.08, (double)rolls[10] / total, 0.02);
            Assert.AreEqual(0.06, (double)rolls[11] / total, 0.02);
            Assert.AreEqual(0.03, (double)rolls[12] / total, 0.02);

        }

        [TestMethod]
        public void ExchangeBankSuccessNoPorts()
        {
            Player one = new Player(0);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 8);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 0, 0, 0, 0 }, new int[] { 0, 0, 2, 0, 0, 0 }, bank));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 2, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0 }, bank));
            Assert.AreEqual(2, one.HandSize());
            Assert.AreEqual(0, one.ResourceCount(Resource.Brick));
            Assert.AreEqual(2, one.ResourceCount(Resource.Lumber));
            Assert.AreEqual(27, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(17, bank.ResourceCount(Resource.Lumber));
        }

        [TestMethod]
        public void ExchangeBankSuccessTwoToOne()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.LumberPort);

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 10);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 10, 0, 0, 0 }, new int[] { 0, 1, 1, 2, 1, 2 }, bank));
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
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 8);
            one.AddResource(Resource.Lumber, 10);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 8, 10, 0, 0, 0 }, new int[] { 0, 0, 1, 2, 0, 2 }, bank));
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
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);

            // make player one hand
            one.AddResource(Resource.Brick, 11);
            one.AddResource(Resource.Lumber, 10);
            one.AddResource(Resource.Wool, 9);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 11, 10, 0, 0, 9 }, new int[] { 0, 3, 1, 2, 3, 2 }, bank));
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
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);
            one.AddPort(Port.LumberPort);

            // make player one hand
            one.AddResource(Resource.Brick, 2);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 4);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 2, 3, 0, 0, 4 }, new int[] { 0, 3, 0, 0, 0, 0 }, bank));
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
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.WoolPort);

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            // make exchange
            Assert.AreEqual(true, one.ExchangeWithBank(new int[] { 0, 17, 3, 0, 0, 5 }, new int[] { 0, 4, 4, 0, 0, 0 }, bank));
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
            Player one = new Player(0);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 16, 0, 0, 0 }, new int[] { 0, 4, 0, 0, 0, 0 }, bank));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 16, 0, 0, 0, 0 }, new int[] { 0, 4, 0, 0, 0 }, bank));
        }

        [TestMethod]
        public void ExchangeBankInsufficientResources()
        {
            Player one = new Player(0);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 17);
            one.AddResource(Resource.Lumber, 3);
            one.AddResource(Resource.Wool, 5);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 20, 0, 0, 0, 0 }, new int[] { 0, 5, 0, 0, 0, 0 }, bank));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 4, 0, 0, 0 }, new int[] { 0, 0, 0, 1, 0, 0 }, bank));
            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 0, 0, 8, 0, 0 }, new int[] { 0, 0, 0, 0, 2, 0 }, bank));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityOne()
        {
            Player one = new Player(0);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 6);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 1, 8, 0, 0, 4 }, new int[] { 0, 4, 0, 0, 0, 0 }, bank));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityTwo()
        {
            Player one = new Player(0);
            Bank bank = new Bank();

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 8);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 8, 0, 0, 4 }, new int[] { 0, 4, 0, 3, 0, 0 }, bank));
        }

        [TestMethod]
        public void ExchangeBankWrongQuantityThree()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 5);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 5, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 0 }, bank));
        }

        [TestMethod]
        public void ExchangeBankWrongRequestedOne()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);

            // make player one hand
            one.AddResource(Resource.Brick, 9);
            one.AddResource(Resource.Lumber, 11);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 9, 11, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 4 }, bank));
        }

        [TestMethod]
        public void ExchangeBankWrongRequestedTwo()
        {
            Player one = new Player(0);
            Bank bank = new Bank();
            one.AddPort(Port.AnyPort);
            one.AddPort(Port.BrickPort);

            // make player one hand
            one.AddResource(Resource.Brick, 13);
            one.AddResource(Resource.Lumber, 11);
            one.AddResource(Resource.Wool, 4);

            Assert.AreEqual(false, one.ExchangeWithBank(new int[] { 0, 13, 11, 0, 0, 4 }, new int[] { 0, 1, 0, 4, 0, 4 }, bank));
        }
    }
}
