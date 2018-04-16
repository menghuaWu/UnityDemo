using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControllerRayCast : MonoBehaviour {

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }
    public Transform centerEyeAnchor = null;
    public Transform rightHandAnchor = null;
    public LineRenderer lineRenderer = null;
    public float maxRayDistance = 500.0f;
    
    
    
    public LayerMask layerMask;


    Transform Pointer
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.RTrackedRemote) != OVRInput.Controller.None)
            {
                return rightHandAnchor;
            }
            return centerEyeAnchor;
        }
    }


    private void OnEnable()
    {
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }

        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
        }
    }
    // Use this for initialization
    void Start () {
       
        
	}
	
	// Update is called once per frame
	void Update () {
        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }
        Ray laserPointer = new Ray(pointer.position, pointer.forward);
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }
        RaycastHit hit;
        
        if (Physics.Raycast(laserPointer, out hit, maxRayDistance, layerMask))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            

            if (hit.collider.CompareTag("Back"))
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {

                    GameManager._Instance.ChangeScene("Start");
                }
            }else if (hit.collider.CompareTag("Restart"))
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {

                    GameManager._Instance.ChangeScene("Play");
                }
               
            }
        }
        


    }
}
