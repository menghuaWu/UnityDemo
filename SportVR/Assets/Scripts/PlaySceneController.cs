using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneController : MonoBehaviour {

    public static PlaySceneController _Instance;
    GameObject PSC_PA;
    GameObject PSCI_PA;

    

    // Use this for initialization
    void Start () {
        Resources.UnloadUnusedAssets();
        PSC_PA = Resources.Load("Prefabs/PlayerArea") as GameObject;
        PSCI_PA = Instantiate(PSC_PA);

       
        
    }

    private void Awake()
    {
        _Instance = this;
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Destroy(PSCI_PA);
        PSC_PA = null;
        Resources.UnloadAsset(PSC_PA);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
