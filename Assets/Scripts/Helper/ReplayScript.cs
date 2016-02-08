using UnityEngine;

/// <summary>
/// Relance le jeux
/// </summary>
class ReplayScript : MonoBehaviour
{
    public PlayerScript player;
    public void onClick()
    {
        if (player.health <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

