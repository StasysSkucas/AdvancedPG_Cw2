using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodSpawn : MonoBehaviour
{
    public FinalBoidManager bm;
    public GameObject foodPrefabA;
    public GameObject foodPrefabB;
    public GameObject foodPrefabC;  
    public List<GameObject> spawnedFood = new List<GameObject>();
    public bool FoodSpawned = false;




    public IEnumerator SpawnFood()
    {
        GameObject.FindGameObjectWithTag("BoidManager").GetComponent<FinalBoidBehaviour>(); ;
        Vector3 foodPos = bm.transform.localPosition + new Vector3(UnityEngine.Random.Range(-bm.TankSize, bm.TankSize), -5, UnityEngine.Random.Range(-bm.TankSize, bm.TankSize));

        int FoodChance = UnityEngine.Random.Range(0,100);

        if (FoodChance < 30)
        {
            GameObject FOBJ = Instantiate(foodPrefabA, foodPos, Quaternion.identity);
            FOBJ.transform.position = foodPos;
            spawnedFood.Add(FOBJ);
            yield return new WaitForSeconds(11f);
            FoodSpawned = false;
            bm.foodactive = false;
            Destroy(FOBJ);
        }
        else if (FoodChance < 40)
        {
            GameObject FOBJ = Instantiate(foodPrefabB, foodPos, Quaternion.identity);
            FOBJ.transform.position = foodPos;
            spawnedFood.Add(FOBJ);
            yield return new WaitForSeconds(11f);
            FoodSpawned = false;
            bm.foodactive = false;
            Destroy(FOBJ);
        }
        else 
        {
            GameObject FOBJ = Instantiate(foodPrefabC, foodPos, Quaternion.identity);
            spawnedFood.Add(FOBJ);
            yield return new WaitForSeconds(11f);
            FoodSpawned = false;
            bm.foodactive = false;
            Destroy(FOBJ);
        }
    }
}
