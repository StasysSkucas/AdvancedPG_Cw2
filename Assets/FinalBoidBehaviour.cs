using System.Collections.Generic;
using UnityEngine;

public class FinalBoidBehaviour : MonoBehaviour
{
    public FinalBoidManager bmanager;
    public FoodScript foodmanager;
    private float velocity;
    public FoodSpawn FS;

    void Start()
    {
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        FS = GameObject.FindGameObjectWithTag("FoodSpawner").GetComponent<FoodSpawn>();
 
        velocity = Random.Range(bmanager.MinSpeed, bmanager.MaxSpeed);
    }


    void Update()
    {
        SetLimits();

        if (bmanager.foodactive)
        {  
            TrackFood();
        }
        else
        {
               if (Random.Range(0, 100) < 10f)
            BoidBehave();
        }
    

     

        transform.Translate(0, 0, velocity * Time.deltaTime);
    }

    public void BoidBehave()
    {
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        List<GameObject> FOS;
        FOS = bmanager.BoidArray;

        float gSpeed = 0f;
        float distance;
        int groupSize = 0;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        Vector3 disperse = Vector3.zero;

        foreach (var f in FOS)
        {
            if (f != this.gameObject)
            {
                distance = Vector3.Distance(f.transform.localPosition, this.transform.localPosition);
                if (distance <= bmanager.nDistance)
                {
                    center += f.transform.localPosition;
                    groupSize++;

                    if (distance < bmanager.avoidanceStrength)
                    {
                        avoid += avoid + (this.transform.localPosition - f.transform.localPosition);
                    }

                    FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>();
                    gSpeed += newBoidBehaviour.velocity;
                }
            }
        }

        if (groupSize > 0)
        {
            center = center / groupSize + (bmanager.idlePos - this.transform.localPosition);
            velocity = gSpeed / groupSize;
            Vector3 direction = ((center + avoid) - transform.localPosition);

            if (groupSize >= 30)
            {
                foreach (var f in FOS)
                {
                    if (f != this.gameObject)
                    {
                        distance = Vector3.Distance(f.transform.localPosition, this.transform.localPosition);
                        if (distance <= bmanager.disperseRadius)
                        {
                            disperse += (this.transform.localPosition - f.transform.localPosition) / distance;
                        }
                        FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>();
                        gSpeed += gSpeed + newBoidBehaviour.velocity * 2;
                    }
                }
                direction += disperse;
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), bmanager.RotationSpeed * Time.deltaTime);
            }
        }
        if (groupSize == 0) return;
    }


    private void SetLimits()
    {
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        Bounds b = new(bmanager.transform.localPosition, new Vector3(bmanager.TankSize, bmanager.TankSize, bmanager.TankSize) * bmanager.TankLimiter);

        if (b.Contains(transform.localPosition))
        {
            return;
        }
        Vector3 turnDirection = (bmanager.transform.localPosition - this.transform.localPosition);
        if (turnDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnDirection.normalized), bmanager.RotationSpeed * Time.deltaTime);
        }
    }

    public void TrackFood()
    {
        foodmanager = GameObject.FindGameObjectWithTag("Food").GetComponent<FoodScript>();
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        FS = GameObject.FindGameObjectWithTag("FoodSpawner").GetComponent<FoodSpawn>();
        if (FS.FoodSpawned && bmanager.foodactive)
        {

            float distanceToFood = Vector3.Distance(foodmanager.ActualFoodPos, this.transform.localPosition);
            if (distanceToFood <= bmanager.nDistance)
            {
                Vector3 foodDirection = (foodmanager.ActualFoodPos - this.transform.localPosition).normalized;
                if (foodDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foodDirection.normalized), bmanager.RotationSpeed * Time.deltaTime);
                }
                transform.localPosition += Time.deltaTime * velocity * transform.forward;
            }
        }
    }
}

