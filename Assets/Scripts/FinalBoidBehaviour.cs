using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void BoidBehave() // Base Boid Behaviour Function with Cohesion, Avoidance and Dispersion
    {
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        List<GameObject> FOS;
        FOS = bmanager.BoidList;

        float gSpeed = 0f;
        float distance;
        int groupSize = 0;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        Vector3 disperse = Vector3.zero;
        Vector3 alignment = Vector3.zero;

        foreach (var f in FOS) // Calculate the average distance from each neighbour
        {
            if (f != this.gameObject)
            {
                distance = Vector3.Distance(f.transform.localPosition, this.transform.localPosition);
                if (distance <= bmanager.nDistance)
                {
                    center = f.transform.localPosition;
                    groupSize++;

                    if (distance < bmanager.avoidanceStrength) //Calculate avoidance based on variable
                    {
                        avoid += avoid + (this.transform.localPosition - f.transform.localPosition);
                    }

                    FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>(); //Set a new group or Flock
                    gSpeed += newBoidBehaviour.velocity; //Set the group Velocity
                }
            }
        }

        if (groupSize > 0) // Group Size Greated that 0 to Apply Flocking
        {
            center = center / groupSize + (bmanager.idlePos - this.transform.localPosition);
            velocity = gSpeed / groupSize;
            alignment /= groupSize; //Calculate the Alignment 
            Vector3 direction = ((center + avoid + alignment) - transform.localPosition);

            if (groupSize > 30) // Group Size Greater than 30 The groups will disperse
            {
                foreach (var f in FOS)
                {
                    if (f != this.gameObject)
                    {
                        distance = Vector3.Distance(f.transform.localPosition, this.transform.localPosition);
                        if (distance < bmanager.disperseRadius)
                        {
                            disperse += (this.transform.localPosition - f.transform.localPosition) / distance; //Calculate the Dispersion based on the group size and radius
                        }
                        FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>();
                        gSpeed += newBoidBehaviour.velocity * 2;
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

    // Tank Size Limit function ---- IF Bounds are reached, Boids will turn around towards the Flock manager Location
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

    // Food Tracking Function - When FoodActive and FoodSpawned Booleans are set to True
    public void TrackFood()
    {
        foodmanager = GameObject.FindGameObjectWithTag("Food").GetComponent<FoodScript>();
        bmanager = GameObject.Find("FinalBoidManager").GetComponent<FinalBoidManager>();
        FS = GameObject.FindGameObjectWithTag("FoodSpawner").GetComponent<FoodSpawn>();
        if (FS.FoodSpawned && bmanager.foodactive)
        {

            float distanceToFood = Vector3.Distance(foodmanager.ActualFoodPos, this.transform.localPosition);
            if (distanceToFood <= bmanager.nDistance) //If Food Distance is Less that average Neighbour distance
            {
                Vector3 foodDirection = (foodmanager.ActualFoodPos - this.transform.localPosition);
                if (foodDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foodDirection.normalized), bmanager.RotationSpeed * Time.deltaTime);
                }
                transform.localPosition += Time.deltaTime * velocity * transform.forward;
            }
            else if (distanceToFood >= bmanager.nDistance) // If Food Distance is Greater that average Neighbour distance
            {
                Vector3 foodDirection = (foodmanager.ActualFoodPos - this.transform.localPosition);
                if (foodDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foodDirection.normalized), bmanager.RotationSpeed * Time.deltaTime);
                }
                transform.localPosition += Time.deltaTime * (velocity / 3) * transform.forward;
            }
        }
    }
}

