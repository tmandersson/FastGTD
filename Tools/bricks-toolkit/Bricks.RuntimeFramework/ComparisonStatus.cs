using System;

namespace Bricks.RuntimeFramework
{
    [Serializable]
    public class ComparisonStatus
    {
        private Tristate state;
        private readonly ComparisonStatuses childStatuses;
        private bool isProcessed;

        protected ComparisonStatus() {}

        private ComparisonStatus(Tristate state) 
        {
            childStatuses = new ComparisonStatuses();
            this.state = state;
        }

        public static ComparisonStatus DontKnow
        {
            get { return new ComparisonStatus(Tristate.DontKnow); }
        }

        public static ComparisonStatus Eager
        {
            get { return new ComparisonStatus(Tristate.Eager); }
        }

        public static ComparisonStatus Lazy
        {
            get { return new ComparisonStatus(Tristate.Lazy); }
        }

        public virtual Tristate State
        {
            get
            {
                if (Unknown && !isProcessed)
                {
                    isProcessed = true;
                    state = And(state, childStatuses.State);
                    isProcessed = false;
                    ClearChildrenIfEager();
                }
                
                return state;
            }
            set
            {
                state = value;
                ClearChildrenIfEager();
            }
        }

        public Tristate And(Tristate one, Tristate two)
        {
            if (Tristate.Eager.Equals(one) || Tristate.Eager.Equals(two))
                return Tristate.Eager;
            if( Tristate.DontKnow.Equals(one))
                return two;
            if( Tristate.DontKnow.Equals(two))
                return one;
            return Tristate.Lazy;
        }

        private void ClearChildrenIfEager()
        {
            if (Known)
                childStatuses.Clear();
        }

        public bool Unknown
        {
            get { return !Known; }
        }

        public bool Known
        {
            get { return Tristate.Eager.Equals( state ); }
        }

        public override bool Equals(object other)
        {
            ComparisonStatus comparisonStatus = other as ComparisonStatus;
            if (comparisonStatus == null) return false;
            return Equals(state, comparisonStatus.state);
        }

        public override int GetHashCode()
        {
            return state.GetHashCode();
        }

        public ComparisonStatus Combine(ComparisonStatus other)
        {
            And(other);
            RecordUnknowns(other);
            return this;
        }

        internal void And(ComparisonStatus other)
        {
            if (Tristate.Eager.Equals(State)) return;
            
            if(Tristate.DontKnow.Equals(other.State)) return;

            if (state.Equals(Tristate.DontKnow))
                State = other.State;
            
            else if (state.Equals(Tristate.Lazy) && other.State.Equals(Tristate.Lazy))
                State = Tristate.Lazy;
            
            else if (!other.state.Equals(Tristate.DontKnow))
                State = Tristate.Eager;
        }

        private void RecordUnknowns(ComparisonStatus other)
        {
            if (other.state.Equals(Tristate.DontKnow))
                childStatuses.Add(other);
        }

        public static ComparisonStatus Create(bool status)
        {
            return status ? Lazy : Eager;
        }

        public override string ToString()
        {
            return state.ToString();
        }

        public void CopyFrom(ComparisonStatus comparisonStatus)
        {
            state = comparisonStatus.state;
            if(childStatuses == null) throw new Exception("we are screwed");
            childStatuses.AddRange(comparisonStatus.childStatuses);
        }

        public void SetProcessed()
        {
            isProcessed = true;
        }

        public ComparisonStatuses ChildStatuses
        {
            get { return childStatuses; }
        }
    }
}