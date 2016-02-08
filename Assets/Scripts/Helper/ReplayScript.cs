using UnityEngine;

/// <summary>
/// Relance le jeux
/// </summary>
class ReplayScript : MonoBehaviour
{
    public void onClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

