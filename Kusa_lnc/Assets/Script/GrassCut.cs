using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCut : MonoBehaviour
{
    Vector3 scale = new Vector3(1f, 0f, 1f);

    void Update()
    {
        if (transform.localScale.y < 4.0f)
        {
            scale.y += 0.05f;
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Cutter")
        {
            Debug.Log("Cut");
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(10, 100, 10);
        }
    }

}
