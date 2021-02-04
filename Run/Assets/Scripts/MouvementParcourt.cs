using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementParcourt : MonoBehaviour
{
    private ParametresJeu parametres;

    private void Awake()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * parametres.vitesse, Space.World);
    }
}
