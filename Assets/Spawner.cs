using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnedObject_prefab;
    GameObject spawnedObject;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            spawnedObject = GameObject.Instantiate(spawnedObject_prefab, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody>().AddForce(transform.forward * 250.0f, ForceMode.Impulse);
            timer = 1.0f;
        }
    }
}
