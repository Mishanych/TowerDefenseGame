using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 55)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void RaiseEvent()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void RegisterEvent(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnRegisterEvent(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}
