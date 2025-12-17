using UnityEngine;
using UnityEngine.Rendering;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public float spawnrate=3f;
    public float timer;

    void Start()
    {
      SpawnZombie();
      timer=spawnrate;  
    }

    void Update()
    {
        
        if(timer>0)
        {
            timer-=Time.deltaTime;
        }
        else
        {
            timer=spawnrate;
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        Instantiate(zombie, transform.position, transform.rotation);
    }

}
