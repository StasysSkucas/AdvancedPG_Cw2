using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFoodSpawn : MonoBehaviour
{
    public FinalBoidManager bm; //Final BM
    public GameObject foodPrefab;
    public List<GameObject> spawnedFood = new List<GameObject>();
    public bool FoodSpawned = false;
 

    public void SpawnFood(GameObject foodPrefab)
    {
        bm = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        Vector3 foodPos = bm.transform.localPosition + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize), -5, UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));
        GameObject FOBJ = Instantiate(foodPrefab, foodPos, Quaternion.identity);
        FOBJ.transform.localPosition = foodPos;
        bm.SetFoodDestination(foodPos);
        spawnedFood.Add(FOBJ);
        Destroy(FOBJ, 11f);
    }

    


}
