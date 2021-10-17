using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        _gameEvent.RegisterEvent(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnRegisterEvent(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
