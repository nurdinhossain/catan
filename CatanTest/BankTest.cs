using Catan; 

namespace CatanTest
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void BankInitialize()
        {
            Bank bank = new Bank();

            Assert.AreEqual(0, bank.ResourceCount(Resource.NoResource));
            for (int i = (int)Resource.NoResource+1; i < Enum.GetNames(typeof(Resource)).Length; i++)
            {
                Assert.AreEqual(19, bank.ResourceCount((Resource)i));
            }
        }

        [TestMethod]
        public void BankDeposit()
        {
            Game game = new Game("standard_map.txt");
            Bank bank = game.GetBank();
            Player player = new Player(game, 0);

            player.AddResource(Resource.Grain, 20);
            player.AddResource(Resource.Brick, 30);

            bank.Deposit(player, Resource.Grain, 10);
            bank.Deposit(player, Resource.Brick, 13);

            Assert.AreEqual(10, player.ResourceCount(Resource.Grain));
            Assert.AreEqual(17, player.ResourceCount(Resource.Brick));
            Assert.AreEqual(29, bank.ResourceCount(Resource.Grain));
            Assert.AreEqual(32, bank.ResourceCount(Resource.Brick));
            Assert.AreEqual(2, bank.NumTransactions());
        }

        [TestMethod]
        public void BankWithdrawTrue()
        {
            Game game = new Game("standard_map.txt");
            Bank bank = game.GetBank();
            Player player = new Player(game, 0);

            Assert.AreEqual(true, bank.Withdraw(player, Resource.Ore, 10));
            Assert.AreEqual(true, bank.Withdraw(player, Resource.Wool, 19));

            Assert.AreEqual(10, player.ResourceCount(Resource.Ore));
            Assert.AreEqual(19, player.ResourceCount(Resource.Wool));
            Assert.AreEqual(9, bank.ResourceCount(Resource.Ore));
            Assert.AreEqual(0, bank.ResourceCount(Resource.Wool));
            Assert.AreEqual(2, bank.NumTransactions());
        }

        [TestMethod]
        public void BankWithdrawTrueLeftover()
        {
            Game game = new Game("standard_map.txt");
            Bank bank = game.GetBank();
            Player player = new Player(game, 0);

            Assert.AreEqual(true, bank.Withdraw(player, Resource.Lumber, 20));

            Assert.AreEqual(0, bank.ResourceCount(Resource.Lumber));
            Assert.AreEqual(1, bank.NumTransactions());
        }

        [TestMethod]
        public void BankWithdrawFalse()
        {
            Game game = new Game("standard_map.txt");
            Bank bank = game.GetBank();
            Player player = new Player(game, 0);

            Assert.AreEqual(true, bank.Withdraw(player, Resource.Lumber, 20));
            Assert.AreEqual(false, bank.Withdraw(player, Resource.Lumber, 1));

            Assert.AreEqual(0, bank.ResourceCount(Resource.Lumber));
            Assert.AreEqual(1, bank.NumTransactions());
        }
    }
}
