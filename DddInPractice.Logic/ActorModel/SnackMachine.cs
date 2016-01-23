using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Akka.Event;
using DddInPractice.Logic.DomainModel;
using Even;

namespace DddInPractice.Logic.ActorModel
{
    public partial class SnackMachine:Aggregate<SnackMachineEntity>
    {
        protected ILoggingAdapter Logger = Context.GetLogger(); 
        public SnackMachine()
        {
            OnCommand<DeploySnackMachine>(cmd =>
            {
                if (State != null)
                {
                    Reject("A snackmachine has already been deployed.");
                }
                Persist(new SnackMachineDeployed());
                if (cmd.MoneyInside != Money.None)
                {
                    Persist(new ChangeSupplied(cmd.MoneyInside));
                }
            });
            OnCommand<InsertMoney>(cmd =>
            {
                
            });

            OnEvent<SnackMachineDeployed>(evt =>
            {
                State = new SnackMachineEntity();
                Logger.Info($"SnackMachine created With Id:{Stream.Name}");
            });

            OnEvent<ChangeSupplied>(evt =>
            {
                State.SupplyChange(evt.MoneyDeposited);
                Logger.Info($"SnackMachine now contains ${State.MoneyInside.Amount}");
            });

            OnEvent<MoneyInserted>(evt =>
            {
                State.InsertMoney(evt.Money);
            });
        }

        protected override bool IsValidStream(string stream)
        {
            // by default, streams use "category-uuid" pattern
            // changing this allows aggregates to use any format

            var pattern = Category + @"-\d+";
            return Regex.IsMatch(stream, pattern);
        }

    }
}
