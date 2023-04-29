using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public FinalBoidManager bm;
    public GameObject foodPrefab;
    public List<GameObject> spawnedFood = new List<GameObject>();
    public bool FoodSpawned = false;

    private void Start()
    {
  
    }

    public void SpawnFood(GameObject foodPrefab)
    {
        bm = GameObject.Find("FinalBoidManager(Clone)").GetComponent<FinalBoidManager>();
        Vector3 foodPos = bm.transform.position + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));

        GameObject FOBJ = Instantiate(foodPrefab, foodPos, Quaternion.identity);
        FOBJ.transform.localPosition = foodPos;
        bm.SetFoodDestination(foodPos);
        spawnedFood.Add(FOBJ);
        //Destroy(FOBJ, 4f); TURN ON LATER
    }

}
