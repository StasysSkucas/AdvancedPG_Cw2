
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public FoodSpawn FS;
    public void RedFood()
    {
        FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
        FS.StartCoroutine(FS.SpawnFood());
        FS.FoodSpawned = true;
    }
}


