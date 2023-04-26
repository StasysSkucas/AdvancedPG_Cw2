using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject Food;
    public BoidManager bm;
    public bool FoodActive = false;
    public GameObject[] foodarray;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnFood();
        }
    }
    public void SpawnFood()
    {
        FoodActive = true;
        Vector3 foodPos = bm.transform.position + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize),
                                                              UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));
        GameObject FOBJ = Instantiate(Food, foodPos, Quaternion.identity);
        bm.SetFoodDestination(foodPos);
        //Array.Resize(ref foodarray, foodarray.Length + 1);
        //foodarray[foodarray.Length - 1] = FOBJ;
    }
}
