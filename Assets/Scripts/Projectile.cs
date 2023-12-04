using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AudioSource explosionSound;
    private MeshRenderer meshRenderer;
    private SphereCollider spherecollider;
    Component[] components;

    public GameObject explosion1;
    public GameObject explosion2;

    private void Awake()
    {
        explosionSound = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        spherecollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            StartCoroutine(DestroyAfterExplosion(explosion1));
        }
        if (collision.gameObject.tag == "Allien")
        {
            StartCoroutine(DestroyAfterExplosion(explosion2));
        }
        if(collision.gameObject.tag == "Planet")
        {
            StartCoroutine(DestroyAfterExplosion(explosion1));
        }
        if (collision.gameObject.tag == "Projectile_Enemy")
        {
            StartCoroutine(DestroyAfterExplosion(explosion2));
        }


    }

    IEnumerator DestroyAfterExplosion(GameObject explosion)
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        ApplyExplosionForce();
       
        meshRenderer.enabled = false;
        spherecollider.enabled = false;
        explosion.SetActive(true);
        explosionSound.Play();

        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    public void ApplyExplosionForce()
    {
        float explosionForce = 100.0f;
        float affectedRadius = 50.0f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, affectedRadius);

        foreach (Collider affectedObjects in colliders)
        {
            if (affectedObjects.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(explosionForce, transform.position, affectedRadius, 1.0f, ForceMode.Impulse);
        }
    }
}


