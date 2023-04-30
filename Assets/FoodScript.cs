using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodScript : MonoBehaviour
{
    public FoodSpawn FS;
    [SerializeField] int FoodHealth = 1;
    public FinalBoidManager Bb;
     float FloatForce = 0.01f;
    Rigidbody rb;
   public bool inWater = false;
    public Vector3 ActualFoodPos;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Water());
    }

    private IEnumerator Water()
    {
        yield return new WaitForSeconds(1f);
        inWater = true;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up * FloatForce); // this floats the food up
        ActualFoodPos = transform.position;
    }
    private void OnTriggerEnter(Collider col)
    {
        FS = GameObject.FindGameObjectWithTag("FoodSpawner").GetComponent<FoodSpawn>();
        Bb = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>(); //Put FishManagerClone HERE

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
            Destroy(this.gameObject, 0.5f);
        }

        if (col.gameObject.CompareTag("Fish"))
        {
            Debug.Log("EAT BALLS");
        
        }
    }
}
