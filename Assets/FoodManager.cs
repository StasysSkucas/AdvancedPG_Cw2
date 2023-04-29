
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public FoodSpawn FS;
 

    public void RedFood()
    {
        FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
        FS.SpawnFood(FS.foodPrefab);
        FS.FoodSpawned = true;
    }

    public void OrangeFood()
    {
        FS = GameObject.Find("L2FoodSpawner").GetComponent<FoodSpawn>();
        FS.SpawnFood(FS.foodPrefab);
        FS.FoodSpawned = true;
    }


    public void GreenFood()
    {
        FS = GameObject.Find("L3FoodSpawner").GetComponent<FoodSpawn>();
        FS.SpawnFood(FS.foodPrefab);
        FS.FoodSpawned = true;
    }
}


