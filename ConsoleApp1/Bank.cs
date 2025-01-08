namespace Catan
{
    public class Bank
    {
        // resources held by bank
        private int[] _resources;

        // ledger of all transactions (player, resource, quantity added to bank)
        // ** note: to represent WITHDRAWALS, the quantity added should be negative **
        private List<(Player, Resource, int)> _transactions;

        // constructor
        public Bank()
        {
            _resources = new int[Enum.GetNames(typeof(Resource)).Length];
            _transactions = new List<(Player, Resource, int)>();

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

        // return transaction
        public (Player player, Resource resource, int amount) GetTransactionAt(int index)
        {
            return _transactions[index];
        }

        // return number of transactions
        public int NumTransactions()
        {
            return _transactions.Count;
        }

        // deposit resources into bank
        public void Deposit(Player player, Resource resource, int amount)
        {
            // take resources away from player
            player.RemoveResource(resource, amount);

            // add resources to bank
            _resources[(int)resource] += amount;

            // record transaction
            _transactions.Add((player, resource, amount));
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

            // record transaction
            _transactions.Add((player, resource, -amount));

            return true;
        }
    }
}
