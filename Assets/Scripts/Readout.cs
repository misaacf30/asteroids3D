using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Readout : MonoBehaviour
{
    private Text readoutText;

    public static int score = 0;
    private float timer;

    private void Awake()
    {
        readoutText = this.GetComponent<Text>();
        score = PlayerPrefs.GetInt("highscore");
    }


    void Update()
    {
        readoutText.text = "Pitch: " + Camera.main.transform.localEulerAngles.x + "\n" +     // -Camera.main.transform.rotation.x * 100
                            "Yaw: " + Camera.main.transform.localEulerAngles.y + "\n" +       // Camera.main.transform.rotation.y * 100
                            "Roll: " + Camera.main.transform.localEulerAngles.z + "\n" +     // -Camera.main.transform.rotation.z * 100
                            "X-Pos: " + Camera.main.transform.position.x + "\n" +
                            "Y-Pos: " + Camera.main.transform.position.y + "\n" +
                            "Z-Pos: " + Camera.main.transform.position.z + "\n" +
                            "Forcefield (%): " + PlayerController.health + "\n" +
                            "Lives: " + PlayerController.lives + "\n" +
                            "Asteroids: " + GenerateAsteroids.asteroidCount + "\n" +
                            "Score: " + score;

        timer += Time.deltaTime;

        PlayerPrefs.SetInt("highscore", score);
    }

    // Still need to be implemented
    Vector3 updateRotationAngles()
    {        
        return new Vector3(0, 0, 0);
    }
}
