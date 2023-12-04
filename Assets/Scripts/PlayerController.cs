using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject missilePrefab;
    public float projectileForce = 10000f;

    public Vector3 thrust;
    public Vector3 rotation;

    public float thrustSpeed = 100f;
    public float rotationSpeed = 100f;

    public static int health = 100;
    public static int lives;

    public AudioSource thrustingSound;
    public AudioSource firingSound;
    public AudioSource collisionSound;

    private GameObject forceField;
    private Vector3 startPos;
    private float timeLeft = 4.0f;


    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;       // keep mouse cursor inside screen (unless ESC is pressed)
        //Cursor.visible = false;                           // hide cursor (unless ESC is pressed)
        Cursor.lockState = CursorLockMode.Locked;           // Enable cursonr lock mode, and press ESC to disable      
        health = 100;

        forceField = this.gameObject.transform.GetChild(0).gameObject;
        //forceField.GetComponent<SpriteRenderer>().color = 
        forceField.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        startPos = gameObject.transform.position;

        lives = PlayerPrefs.GetInt("lives");
    }

    void Update()
    {
        thrust.z = Input.GetAxis("Vertical");       // Move
        
        rotation.x = Input.GetAxis("Mouse Y");      // Pitch
        rotation.y = Input.GetAxis("Mouse X");      // Yaw
        rotation.z = Input.GetAxis("Roll");         // Roll

        // Movement
        this.transform.position += this.transform.forward * thrust.z * thrustSpeed * Time.deltaTime;

        // Rotations
        this.transform.rotation *= Quaternion.AngleAxis(-(rotationSpeed * Time.deltaTime * rotation.x), Vector3.right);     // Pitch
        this.transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime * rotation.y, Vector3.up);           // Yaw
        this.transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime * rotation.z, Vector3.forward);      // Roll

        if (Input.GetMouseButtonDown(0))            // Fire proton torpedoes
        {
            firingSound.Play();
            var go = Instantiate(missilePrefab, Camera.main.transform.position, Quaternion.identity);
            go.transform.forward = Camera.main.transform.forward;
            go.transform.Translate(transform.forward);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * projectileForce);           //  == 10000f
            Object.Destroy(go, 6);            // *****
        }

        
        // Thrusting sound
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            thrustingSound.Play();

        // If health is 0, decrease lives
        if (health <= 0)
        {
            Camera.main.transform.position = startPos;
            lives--;
            health = 100;
            
        }

        if(lives == 0)
            SceneManager.LoadScene("ScoresScene");


        // If asteroids and allien are destroyed, load next scene
        if (GenerateAsteroids.asteroidCount == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        // If ESC is pressed, load next scene
        if (Input.GetKey(KeyCode.F1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        timeLeft -= Time.deltaTime;  
       
        if (timeLeft <= 0) 
        {
            health -= 1;
            //do something
            timeLeft = 4.0f;
        }

        PlayerPrefs.SetInt("lives", lives);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")         // Collision with Asteroid
        {
            collisionSound.Play();

            StartCoroutine(ForceFieldHit());

            health -= 10;
        }

        if(other.gameObject.tag == "Projectile_Enemy")
        {
            collisionSound.Play();
            StartCoroutine(ForceFieldHit());
            health -= 10;
        }
    }

    IEnumerator ForceFieldHit()
    {
        forceField.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.9f, 0.9f, 0.3f);
        yield return new WaitForSeconds(1f);
        forceField.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }


    private void OnDrawGizmos()
    {
        // Draws a 10 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.DrawRay(transform.position, direction);
    }
}