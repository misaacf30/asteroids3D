using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{
    //public Transform[] asteroidsPrefab;
    public Transform asteroidPrefab;
    //public int fieldRadius = 1000;
    public int minFieldRadius = 1000;
    public int maxFieldRadius = 1500;

    public int numOfAsteroids = 10;

    public static int asteroidCount;
    
    void Start()
    {
        //asteroidsPrefab[Random.Range(0, asteroidsPrefab.Length - 1)]      // asteroidPrefab

        Random.InitState(100);

        for (int i = 0; i < numOfAsteroids; i++)
        {
            // *** EXPERIMENTING ***
            var rand = Random.onUnitSphere;
            rand.y = 0;
            //rand.x = 1;
            //rand.z = 1000;


            // Instantiate asteroids as child
            Transform temp = Instantiate(asteroidPrefab, Random.onUnitSphere * Random.Range(minFieldRadius, maxFieldRadius), Random.rotation, this.transform);
            // Transform temp = Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation, this.transform);

            // Set asteroid local scale to random values
            temp.localScale = temp.localScale * Random.Range(10, 15);
        }    
    }

    private void Update()
    {
        asteroidCount = transform.childCount;
    }

}
