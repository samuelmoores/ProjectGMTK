using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnedObject_prefab;
    GameObject spawnedObject;
    int index;
    int ballsCount;
    float timerCurrent;
    float timer;
    int spawnCount;

    // Start is called before the first frame update
    void Start()
    {
        timer = 5.0f;
        timerCurrent = timer;
        index = 0;
        ballsCount = 2;

        spawnedObject = GameObject.Instantiate(spawnedObject_prefab[index], transform.position, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody>().AddForce(transform.forward * 125f, ForceMode.Impulse);
        spawnCount++;
        index++;
        timerCurrent = timer;

    }

    // Update is called once per frame
    void Update()
    {
        if(timerCurrent > 0.0f)
        {
            timerCurrent -= Time.deltaTime;
        }
        else if(spawnCount < 5)
        {
            spawnedObject = GameObject.Instantiate(spawnedObject_prefab[index], transform.position, Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody>().AddForce(transform.forward * 125f, ForceMode.Impulse);
            index++;
            spawnCount++;

            if (index == ballsCount)
            {
                index = 0;
            }
            timerCurrent = timer;
        }
    }
}
