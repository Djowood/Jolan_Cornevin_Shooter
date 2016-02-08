using UnityEngine;

/// <summary>
/// Comportement générique pour les méchants
/// </summary>
public class EnemyScript : MonoBehaviour
{
    //Vie du personnage
    public uint health = 1;
    //Dommage causé
    public uint damage = 1;
    //Si il est dans l'écran ou non 
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;
    private float timerSinceSpawn;

    void Awake()
    {
        // Récupération de toutes les armes de l'ennemi
        weapons = GetComponentsInChildren<WeaponScript>();

        // Récupération du script de mouvement lié
        moveScript = GetComponent<MoveScript>();
    }
    
    /// <summary>
    /// Toutes les fonctions de l'enemie sont désactivé car il est par défaut en dehors de l'écran
    /// </summary>
    void Start()
    {
        hasSpawn = false;

        // On désactive tout
        GetComponent<Collider2D>().enabled = false;
        moveScript.enabled = false;
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        //Si l'ennemi n'est pas encore apparu à l'écran
        if (hasSpawn == false)
        {
            //Test si l'enemy est dans la camera.
            if (CameraHelper.isEnemyInside(GetComponent<Renderer>()))
            {
                Spawn();
                timerSinceSpawn = 0;
            }
        }
        else
        {
            GetOut();
            Fire();
            DestoryIfOut();
        }
    }

    /// <summary>
    /// Activation de toutes les fonction de l'enemi, qui vient de rentrer dans l'écran
    /// </summary>
    private void Spawn()
    {
        hasSpawn = true;

        // On active tout
        GetComponent<Collider2D>().enabled = true;
        moveScript.enabled = true;
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
    
    /// <summary>
    /// Compte le nombre de seconde depuis la création de l'objet
    ///  Si il est la depuis plus de 5 secondes et pas encore détruit, alors on le fait sortir 
    /// </summary>
    private void GetOut()
    {
        timerSinceSpawn += Time.deltaTime;
        if (timerSinceSpawn > 5)
        {
            moveScript.speed.y = 5;
            moveScript.direction.y = -1;
        }
    }

    /// <summary>
    /// On fait tirer toutes les armes automatiquement
    /// </summary>
    private void Fire()
    {
        foreach (WeaponScript weapon in weapons)
        {
            if (weapon != null && weapon.enabled && weapon.CanAttack)
            {
                weapon.Attack(true);
                SoundEffectsHelper.Instance.MakeEnemyShotSound();
            }
        }
    }

    /// <summary>
    /// Test si l'enemi est sorti de l'écran et le détruit si c'est le cas
    /// </summary>
    private void DestoryIfOut()
    {
        if (!GetComponent<Renderer>().IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }
}
