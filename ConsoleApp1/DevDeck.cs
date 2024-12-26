namespace Catan
{
    public enum DevelopmentCard
    {
        Knight,
        RoadBuilding,
        YearOfPlenty,
        Monopoly,
        VictoryPoint
    }

    public class DevDeck
    {
        // private storage for development cards
        private int[] _cards;

        // constructor with default card quantities
        public DevDeck()
        {
            _cards = new int[5] { 14, 2, 2, 2, 5 };
        }

        public int CardsRemaining()
        {
            return _cards.Sum();
        }

        // it is the responsibility of the client to ensure the deck is not empty before drawing
        public DevelopmentCard Draw()
        {
            if (CardsRemaining() == 0)
            {
                throw new ArgumentOutOfRangeException("No cards remaining.");
            }

            // pick random card 
            Random rand = new Random();
            int index = rand.Next(CardsRemaining());
            int i;
            for (i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] == 0)
                    continue;

                index -= _cards[i];
                if (index <= 0)
                {
                    break;
                }
            }

            // take card and return
            _cards[i]--;
            return (DevelopmentCard)i;
        }
    }
}
