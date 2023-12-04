using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Allien : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider spherecollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spherecollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            meshRenderer.enabled = false;
            spherecollider.enabled = false;
            Destroy(this);
        }
        if (collision.gameObject.tag == "Projectile")
        {
            meshRenderer.enabled = false;
            spherecollider.enabled = false;
            Destroy(this);
        }
        if (collision.gameObject.tag == "MainCamera")
        {
            meshRenderer.enabled = false;
            spherecollider.enabled = false;
            Destroy(this);
        }
        

    }
}
