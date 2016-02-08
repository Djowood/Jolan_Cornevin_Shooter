using UnityEngine;


/// <summary>
/// Déplace l'objet
/// </summary>
public class MoveScript : MonoBehaviour
{
    //Vitesse de déplacement
    public Vector2 speed = new Vector2(10, 10);
    //Direction
    public Vector2 direction = new Vector2(-1, 0);
    private Vector2 movement;

    void Update()
    {
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        // Application du mouvement
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}