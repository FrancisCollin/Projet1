using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementJoueur : MonoBehaviour
{
    public float moveSpeed = 2;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 5f;

    private float moveAxis;
    private Vector3 gauche;
    private Vector3 centre;
    private Vector3 droite;
    private Rigidbody rb;
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

        if (Input.GetKeyDown("d"))
        {
            if (transform.position == centre)
            {
                transform.position = droite;
            }
            else if(transform.position == gauche)
            {
                transform.position = centre;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            if (transform.position == centre)
            {
                transform.position = gauche;
            }
            else if (transform.position == droite)
            {
                transform.position = centre;
            }
        }

        if (Input.GetKeyDown("w"))
        {
            rb.velocity = Vector3.up * jumpVelocity;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
}
