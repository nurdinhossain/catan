namespace Catan
{
    public class Bank
    {
        // resources held by bank
        private int[] _resources;

        // constructor
        public Bank()
        {
            _resources = new int[Enum.GetNames(typeof(Resource)).Length];

            // populate each resource with 19 units
            for (int i = (int)(Resource.NoResource) + 1; i < _resources.Length; i++)
            {
                _resources[i] = 19;
            }
        }

        // resource count of each resource
        public int ResourceCount(Resource resource)
        {
            return _resources[(int)resource];
        }

        public int TotalResources()
        {
            return _resources.Sum();
        }

        // deposit resources into bank
        public void Deposit(Player player, Resource resource, int amount)
        {
            // take resources away from player
            player.RemoveResource(resource, amount);

            // add resources to bank
            _resources[(int)resource] += amount;
        }

        // withdraw resources from bank
        public bool Withdraw(Player player, Resource resource, int amount)
        {
            // if bank has no resource left, return false
            int bankQuantity = _resources[(int)resource];
            if (bankQuantity == 0) return false;

            // if bank has resources less than amount requested, validate the request but only withdraw whatever's left
            if (bankQuantity < amount)
            {
                return Withdraw(player, resource, bankQuantity);
            }

            // otherwise, withdraw
            _resources[(int)resource] -= amount;
            player.AddResource(resource, amount);

            return true;
        }
    }
}
