﻿using UnityEngine;
namespace AI
{
    public abstract class StateAI
    {
        public abstract void EntryAction();
        public abstract void ExitAction();
        public abstract void UpdateAction();
    }
}
