using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeTool : MonoBehaviour {
    public int timer = 11;
    public GameObject TimeTxt;
    public GameObject Backbtn;

    // Use this for initialization
    void Start () {
        StartCoroutine("CountDown");
        

    }
	
	// Update is called once per frame
	void Update () {

        transform.Find("TimeTxt").GetComponent<Text>().text = timer + "";

        if (timer == 0)
        {
            StopCoroutine("CountDown");
            TimeTxt.SetActive(false);
            Backbtn.SetActive(true);
        }
	}

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
    }
}
