using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAlliens : MonoBehaviour
{
    //public Transform[] asteroidsPrefab;
    public Transform asteroidPrefab;
    //public int fieldRadius = 1000;
    public int minFieldRadius;
    public int maxFieldRadius;
    public int numOfAlliens;

    public static int allienCount;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(50);

        for (int i = 0; i < numOfAlliens; i++)
        {
            // *** EXPERIMENTING ***
            var rand = Random.onUnitSphere;
            rand.y = 0;
            //rand.x = 1;
            //rand.z = 1000;


            // Instantiate as child
            Transform temp = Instantiate(asteroidPrefab, Random.onUnitSphere * Random.Range(minFieldRadius, maxFieldRadius), Random.rotation, this.transform);
            // Transform temp = Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation, this.transform);

            // Set asteroid local scale to random values
            //temp.localScale = temp.localScale * Random.Range(10, 15);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
