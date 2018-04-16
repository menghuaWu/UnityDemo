using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

    public float maxForce;
    float dirX;
    float dirY;
    float dirZ;

    // Use this for initialization
    void Start () {
        maxForce = GameManager._Instance._getballcollision();


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("ball"))
        {
           
            
            
            dirX = Random.Range(-0.8f, 1.2f);
            dirY = Random.Range(-0.2f, 0.2f);
            
            Vector3 newDir = new Vector3(dirX+0.1f, dirY+0.1f, 2f);
           
            collision.gameObject.GetComponent<Rigidbody>().AddForce(newDir * maxForce, ForceMode.Impulse);
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 100;
        }
    }



   
}
