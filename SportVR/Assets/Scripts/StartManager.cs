using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    

    public static int _Levelchoise;

    private void Start()
    {
        _Levelchoise = 1;
        
    }

    public void ChangeScene(string scene)
    {
        GC.Collect();
        SceneManager.LoadScene(scene);
    }

    public void ChoiseValue(int val)
    {
        _Levelchoise = val;
        
    }
}
