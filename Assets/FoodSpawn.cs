using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public BoidManager bm;
    public GameObject foodPrefab;
    public List<GameObject> spawnedFood = new List<GameObject>();
    public bool FoodSpawned = false;

    public void SpawnFood(GameObject foodPrefab)
    {
        Vector3 foodPos = bm.transform.position + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));

        GameObject FOBJ = Instantiate(foodPrefab, foodPos, Quaternion.identity);
        bm.SetFoodDestination(foodPos);
        spawnedFood.Add(FOBJ);
    }

}
