using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRGazeMovement : MonoBehaviour {

    float hitPosX, hitPosY;
    float cameRayLenth = 500f;
    int gazeMoveLayer;

    private void Awake()
    {
        gazeMoveLayer = LayerMask.GetMask("hit");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        GazeMove();
    }

    void GazeMove()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray _camRay = new Ray(transform.position, transform.forward);
        RaycastHit gazeHit;

        if (Physics.Raycast(_camRay, out gazeHit, cameRayLenth, gazeMoveLayer))
        {
            hitPosX = gazeHit.point.x;
            hitPosY = gazeHit.point.y;
        }
    }

    public float GetPointX()
    {
        return hitPosX;
    }
    public float GetPointY()
    {
        return hitPosY;
    }
}
