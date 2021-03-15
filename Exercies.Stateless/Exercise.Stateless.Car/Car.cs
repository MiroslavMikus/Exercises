using Stateless;
using Stateless.Graph;
using System;

namespace Exercise.Stateless.Car
{
    /// <summary>
    ///  External state storage
    /// </summary>
    public static class Storage
    {
        public static Car.State CurrentState { get; set; } = Car.State.Parking;
    }

    public class Car
    {
        public enum Trigger { Go, Stop, Break, Repair }
        public enum State { Moving, Parking, Maintenance }

        private readonly StateMachine<State, Trigger> _state;

        /// <summary>
        /// Trigger with string parameter
        /// </summary>
        private StateMachine<State, Trigger>.TriggerWithParameters<string> _goTrigger;

        /// <summary>
        /// http://www.webgraphviz.com/
        /// </summary>
        public string Graph { get => UmlDotGraph.Format(_state.GetInfo()); }

        public Car()
        {
            _state = new StateMachine<State, Trigger>(() => Storage.CurrentState, state =>
            {
                Storage.CurrentState = state;
            });

            _goTrigger = _state.SetTriggerParameters<string>(Trigger.Go);

            _state.Configure(State.Moving)
                .OnEntryFrom(_goTrigger, destination => Console.WriteLine("Target destination: " + destination))
                .Permit(Trigger.Stop, State.Parking)
                .Permit(Trigger.Break, State.Maintenance);

            _state.Configure(State.Parking)
                .OnEntry(() => Console.WriteLine("Arrived at the target destination"))
                .OnEntryFrom(Trigger.Stop, () => Console.WriteLine("Stopping your car"))
                .OnEntryFrom(Trigger.Repair, () => Console.WriteLine("Your car is ready to go"))
                .Permit(Trigger.Go, State.Moving);

            _state.Configure(State.Maintenance)
                .Permit(Trigger.Repair, State.Parking);
        }

        public void MoveToDestination(string destination)
        {
            _state.Fire(_goTrigger, destination);
            _state.Fire(Trigger.Stop);
        }
    }
}
