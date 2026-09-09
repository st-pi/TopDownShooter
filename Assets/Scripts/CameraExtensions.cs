using UnityEngine;

public static class CameraExtensions
{
    public static Rect GetWorldRect(this Camera camera)
    {
        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * camera.aspect;
        Vector2 center = camera.transform.position;

        return new Rect(center.x - halfWidth, center.y - halfHeight, halfWidth * 2.0f, halfHeight * 2.0f);
    }
}
