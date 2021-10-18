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

    bool firstTime = true;

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
        float offSet = Random.Range(0.3f, 0.4f);

        if(puckObject.transform.position.x < 0f)
        {
            firstTime = true;

            movementSpeed = MaxMovementSpeed * Random.Range(0.7f, 1.0f);
            targetPosition = new Vector2(Mathf.Clamp(puckObject.transform.position.x, -5.6f, -1f), puckObject.transform.position.y);
        }
        else
        {
            if(firstTime)
            {
                firstTime = false;
                offSet = Random.Range(-1f, 1f);
            }

            movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
            targetPosition = new Vector2(startingPosition.x, puckObject.transform.position.y);
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.deltaTime));
    }
}
