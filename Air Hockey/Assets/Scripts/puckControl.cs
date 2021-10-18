using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puckControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject pusher;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text AIScoreText;

    float dir;
    internal Vector3 prevPos;
    internal Vector3 curPos;

    float maxSpeed = 10f;

    int playerScore = 0;
    int AIScore = 0;

    pusherControl pusherControl; 
    void Start()
    {
        curPos = new Vector3();
        prevPos = new Vector3();
        //pusherControl = pusher.GetComponent<pusherControl>();
        changeText();
        StartCoroutine("calculateDir");
    }
    void Update()
    {
        if(rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void changeText()
    {
        playerScoreText.text = (playerScore.ToString());
        AIScoreText.text = (AIScore.ToString());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "goalAI")
        {
            Debug.Log("AI");
            transform.position = new Vector3(1, 0, 0);
            rb.velocity = Vector3.zero;
            AIScore++;
            changeText();
        }
        if(collision.gameObject.tag == "goalPlayer")
        {
            Debug.Log("Player");
            transform.position = new Vector3(-1, 0, 0);
            rb.velocity = Vector3.zero;
            playerScore++;
            changeText();
        }
        if(collision.gameObject.tag == "pusher")
        {
            rb.AddForce(new Vector2(pusherControl.xspeed * 20, pusherControl.yspeed * 20));
        }
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
