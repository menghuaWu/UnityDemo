using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScoreText : MonoBehaviour {

    public int place;

	// Use this for initialization
	void Start () {
        int score = LeaderBoard._instance.GetScore(place);

        if (score != 0)
        {
            GetComponent<Text>().text = score.ToString() ;
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        
	}
	
	
}
