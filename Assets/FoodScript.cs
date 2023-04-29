using System;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public FoodSpawn FS;
    [SerializeField] int FoodHealth = 1;
    public BoidManager Bb;


    private void OnTriggerEnter(Collider col)
    {
        FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
        Bb = GameObject.Find("L1FishManager").GetComponent<BoidManager>(); //Put FishManagerClone HERE
        if (col.gameObject.CompareTag("Fish"))
        {
            if (FoodHealth > 0)
            {
                FoodHealth--;
            }

            if (FoodHealth == 0)
            {
                if (FS.spawnedFood != null) 
                { 
                    Bb.foodactive = false;
                    FS.spawnedFood.Remove(this.gameObject); 
                }
                Destroy(this.gameObject,0.5f);
            }
        }
    }
}
