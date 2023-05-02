
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public FoodSpawn FS;
    public FinalBoidManager FB;
    public void RedFood() //Spawn through Food Manager
    {
        FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
        FB = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        FS.StartCoroutine(FS.SpawnFood());
        FS.FoodSpawned = true;
        FB.foodactive = true;
    }
}


