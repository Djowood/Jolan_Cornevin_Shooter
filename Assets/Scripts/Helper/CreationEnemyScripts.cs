using UnityEngine;
using System.Collections;

/// <summary>
/// Script créant des énemie à une position aléatoire aléatoirement 
/// </summary>
public class CreationEnemyScripts : MonoBehaviour {

    private float timerUntilNewCreation;
    
    //Prefab de l'enemi
    public Transform enemiPrefab;

    // Use this for initialization
    void Start () {
        timerUntilNewCreation = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timerUntilNewCreation += Time.deltaTime;
        if (timerUntilNewCreation > 1)
        {
            timerUntilNewCreation = 0;

            // Création d'un objet copie du prefab
            Transform enemiTransform = Instantiate(enemiPrefab) as Transform;

            var leftBorder = CameraHelper.Left;
            var rightBorder = CameraHelper.Right;
            var bottomBorder = CameraHelper.Bottom;

            //Une position aléatoire 
            float positionAxeX = Random.Range(rightBorder, leftBorder);
            enemiTransform.position = new Vector3(positionAxeX, bottomBorder * 1.5f);

            // Propriétés du script
            EnemyScript enemy = enemiTransform.gameObject.GetComponent<EnemyScript>();
        }
    }
}
