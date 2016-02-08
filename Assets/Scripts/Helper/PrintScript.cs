using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Gére l'affichage de la vie et du score du joueur
/// </summary>
public class PrintScript : MonoBehaviour
{
    public static int score;        // The player's score.
    public PlayerScript player;
    Text text;                      // Reference to the Text component.

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score + "\n Vie: " + player.health;
    }
}