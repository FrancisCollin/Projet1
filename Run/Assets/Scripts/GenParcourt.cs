using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenParcourt : MonoBehaviour
{
    public Transform terrainObj;
    public List<GameObject> terrains;

    private Vector3 nextTerrainPos;
    private Vector3 lastTerrainPos;
    private int firstTerrainId = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var last = terrains[terrains.Count - 1];
        lastTerrainPos.z = last.transform.position.z;
        nextTerrainPos.z = lastTerrainPos.z + 3.72f;
        if (lastTerrainPos.z < 15.04f)
        {
            SpawnTerrain();
        }

        var first = terrains[firstTerrainId];
        if (first.transform.position.z <= -3.76f) //3.76
        {
            Destroy(first);
            firstTerrainId++;
        }
    }

    void SpawnTerrain()
    {
        

        var go = Instantiate(terrainObj, nextTerrainPos, terrainObj.rotation);
        terrains.Add(go.gameObject);
      
    }
}
