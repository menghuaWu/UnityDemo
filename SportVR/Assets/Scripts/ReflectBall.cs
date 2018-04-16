using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBall : MonoBehaviour {

    
    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Middle_bounce") || collision.gameObject.CompareTag("Right_bounce") || collision.gameObject.CompareTag("Left_bounce"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
           
        }
        if (collision.gameObject.CompareTag("back"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }

}   
