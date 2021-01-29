using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementJoueur : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 5f;

    private float moveAxis;
    private Vector3 gauche;
    private Vector3 centre;
    private Vector3 droite;
    private Vector3 destination;
    private Rigidbody rb;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        gauche = GameObject.Find("Gauche").transform.position;
        centre = GameObject.Find("Centre").transform.position;
        droite = GameObject.Find("Droite").transform.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAxis = Input.GetAxis("Horizontal");
        Debug.Log(destination);
        if (rb.velocity.y < 0)
        {
            jumping = false;
        }

        if (!jumping)
        {
            rb.velocity = (destination - transform.position).normalized * moveSpeed;
        }
        

        
        if (Input.GetKeyDown("d"))
        {
            if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
            {        
                destination.x = droite.x;
            }
            else if(CloseEnoughForMe(transform.position.x, gauche.x, 0.1f))
            {
                destination.x = centre.x;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
            {
                destination.x = gauche.x;
            }
            else if (CloseEnoughForMe(transform.position.x, droite.x, 0.1f))
            {
                destination.x = centre.x;
            }
        }

        if (Input.GetKeyDown("w"))
        {
            jumping = true;
            rb.velocity = Vector3.up * jumpVelocity;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    static bool CloseEnoughForMe(float value1, float value2, float acceptableDifference)
    {
        return Math.Abs(value1 - value2) <= acceptableDifference;
    }
}
