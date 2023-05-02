using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodScript : MonoBehaviour
{
    public FoodSpawn FS;
    int FoodHealth = 1;
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
        rb.AddForce(Vector3.up * FloatForce); //Floats the Food
        ActualFoodPos = transform.position; //Set Food Position
    }
    private void OnTriggerEnter(Collider col) //Check collision with the Food Object
    {
        FS = GameObject.FindGameObjectWithTag("FoodSpawner").GetComponent<FoodSpawn>();
        Bb = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        if (FoodHealth > 0)
        {
            FoodHealth--;
        }

        if (FoodHealth == 0)
        {
                Bb.foodactive = false;
                Destroy(this.gameObject, 0.5f);
        }
    }
}
