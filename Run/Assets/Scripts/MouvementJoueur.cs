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
    public GameObject RightHandLight;
    public GameObject LeftHandLight;
    public ParticleSystem particleJump;

    private float moveAxis;
    private Vector3 gauche;
    private Vector3 centre;
    private Vector3 droite;
    private Vector3 destination;
    private Rigidbody rb;
    private bool jumping = false;
    private bool inAir = false;

    private AudioSource sound;
    public AudioClip[] wooshs;
    public AudioClip jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        gauche = GameObject.Find("Gauche").transform.position;
        centre = GameObject.Find("Centre").transform.position;
        droite = GameObject.Find("Droite").transform.position;

        rb = GetComponent<Rigidbody>();

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAxis = Input.GetAxis("Horizontal");

        if (rb.velocity.y < 0)
        {
            jumping = false;
        }
        if (transform.position.y > 0.05f)
        {
            inAir = true;
            RightHandLight.SetActive(true);
            LeftHandLight.SetActive(true);
            particleJump.Play();
        }
        else
        {
            inAir = false;
            RightHandLight.SetActive(false);
            LeftHandLight.SetActive(false);
            particleJump.Stop();
        }

        if (!jumping)
        {
            if (!CloseEnoughForMe(transform.position.x, destination.x, 0.1f))
            {
                rb.velocity = (destination - transform.position).normalized * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
            
        }
        

        
        if (Input.GetKeyDown("d") || (Input.GetKey("d") && Input.GetKey("space")))
        {
            jumping = false;
            if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
            {        
                destination.x = droite.x;
                PlayWoosh();
            }
            else if(CloseEnoughForMe(transform.position.x, gauche.x, 0.1f))
            {
                PlayWoosh();
                destination.x = centre.x;
            }
        }

        if (Input.GetKeyDown("a") || (Input.GetKey("a") && Input.GetKey("space")))
        {
            jumping = false;
            if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
            {
                destination.x = gauche.x;
                PlayWoosh();
            }
            else if (CloseEnoughForMe(transform.position.x, droite.x, 0.1f))
            {
                destination.x = centre.x;
                PlayWoosh();
            }
        }

        if (Input.GetKeyDown("w") && inAir == false)
        {
            jumping = true;
            rb.velocity = Vector3.up * jumpVelocity;

            sound.clip = jumpSound;
            sound.Play();
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

    void PlayWoosh()
    {
        sound.clip = wooshs[UnityEngine.Random.Range(0, wooshs.Length)];
        sound.Play();
    }
}
