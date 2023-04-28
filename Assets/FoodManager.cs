
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public FoodSpawn FS;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
            FS.SpawnFood(FS.foodPrefab);
            FS.FoodSpawned = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FS = GameObject.Find("L3FoodSpawner").GetComponent<FoodSpawn>();
            FS.SpawnFood(FS.foodPrefab);
            FS.FoodSpawned = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FS = GameObject.Find("L3FoodSpawner").GetComponent<FoodSpawn>();
            FS.SpawnFood(FS.foodPrefab);
            FS.FoodSpawned = true;
        }
    }
}
