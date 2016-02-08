using UnityEngine;

/// <summary>
/// Comportement des tirs
/// </summary>
public class ShotScript : MonoBehaviour
{
    public uint damage = 1;
    public bool isEnemyShot = false;

    void Start()
    {
        
    }
    void Update()
    {
        if (!GetComponent<Renderer>().IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }
}

