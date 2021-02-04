using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private ParametresJeu parametres;
    private MouvementJoueur joueur;
    private Rigidbody rb;
    public bool mort = false;

    public AudioClip hurt;
    private AudioSource sound;

    private void Start()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
        joueur = GetComponent<MouvementJoueur>();
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            sound.clip = hurt;
            sound.Play();

            joueur.enabled = false;
            mort = true;

            StartCoroutine("Die");

            
        }
    }

    IEnumerator Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(0.01f);
        parametres.vitesse = 0;
    }
}
