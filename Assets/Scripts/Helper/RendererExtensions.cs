using UnityEngine;

/// <summary>
/// Pour tester si un objet Renderer est dans l'écran ou non 
/// </summary>
public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}