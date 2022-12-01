using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected FiniteStateMachine FiniteStateMachine;

    private void Awake()
    {
        FiniteStateMachine = GetComponent<FiniteStateMachine>();
    }

    public void Activate()
    {
        FiniteStateMachine.TrySetState(this);
    }

    public bool IsActive()
    {
        return FiniteStateMachine != null && FiniteStateMachine.IsStateActive(this);
    }

    public bool TrySetState(State state)
    {
        return FiniteStateMachine.TrySetState(state);
    }
    
    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }
    
    public virtual bool CanEnter()
    {
        return true;
    }

    public virtual bool CanExit()
    {
        return true;
    }

}