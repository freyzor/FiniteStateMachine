using UnityEngine;

public class SearchState : State
{
    private Vector3 _lastPlayerPosition;
    private NpcController _controller;
    private float _waitEndTime;
    private bool _isWaiting;
    private NpcKnowledge _knowledge;

    public float moveSpeed = 3f;
    public float waitTime = 2f;
    public float approachDistance = 0.5f;
    public State nextState;
    public State attackState;

    private void Start()
    {
        _controller = GetComponent<NpcController>();
        _knowledge = GetComponent<NpcKnowledge>();
    }

    public override void OnEnter()
    {
        _lastPlayerPosition = _knowledge.lastSeenTargetPosition;
        _isWaiting = false;
    }

    public override void OnUpdate()
    {
        if (_knowledge.HasTarget())
        {
            if (TrySetState(attackState))
                return;
        }
        if (_isWaiting)
        {
            if (_waitEndTime < Time.time)
            {
                TrySetState(nextState);
            }
            return;
        }
        Vector3 toTarget = _lastPlayerPosition - transform.position;
        float distance = toTarget.magnitude;
        if (distance <= approachDistance)
        {
            _isWaiting = true;
            _waitEndTime = Time.time + waitTime;
            _controller.speed = 0f;
        }
        else
        {
            _controller.direction = toTarget;
            _controller.speed = moveSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (!IsActive()) return;

        Color color = new Color(1f, 0.96f, 0.1f);
        DrawGizmo.DrawCircle(_lastPlayerPosition, 1f, color);
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, _lastPlayerPosition);
    }
}
