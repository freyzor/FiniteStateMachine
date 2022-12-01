using UnityEngine;

public class StunnedState : State
{
    public float stunTime = 3f;
    private float _endStunTime;
    private NpcController _controller;

    private void Start()
    {
        _controller = GetComponent<NpcController>();
    }

    public override void OnEnter()
    {
        _endStunTime = Time.time + stunTime;
    }

    public override void OnUpdate()
    {
        if (IsStunned())
        {
            _controller.speed = 0f;
            return;
        }

        TrySetState(FiniteStateMachine.initialState);
    }

    public override bool CanExit()
    {
        return !IsStunned();
    }

    private bool IsStunned()
    {
        return _endStunTime > Time.time;
    }

    private void OnDrawGizmos()
    {
        if (!IsActive()) return;

        DrawGizmo.DrawCircle(transform.position, 1f, new Color(1f, 0.94f, 0.07f));
    }
}