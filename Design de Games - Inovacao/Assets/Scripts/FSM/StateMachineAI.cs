using UnityEngine;
namespace AI
{
    public abstract class StateMachineAI : MonoBehaviour
    {
        protected StateAI state01;
        protected StateAI state02;
        protected StateAI state03;

        public bool anim01;
        public bool anim02;
        public bool anim03;

        protected void Update()
        {
            if (state01 != null) state01.UpdateAction();
            if (state02 != null) state02.UpdateAction();
            if (state03 != null) state03.UpdateAction();
        }

        public void SetState01(StateAI _state)
        {
            if (state01 != null) { state01.ExitAction(); }
            state01 = _state;
            state01.EntryAction();
        }

        public void SetState02(StateAI _state)
        {
            if (state02 != null) { state02.ExitAction(); }
            state02 = _state;
            state02.EntryAction();
        }

        public void SetState03(StateAI _state)
        {
            if (state03 != null) { state03.ExitAction(); }
            state03 = _state;
            state03.EntryAction();
        }
    }
}
