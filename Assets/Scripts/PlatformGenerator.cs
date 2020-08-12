using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public Transform generatorPoint;
    private float distanceBetween;
    private float heightBetween;
    public float platformWidth;
    public float[] distanceMax;
    public float[] heightMax;
    public ObjectPool objPool;

    public GameObject[] platforms;
    public int ind;
    public float[] platformWidths;
    public ObjectPool[] objPools;

    public float ObstaclesThreshold;
    public ObjectPool[] ObstaclePools;
    public int ObStacleInd;


    void Start()
    {
        //platformWidth = platforms[0].GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[objPools.Length];
        for(int i = 0; i < objPools.Length;i++)
        {
            platformWidths[i] = objPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generatorPoint.position.x)
        {

            distanceBetween = Random.Range(0, distanceMax.Length);
            ind = Random.Range(0, objPools.Length);

            heightBetween = Random.Range(0, heightMax.Length);
            transform.position = new Vector3(transform.position.x + platformWidths[ind] + distanceBetween, transform.position.y, transform.position.z);
            //Instantiate(platforms[ind], transform.position, transform.rotation);
            GameObject newPlatform = objPools[ind].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            if(Random.Range(0f,100f) < ObstaclesThreshold)
            {
               
                float[] validChoices = { 1f, 2f, 3f};
                float ranHeight;
                ObStacleInd = Random.Range(0, ObstaclePools.Length);
                ranHeight = Random.Range(0.8f, validChoices.Length);
                if(ObStacleInd == 1 || ObStacleInd == 2)
                {
                    Vector3 obstaclePosition = new Vector3(0f, 0.8f, 0f);
                    GameObject newObstacles = ObstaclePools[ObStacleInd].GetPooledObject();
                    newObstacles.transform.position = transform.position + obstaclePosition;
                    newObstacles.transform.rotation = transform.rotation;
                    newObstacles.SetActive(true);
                }
                else
                {
                    Vector3 obstaclePosition = new Vector3(0f, ranHeight, 0f);
                    GameObject newObstacles = ObstaclePools[ObStacleInd].GetPooledObject();
                    newObstacles.transform.position = transform.position + obstaclePosition;
                    newObstacles.transform.rotation = transform.rotation;
                    newObstacles.SetActive(true);
                }
                
            }
            
           
        }
    }
}
