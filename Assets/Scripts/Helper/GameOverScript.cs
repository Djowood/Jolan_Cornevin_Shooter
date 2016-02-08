using UnityEngine;

/// <summary>
/// Gere l'affichage de l'animation en cas de fin de jeux
/// </summary>
public class GameOverScript : MonoBehaviour
{
    public Animator anim;                          // Reference to the animator component.
    public PlayerScript player;
    
    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // If the player has run out of health...
        if (player.health <= 0)
        {
            // ... tell the animator the game is over.
            anim.SetTrigger("GameOver");
        }
    }
}