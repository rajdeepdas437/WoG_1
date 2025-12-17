using UnityEngine;

public class NinjaSpawnScript : MonoBehaviour
{
    public GameObject ninja;
    public float spawnrate=5f;
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
        Instantiate(ninja, transform.position, transform.rotation);
    }
}
