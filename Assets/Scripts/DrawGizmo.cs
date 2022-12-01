using UnityEngine;

public static class DrawGizmo
{
    public static void DrawCircle(Vector3 position, float radius, Color color, int segments = 32)
    {
        Gizmos.color = color;
        float angleSize = 360f / segments;
        Vector3 startPoint = Vector3.right * radius;
        Vector3 lastOffset = position + startPoint;
        for (int i = 1; i <= segments; i++)
        {
            Vector3 newOffset = position + Quaternion.Euler(0, angleSize * i, 0) * startPoint;
            Gizmos.DrawLine(lastOffset, newOffset);
            lastOffset = newOffset;
        }
    }

    public static void DrawArc(Vector3 position, Vector3 forward, float radius, float angle, Color color, int segments = 16)
    {
        Gizmos.color = color;
        float angleSize = angle * 2f / segments;
        Vector3 startPoint = Quaternion.Euler(0, -angle, 0) * (forward * radius);
        Vector3 lastPoint = position + startPoint;
        Gizmos.DrawLine(position, lastPoint);
        for (int i = 1; i <= segments; i++)
        {
            Vector3 nextPoint = position + Quaternion.Euler(0, angleSize * i, 0) * startPoint;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.DrawLine(position, lastPoint);
    }
}
