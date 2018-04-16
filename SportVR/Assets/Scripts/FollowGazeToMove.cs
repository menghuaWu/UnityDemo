using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGazeToMove : MonoBehaviour {

    float thisPosX,thisPosY;

    public GameObject FolloeCam;
    OVRGazeMovement OVRgazeMove;

    private void Awake()
    {
        
        OVRgazeMove = FolloeCam.GetComponent<OVRGazeMovement>();
    }
    // Use this for initialization
    void Start () {
        thisPosX = transform.position.x;
        thisPosY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        
        transform.position = new Vector3(OVRgazeMove.GetPointX() + thisPosX, OVRgazeMove.GetPointY() + thisPosY, transform.position.z);    
    }

    
}
