using UnityEngine;

/// <summary>
/// Récupère des informations sur la camera du joueur
/// </summary>
class CameraHelper : MonoBehaviour
{
    public static float Top
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(
                new Vector3(0, 0, 0)
            ).y;
        }
    }
    public static float Bottom
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(
                new Vector3(0, 1, 0)
            ).y;
        }
    }
    public static float Right
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(
                new Vector3(1, 0, 0)
            ).x;
        }
    }
    public static float Left
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(
                new Vector3(0, 0, 0)
            ).x;
        }
    }

    public static bool isEnemyInside(Renderer rendered)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Bounds bounds = rendered.bounds;
        bounds.size = new Vector3(bounds.size.x, bounds.size.y - 5);

        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}

