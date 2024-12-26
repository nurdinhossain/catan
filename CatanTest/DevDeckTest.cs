using Catan;

namespace CatanTest
{
    [TestClass]
    public class DevDeckTest
    {
        [TestMethod]
        public void DrawSuccess()
        {
            DevDeck deck = new DevDeck();

            for (int i = 0; i < 25; i++)
            {
                deck.Draw();
            }

            Assert.AreEqual(0, deck.CardsRemaining());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DrawTooMany()
        {
            DevDeck deck = new DevDeck();

            for (int i = 0; i < 26; i++)
            {
                deck.Draw();
            }
        }
    }
}
