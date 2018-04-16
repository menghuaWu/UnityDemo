using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour {

    //public Transform shootTranform;
    public GameObject ball;
    GameObject shootball;
    public float maxForce;

    private Vector3 curVelocity = Vector3.zero;

    private void Awake()
    {
        
    }
    private void Start()
    {
        maxForce = GameManager._Instance._getshootball();
    }
    private void Update()
    {
        if (GameManager._Instance.dieBall && GameManager._Instance.GetState() == GameManager.GameStarte.PLAY)
        {
            enabled = false;

        }
        
    }

    void OnEnable()
    {
        if (GameManager._Instance.GetState() == GameManager.GameStarte.PLAY)
        {
            shootball = Instantiate(ball, transform.position, transform.rotation);
            shootball.GetComponent<Rigidbody>().AddForce(transform.forward * maxForce, ForceMode.Impulse);
            shootball.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 25f;
        }
        


    }

    IEnumerator WaitToShot()
    {
        yield return new WaitForSeconds(1f);
        GameManager._Instance.dieBall = false;
        enabled = true;

    }

    void OnDisable()
    {
        if (GameManager._Instance.dieBall && GameManager._Instance.GetState() == GameManager.GameStarte.PLAY)
        {
            StartCoroutine(WaitToShot());
            
        }
        else
        {

            Destroy(shootball);    
        }
    }


}
