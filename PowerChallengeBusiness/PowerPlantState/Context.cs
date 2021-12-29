using PowerChallengeBusiness.Models;
using System.Collections.Generic;

namespace PowerChallengeBusiness.PowerPlantState
{
    public class Context
    {
        State state;
        // Constructor
        public Context(State state)
        {
            this.State = state;
        }
        // Gets or sets the state
        public State State
        {
            get { return state; }
            set
            {
                state = value;
            }
        }
        public List<Power> Request()
        {
            return state.Handle(this);
        }
    }
}