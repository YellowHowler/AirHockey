using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusherControl : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;

    Vector3 mousePos;
    Vector3 prevPos;
    Vector3 curPos;
    Camera camera;

    float leftBound = 1f;
    float rightBound = 7.6f;
    float upperBound = 3.4f;
    float lowerBound = -3.4f;

    Vector3 speed;

    internal float xspeed;
    internal float yspeed;

    void Start()
    {
        curPos = new Vector3();
        prevPos = new Vector3();
        speed = new Vector3();
        camera = Camera.main;
        transform.position = new Vector3(5, 0, 0);

        StartCoroutine("calculateSpeed");
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = new Vector3(mousePos.x + 0.1f, mousePos.y, 0);
            
            if(mousePos.x < leftBound) mousePos.x = leftBound;
            if(mousePos.x > rightBound) mousePos.x = rightBound;
            if(mousePos.y < lowerBound) mousePos.y = lowerBound;
            if(mousePos.y > upperBound) mousePos.y = upperBound;

            rb.MovePosition(mousePos);
        }
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
