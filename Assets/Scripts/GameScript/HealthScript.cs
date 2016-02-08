using UnityEngine;

/// <summary>
/// Gestion des points de vie et des dégâts
/// ainsi que toutes les destructions des objets
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Ennemi ou joueur ?
    /// </summary>
    public bool isEnemy = true;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Est-ce un tir ?
        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot != isEnemy)
            {
                uint hp = 0;

                //C'est un tire du joueur
                if (!shot.isEnemyShot)
                {
                    GetComponent<EnemyScript>().health -= shot.damage;
                    hp = GetComponent<EnemyScript>().health;
                }
                //C'est un tire enemy
                else
                {
                    GetComponent<PlayerScript>().health -= shot.damage;
                    hp = GetComponent<PlayerScript>().health;
                }

                if (hp <= 0)
                {
                    SpecialEffectsHelper.Instance.Explosion(transform.position);
                    SoundEffectsHelper.Instance.MakeExplosionSound();

                    // Destruction !
                    Destroy(gameObject);

                    //Le joueur à tuer un enemy, on augmente son score.
                    if (!shot.isEnemyShot)
                    {
                        PrintScript.score++;
                    }
                }

                // Destruction du projectile
                Destroy(shot.gameObject);
            }
        }
    }
}
