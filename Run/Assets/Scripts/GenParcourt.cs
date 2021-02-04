using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenParcourt : MonoBehaviour
{
    public Transform[] terrainObj;
    public Transform[] obstaclesObj;
    public List<GameObject> terrains;
    public List<GameObject> obstacles;
    public bool difficile = true;

    
    private Vector3 nextTerrainPos;
    private Vector3 lastTerrainPos;
    private int firstTerrainId = 0;
    private Vector3 nextObstaclePos;
    private int[] rows = new int[] { -1, 0, 1 };
    private int firstObstacleId = 0;

    private ParametresJeu parametres;

    // Start is called before the first frame update
    void Start()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculeNextPos();

        var firstTerrain = terrains[firstTerrainId];
        if (firstTerrain.transform.position.z <= -3.76f) //3.76
        {
            Destroy(firstTerrain);
            firstTerrainId++;
            parametres.score++;
        }

        var firstObstacle = obstacles[firstObstacleId];
        if (firstObstacle.transform.position.z <= -3.76)
        {
            Destroy(firstObstacle);
            firstObstacleId++;
            
        }
    }

    void SpawnTerrain()
    {
        var terrain = terrainObj[Random.Range(0, terrainObj.Length)];
        var go = Instantiate(terrain, nextTerrainPos, terrain.rotation);
        terrains.Add(go.gameObject);
    }
    void SpawnObstacle()
    {
        var obstacle = obstaclesObj[Random.Range(0, obstaclesObj.Length)];
        var go = Instantiate(obstacle, new Vector3(nextObstaclePos.x,obstacle.position.y, nextObstaclePos.z), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f))));
        obstacles.Add(go.gameObject);
    }

    void CalculeNextPos()
    {
        var lastTerrain = terrains[terrains.Count - 1];

        lastTerrainPos.z = lastTerrain.transform.position.z;
        nextTerrainPos.z = lastTerrainPos.z + 3.72f;

        nextObstaclePos.z = lastTerrainPos.z + Random.Range(-0.2f, 1.86f);
        nextObstaclePos.x = rows[Random.Range(0, 3)];

        if (Random.Range(0, 2) == 1)
        {
            difficile = true;
        }
        else
        {
            difficile = false;
        }

        if (lastTerrainPos.z < 50f) //15.04f
        {
            SpawnTerrain();
            SpawnObstacle();
            if (difficile)
            {
                var rd = Random.Range(0, 2);
                if (nextObstaclePos.x == 0) //Centre
                {
                    
                    if (rd == 0)
                    {
                        nextObstaclePos.x = -1;
                    }
                    else
                    {
                        nextObstaclePos.x = 1;
                    }
                }
                else if(nextObstaclePos.x == -1) //Gauche
                {
                    if (rd == 0)
                    {
                        nextObstaclePos.x = 0;
                    }
                    else
                    {
                        nextObstaclePos.x = 1;
                    }
                }
                else //Droite
                {
                    if (rd == 0)
                    {
                        nextObstaclePos.x = 0;
                    }
                    else
                    {
                        nextObstaclePos.x = -1;
                    }
                }
                SpawnObstacle();
            }
        }
    }
}
