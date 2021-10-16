using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject pusher;

    float dir;
    internal Vector3 prevPos;
    internal Vector3 curPos;

    pusherControl pusherControl; 
    void Start()
    {
        curPos = new Vector3();
        prevPos = new Vector3();
        pusherControl = pusher.GetComponent<pusherControl>();

        StartCoroutine("calculateDir");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            transform.position = new Vector3(5, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(new Vector2(pusherControl.xspeed * 10, pusherControl.yspeed * 10));
    }

    void changePrevPos(Vector3 newValue)
    {
        prevPos = newValue;
    }

    void changeCurPos(Vector3 newValue)
    {
        curPos = newValue;
    }

    IEnumerator calculateDir()
    {
        while(true)
        {
            changePrevPos(transform.position);
            yield return new WaitForSeconds(0.1f);
            changeCurPos(transform.position);
        }
    }
}
