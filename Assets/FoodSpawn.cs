using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodSpawn : MonoBehaviour
{
    public FinalBoidManager bm; //Final BM
    public GameObject foodPrefab;
    public List<GameObject> spawnedFood = new List<GameObject>();
    public bool FoodSpawned = false;

    //public void SpawnFood(GameObject foodPrefab)
    //{
    //    GameObject.FindGameObjectWithTag("BoidManager").GetComponent<FinalBoidBehaviour>(); ;
    //    Vector3 foodPos = bm.transform.localPosition + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize), -5, UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));
    //    GameObject FOBJ = Instantiate(foodPrefab, foodPos, Quaternion.identity);
    //    FOBJ.transform.localPosition = foodPos;
    //    bm.SetFoodDestination(foodPos);
    //    spawnedFood.Add(FOBJ);
    //    Destroy(FOBJ, 11f );
    //    bm.foodactive = false;
     
    //}


    public IEnumerator SpawnFood(GameObject foodPrefab)
    {
        GameObject.FindGameObjectWithTag("BoidManager").GetComponent<FinalBoidBehaviour>(); ;
        Vector3 foodPos = bm.transform.localPosition + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize), -5, UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));
        GameObject FOBJ = Instantiate(foodPrefab, foodPos, Quaternion.identity);
        FOBJ.transform.position = foodPos;
        bm.SetFoodDestination(foodPos);
        spawnedFood.Add(FOBJ);
        yield return new WaitForSeconds(11f);
        FoodSpawned = false;
        bm.foodactive = false;
        Destroy(FOBJ);


    }

}
