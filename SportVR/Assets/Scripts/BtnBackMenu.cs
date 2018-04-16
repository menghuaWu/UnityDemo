using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnBackMenu : MonoBehaviour {

   
    // Use this for initialization
    void Start () {
        //transform.Find("BtnBackMenu").GetComponent<Button>().onClick.AddListener(BackClick);
    }

    

    // Update is called once per frame
    void Update () {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            PlaySceneController._Instance.LoadScene("Start");

        }
		
	}

    private void BackClick()
    {
        //PlaySceneController._Instance.LoadScene("Start");
    }

}
