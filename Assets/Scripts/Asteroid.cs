using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float initialMoveSpeed = 10f;
    public float rotationSpeed = 35f;
    public float moveTowardsSpeed = 20f;
    public float timeBeforeMovingTowardsCamera = 5f;
    public float distance;                                  // ***

    private Rigidbody rb;
    private bool movingTowards = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Get distance between asteroid and camera
        //distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        
        /*if(GetComponent<SphereCollider>())
            Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Allien").GetComponent<BoxCollider>(), GetComponent<SphereCollider>());
        if(GetComponent<CapsuleCollider>())
            Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Allien").GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());*/
    }

    void Update()
    {
        // Rotate around itself
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        transform.RotateAround(Camera.main.transform.position, Camera.main.transform.up, 2f * Time.deltaTime);

        // Starting movement
        //if(!movingTowards)
        //    rb.AddForce(transform.forward * initialMoveSpeed, ForceMode.Impulse);

        // Move towards camera  
        StartCoroutine(MoveTowardsVecinity(timeBeforeMovingTowardsCamera));
        


        // **distance**
        distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
    }

    IEnumerator MoveTowardsVecinity(float t)
    {
        yield return new WaitForSeconds(t);
        // transform.LookAt(Camera.main.transform.position, Vector3.up);
        //movingTowards = true;
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Random.Range(30, 60) * Time.deltaTime);        // speed? ***
        //transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, moveTowardsSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
            Readout.score += 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MainCamera")
        {
            /*for (int i = 0; i < 2; i++)
            {
                Transform temp = Instantiate(smallAsteroid.transform, smallAsteroid.transform.position, smallAsteroid.transform.rotation);
                temp.localScale = temp.localScale * 1f;
                //rb.AddForce(-1 * Camera.main.transform.forward * , ForceMode.Impulse);
            }*/

            Destroy(this.gameObject);
        }
    }

}
