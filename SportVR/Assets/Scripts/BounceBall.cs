using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour {

    
    public float maxForce;

    // Use this for initialization
    void Start () {
        maxForce = GameManager._Instance._getbounceball();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {

            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 newDir = Vector3.forward;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(newDir * -1 * maxForce , ForceMode.Impulse);
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 100;
        }
    }
}
