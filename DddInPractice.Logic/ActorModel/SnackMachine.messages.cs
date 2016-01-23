using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DddInPractice.Logic.DomainModel;

namespace DddInPractice.Logic.ActorModel
{
    public partial class SnackMachine
    {

        public class DeploySnackMachine
        {

            public DeploySnackMachine(Money moneyInside)
            {
                MoneyInside = moneyInside ?? Money.None;
            }

            public Money MoneyInside { get; }
        }

        public class SnackMachineDeployed
        {                       
        }

        public class ChangeSupplied
        {
            public ChangeSupplied(Money moneyDeposited)
            {
                MoneyDeposited = moneyDeposited;
            }

            public Money MoneyDeposited { get; }
        }

        public class InsertMoney
        {
            public InsertMoney(Money money)
            {
                Money = money;
            }

            public Money Money { get; }
        }

        public class MoneyInserted
        {
            public MoneyInserted(Money money)
            {
                Money = money;
            }

            public Money Money { get; }
        }
    }
}
