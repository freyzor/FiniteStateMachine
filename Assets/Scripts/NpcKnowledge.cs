using UnityEngine;

public class NpcKnowledge : MonoBehaviour
{
    public Transform target;
    public Vector3 lastSeenTargetPosition;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void RemoveTarget()
    {
        lastSeenTargetPosition = target.position;
        target = null;
    }

    public bool HasTarget() => target != null;
}
