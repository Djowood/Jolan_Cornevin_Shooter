using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Parallax scrolling
/// </summary>
public class ScrollingScript : MonoBehaviour
{
    //Vitesse du défilement
    public Vector2 speed = new Vector2(2, 2);

    //Direction du défilement
    public Vector2 direction = new Vector2(-1, 0);

    //Appliquer le mouvement de scrolling à la caméra ?
    public bool isLinkedToCamera = false;

    //plan est infini
    public bool isLooping = false;

    //Liste des enfants avec renderer
    private List<Transform> backgroundPart;

    //Récupération des objets enfants du plan
    void Start()
    {
        // Pour la réptition
        if (isLooping)
        {
            // On récupère les objets enfants qui ont un renderer
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.GetComponent<Renderer>() != null)
                {
                    backgroundPart.Add(child);
                }
            }

            // Tri par position
            // Note : cela n'est bon que pour un défilement de gauche à droite
            // il faudrait modifier un peu pour gérer d'autres directions.
            backgroundPart = backgroundPart.OrderBy(
              t => t.position.x
            ).ToList();
        }
    }

    void Update()
    {
        // Mouvement
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Défilement camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        //Répétition
        if (isLooping)
        {
            // On prend le premier objet (la liste est ordonnée)
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Premier test sur la position de l'objet
                // Cela évite d'appeler directement IsVisibleFrom
                // qui est assez lourde à exécuter
                if (firstChild.position.y < Camera.main.transform.position.y)
                {
                    // On vérifie maintenant s'il n'est plus visible de la caméra
                    if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                    {
                        // On récupère le dernier élément de la liste
                        Transform lastChild = backgroundPart.LastOrDefault();

                        // On calcule ainsi la position à laquelle nous allons replacer notre morceau
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                        // On place le morceau tout à la fin
                        // Note : ne fonctionne que pour un scorlling horizontal
                        firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);

                        // On met à jour la liste (le premier devient dernier)
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}
