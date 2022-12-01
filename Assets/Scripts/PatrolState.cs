using UnityEngine;

public class PatrolState : State
{
    public Transform[] waypoints;
    public Transform currentWaypoint;
    public int currentWaypointIndex;
    public float moveSpeed;
    public float approachDistance = 0.5f;
    public AttackState attackState;

    private NpcKnowledge _knowledge;
    private NpcController _controller;
    private int _patrolDirection = 1;
    
    private void Start()
    {
        _controller = GetComponent<NpcController>();
        _knowledge = GetComponent<NpcKnowledge>();
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No patrol waypoints set", this);
        }
    }

    public override void OnEnter()
    {
        _patrolDirection = Random.value < 0.5 ? -1 : 1;
        SelectClosestWaypoint();
    }

    private void SelectClosestWaypoint()
    {
        Vector3 position = transform.position;
        float closestDistance = float.MaxValue;
        int closestWaypointIndex = 0;
        for (int index = 0; index < waypoints.Length; index++)
        {
            var waypoint = waypoints[index].position;
            float distance = Vector3.Distance(waypoint, position);
            if (Vector3.Distance(waypoint, position) < closestDistance)
            {
                closestWaypointIndex = index;
                closestDistance = distance;
            }
        }

        currentWaypointIndex = closestWaypointIndex;
        currentWaypoint = waypoints[currentWaypointIndex];
    }

    private void SelectNextWaypoint()
    {
        currentWaypointIndex = Mod(currentWaypointIndex + _patrolDirection, waypoints.Length);
        currentWaypoint = waypoints[currentWaypointIndex];
    }
    
    private int Mod(int a, int b)
    {
        return a - b * (int)Mathf.Floor(a / (float)b);
    }

    public override void OnUpdate()
    {
        if (_knowledge.HasTarget())
        {
            if (TrySetState(attackState))
                return;
        }
        var position = transform.position;
        var target = currentWaypoint.transform.position;
        float distance = Vector3.Distance(position, target);
        if (distance < approachDistance)
        {
            SelectNextWaypoint();
        }
        else
        {
            _controller.direction = target - position;
            _controller.speed = moveSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentWaypoint == null || !IsActive()) return;
        
        Vector3 waypointPosition = currentWaypoint.position;
        Vector3 position = transform.position;
        Color color = new Color(0.14f, 0.53f, 1f);
        DrawGizmo.DrawCircle(waypointPosition, 1.0f, color);
        Gizmos.color = color;
        Gizmos.DrawLine(position, waypointPosition);
    }
}