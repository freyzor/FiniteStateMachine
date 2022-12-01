using UnityEngine;
using UnityEngine.Events;

public class FiniteStateMachine : MonoBehaviour
{
    public State initialState;

    private State _currentState;

    public UnityEvent<State> onStateChanged;
    
    private void Start()
    {
        TrySetState(initialState);
    }

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    void FixedUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnFixedUpdate();
        }
    }

    public bool TrySetState(State state)
    {
        if (!state.CanEnter())
            return false;

        if (_currentState != null)
        {
            if (!_currentState.CanExit()) 
                return false;
            
            _currentState.OnExit();
        }

        _currentState = state;
        _currentState.OnEnter();
        onStateChanged.Invoke(_currentState);
        return true;
    }

    public bool IsStateActive(State state)
    {
        return state == _currentState;
    }
}