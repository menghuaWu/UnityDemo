using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball") && GameManager._Instance.GetState() == GameManager.GameStarte.PLAY)
        {
            other.gameObject.GetComponent<ReflectBall>().enabled = false;
            Destroy(other.gameObject);
            GameManager._Instance.dieBall = true;
            GameManager._Instance.DestroyHealth();
        }
    }
}
