using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //Rigidbody rig;
    float GRAVITY = -30f;
    float grav;

    float speed;
    float speedup;

    int width;
    bool ballLeft;
    bool ballRight;
    float ballAcc;
    float speedMax;

    bool start;

    Vector3 move;
    Vector3 moveAdj;

    GM gm;

    [SerializeField] PlatformSpawner platSpawner;

    bool alive;

    private void Awake()
    {
        speedup = 0.015f;
        width = Screen.width;
        gm = FindObjectOfType<GM>();
    }

    // Use this for initialization
    public void Start()
    {
        //rig = GetComponent<Rigidbody>();
        move = new Vector3(0, 0, 0);
        grav = GRAVITY;
        speed = 1f;
        transform.position = new Vector3(0, 4f, 0);
        moveAdj = new Vector3(0, 0, 0);
        ballLeft = false;
        ballRight = false;
        ballAcc = 3;
        speedMax = 8;
        alive = true;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Add Gravity
        move.y += grav * Time.deltaTime;

        // Check Inputs
        ballLeft = ballRight = false;
        if (Input.touchCount > 0 && start)
        {
            if (Input.GetTouch(0).position.x < (width / 2))
            {
                ballLeft = true;
            }
            else
            {
                ballRight = true;
            }
        }


        if (Input.GetMouseButton(0) && start)
        {
            if (Input.mousePosition.x < (width / 2))
            {
                ballLeft = true;
            }
            else
            {
                ballRight = true;
            }
        }

        if (ballLeft)
        {
            move.x += -ballAcc * speed;
            if (move.x < -speedMax * speed)
                move.x = -speedMax * speed;
            transform.Rotate(0, 0, 180 * speed * Time.deltaTime, Space.World);
        }
        else if (ballRight)
        {
            move.x += ballAcc * speed;
            if (move.x > speedMax * speed)
                move.x = speedMax * speed;
            transform.Rotate(0, 0, -180 * speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (move.x > 0)
            {
                move.x -= ballAcc;
                if (move.x < 0)
                    move.x = 0;
            }
            else if (move.x < 0)
            {
                move.x += ballAcc;
                if (move.x > 0)
                    move.x = 0;
            }
        }


        // Move
        transform.Translate((move + moveAdj) * Time.deltaTime, Space.World);
        transform.Rotate(180 * speed * Time.deltaTime, 0, 0, Space.World);

        // Add to Speed
        if (start)
            speed += speedup * Time.deltaTime;

        //print(move);

        // Check dead
        if (transform.position.y < -4 && alive)
            Die();

        if (Physics.Raycast(transform.position, move, Mathf.Sqrt(Mathf.Pow(move.y + (-.5f / Time.deltaTime), 2f) + Mathf.Pow(move.z, 2f)) * Time.deltaTime))
            Bounce();

    }

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            Bounce();
        }
    }*/

    public void GetMovin()
    {
        start = true;
        Bounce();
    }

    void Bounce()
    {
        move.y = 15 * speed;

        if (start)
        {
            grav = GRAVITY * speed * speed;
            move.z = speed * 10f;

            //Make a new platform
            platSpawner.Spawn();
            platSpawner.Despawn();

            Center();

            gm.UpdateScore();
        }
    }

    private void Center()
    {
        moveAdj.z = transform.position.z % 10;

        if (moveAdj.z > 5)
            moveAdj.z = 10 - moveAdj.z;
        else
            moveAdj.z *= -1;

        moveAdj.y = -.45f - transform.position.y;

        moveAdj *= speed;
    }

    public void Die()
    {
        alive = false;
        start = false;
        platSpawner.DespawnAll();
        gm.died();

    }
}
