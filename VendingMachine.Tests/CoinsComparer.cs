using System.Collections.Generic;
using VendingMachine.BL.Wallet;

namespace VendingMachine.Tests
{
    public class CoinsComparer : IEqualityComparer<ICoin>
    {
        public bool Equals(ICoin x, ICoin y)
        {
            if (x == null || y == null) return false;

            if (x.CoinValue == y.CoinValue && x.CoinsCount == y.CoinsCount) return true;

            return false;
        }

        public int GetHashCode(ICoin coin)
        {
            if (object.ReferenceEquals(coin, null)) return 0;

            int hashCodeName = coin.CoinValue == null ? 0 : coin.CoinValue.GetHashCode();
            int hasCodeAge = coin.CoinsCount.GetHashCode();

            return hashCodeName ^ hasCodeAge;
        }
    }
}
