using System;
using UnityEngine.Events;

[Serializable]
public class GoalEvent : UnityEvent<PlayerID> {}

[Serializable]
public class MatchStateChangedEvent : UnityEvent<MatchStateTransition> {}
