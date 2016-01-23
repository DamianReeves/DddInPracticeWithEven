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
                Persist(new SnackMachineDeployed(cmd.MoneyInside), evt =>
                {
                    Logger.Info($"SnackMachine was deployed with MoneyInside:{evt.MoneyInside}");                    
                });
            });
            OnCommand<InsertMoney>(cmd =>
            {
                
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
