using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    [SerializeField] GameObject puckObject;
    Rigidbody2D puck;

    private Vector2 targetPosition;
    puckControl puckScript;

    Vector3 mousePos;
    Vector3 prevPos;
    Vector3 curPos;
    Vector3 speed;

    internal float xspeed;
    internal float yspeed;

    float prevx;
    float prevy;
    float x;
    float y;

    void Start()
    {
        curPos = new Vector3();
        prevPos = new Vector3();
        speed = new Vector3();

        transform.position = new Vector3(-4.5f, 0, 0);

        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;

        puck = puckObject.GetComponent<Rigidbody2D>();

        puckScript = puckObject.GetComponent<puckControl>();

        Debug.Log("PUCK: " + puck.position.y);
        Debug.Log("AI: " + transform.position.y);
    }
    void Update()
    {
        float movementSpeed;

        if(puckObject.transform.position.x < 0f)
        {
            movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
        }
        else
        {
            movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
            
            x = puckScript.curPos.x - 0.2f;
            y = puckScript.curPos.y;
            prevx = puckScript.prevPos.x - 0.2f;
            prevy = puckScript.prevPos.y;

            Debug.Log(x - prevx);

            Debug.Log(((startingPosition.x - x) * (y - prevy))/(x - prevx) + y);
            
            if(x - prevx != 0)
                targetPosition = new Vector2(startingPosition.x, ((startingPosition.x - x) * (y - prevy))/(x - prevx) + y);
            else    
                targetPosition = new Vector2(startingPosition.x, startingPosition.y);
        }

        rb.MovePosition(targetPosition);
    }

    IEnumerator calculateSpeed()
    {
        while(true)
        {
            prevPos = transform.position;
            yield return Time.deltaTime;
            curPos = transform.position;

            speed = curPos - prevPos;
            xspeed = speed.x;
            yspeed = speed.y;

            xspeed /= Time.deltaTime;
            yspeed /= Time.deltaTime;
        }
    }
}
