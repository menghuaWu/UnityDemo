using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScore : MonoBehaviour {

    public int score ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        
        if (collision.gameObject.CompareTag("ball"))
        {          
            GameManager._Instance.SetScore(score);
        }
            
            
        
        
    }
}
