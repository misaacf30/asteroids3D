using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allien : MonoBehaviour
{
    public Transform missilePrefab;
    public float distance;

    float timer;
    float waitingTime;
    bool stop = false;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);


        if (distance > 200 && stop == false)
        {
            transform.Rotate(0, 0, 1f);
            transform.RotateAround(Camera.main.transform.position, Camera.main.transform.up, 2f * Time.deltaTime);
        }
        timer += Time.deltaTime;

        StartCoroutine(MoveTowardsVecinity(5));
        

    }

    IEnumerator MoveTowardsVecinity(float t)
    {
        yield return new WaitForSeconds(t);
        //transform.Rotate(0, 0, 1f);
        //stop = false;
        if(distance > 50 && stop == false)
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 30 * Time.deltaTime);

        if (distance < 200 && distance > 50)
        {
            stop = false;
            transform.LookAt(Camera.main.transform);
            if (distance > 100) waitingTime = 3;
            if (distance < 100) waitingTime = 2;

            if (timer > waitingTime)
            {
                var shoot = Instantiate(missilePrefab, this.transform.position, Quaternion.identity);
                shoot.GetComponent<Rigidbody>().AddForce(this.transform.forward * 4000);
                timer = 0;
                Object.Destroy(shoot.gameObject, 6);
            }
        }
        if (distance < 50)
        {
            stop = true;
            transform.LookAt(Camera.main.transform);
            waitingTime = 1;
            if (timer > waitingTime)
            {
                var shoot = Instantiate(missilePrefab, this.transform.position, Quaternion.identity);
                shoot.GetComponent<Rigidbody>().AddForce(this.transform.forward * 4000);
                timer = 0;
                Object.Destroy(shoot.gameObject, 6);
            }
        }

    }


    IEnumerator FirePlayer(float t)
    {
        var go = Instantiate(missilePrefab, this.transform.position, Quaternion.identity);
        go.transform.forward = this.transform.forward;
        go.transform.Translate(transform.forward);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);           //  == 10000f
        Object.Destroy(go, 6);
        yield return new WaitForSeconds(t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
            Readout.score += 50;
            PlayerController.health += 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "MainCamera")
        {
            Destroy(this.gameObject);
        }*/
    }
}