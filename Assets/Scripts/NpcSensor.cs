using UnityEngine;

[RequireComponent(typeof(NpcKnowledge))]
public class NpcSensor : MonoBehaviour
{
    public NpcKnowledge knowledge;
    public LayerMask lineOfSightLayerMask;
    public float visionRange;
    
    private Ray _ray;
    private RaycastHit _hit;
    private Transform _targetTransform;
    private Transform _playerTransform;
    public float visionAngle;

    private void Awake()
    {
        knowledge = GetComponent<NpcKnowledge>();
        _playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (CanSeeTarget(_playerTransform))
        {
            knowledge.SetTarget(_playerTransform);
        }
        else if (knowledge.HasTarget())
        {
            knowledge.RemoveTarget();
        }
    }

    private bool CanSeeTarget(Transform target)
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 targetPosition = target.position + Vector3.up;
        Vector3 toTarget = (targetPosition - origin);
        if (toTarget.magnitude > visionRange)
            return false;

        if (Vector3.Angle(transform.forward, toTarget) > visionAngle)
            return false;

        _ray.origin = origin;
        _ray.direction = toTarget;
        if (Physics.Raycast(_ray, out _hit, visionRange, lineOfSightLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (IsPlayer(_hit.collider))
            {
                Debug.DrawLine(_ray.origin, _hit.point, Color.green);
                return true;
            }

            Debug.DrawLine(_ray.origin, _hit.point, Color.red);
            return false;
        }

        return false;

    }

    private bool IsPlayer(Collider targetCollider) => targetCollider.CompareTag("Player");
    
    private void OnDrawGizmos()
    {
        Color color = new Color(1f, 0.33f, 0.09f);
        Vector3 position = transform.position;
        DrawGizmo.DrawArc(position, transform.forward, visionRange, visionAngle, color);

        if (_targetTransform == null) return;

        if (knowledge.HasTarget())
        {
            DrawGizmo.DrawCircle(knowledge.target.position, 1.0f, color);
        }
        else
        {
            DrawGizmo.DrawCircle(_targetTransform.position, 1.0f, Color.magenta);
            if(_hit.collider != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(_ray.origin, _hit.point);
            }
        }
    }
}
