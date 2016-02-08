using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Vie du personnage
    public uint health = 2;
    //Dommage causé
    public uint damage = 1;
    //La vitesse de déplacement
    public Vector2 speed = new Vector2(50, 50);
    // 2 - Stockage du mouvement
    private Vector2 movement;

    void Update()
    {
        Move();
        Fire();
    }

    void FixedUpdate()
    {
        // 5 - Déplacement
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    /// <summary>
    /// Fonction gérant les déplacement du joueur
    /// </summary>
    private void Move()
    {
        //Récupére l'appuis des touches
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //Calcul du mouvement
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        //Déplacement limité au cadre de la caméra
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = CameraHelper.Left;
        var rightBorder = CameraHelper.Right;
        var topBorder = CameraHelper.Top;
        var bottomBorder = CameraHelper.Bottom;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );
    }

    /// <summary>
    /// Fonction gérant les tires du joueur
    /// </summary>
    private void Fire()
    {
        bool shoot = Input.GetButtonDown("Fire1");

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                //False : le tire vient d'un alié
                weapon.Attack(false);
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }
    }
}