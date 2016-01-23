using System;
using System.Linq;

namespace DddInPractice.Logic.DomainModel
{
    public sealed class SnackMachineEntity : Entity
    {
        public Money MoneyInside { get; private set; } = Money.None;
        public Money MoneyInTransaction { get; private set; } = Money.None;

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes =
            {
                Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar
            };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void SupplyChange(Money money)
        {
            //Money[] coinsAndNotes =
            //{
            //    Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar
            //};
            //if (!coinsAndNotes.Contains(money))
            //    throw new InvalidOperationException();

            MoneyInside += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }
    }
}
