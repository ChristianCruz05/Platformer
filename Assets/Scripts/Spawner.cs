using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bullet;
    public float interval = 3f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        bullet.transform.position = transform.position;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer +=Time.deltaTime;
        while(timer >= interval)
        {
            Instantiate(bullet);
            timer -= interval;
        }
    }
}
