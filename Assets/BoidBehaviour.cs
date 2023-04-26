using Unity.VisualScripting;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{

    public BoidManager bmanager;
    private float velocity;
    public FoodSpawn spawn;

    void Start()
    {
        velocity = Random.Range(bmanager.MinSpeed, bmanager.MaxSpeed);
    }


    void Update()
    {
        //if(Random.Range(0,100)<2) 
        SetLimits();
        //if (Random.Range(0, 100) < 50)
        BoidBehave();

        transform.Translate(0, 0, velocity * Time.deltaTime);
    }

    public void BoidBehave()
    {
        GameObject[] FOS;
        FOS = bmanager.BoidArray;

        float gSpeed = 0f; 
        float distance;
        int groupSize = 0;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        Vector3 disperse = Vector3.zero;

        Vector3 foodDir = Vector3.zero;
        bool foodFound = false;
        
        foreach (var f in FOS) 
        {
           if (f != this.gameObject)
           {
                distance = Vector3.Distance(f.transform.position, this.transform.position);
                if (distance <= bmanager.nDistance)
                {
                    center += f.transform.position;
                    groupSize++;

                    if (distance < bmanager.avoidanceStrength) 
                    {
                        avoid += avoid + (this.transform.position - f.transform.position);
                    }

                    BoidBehaviour newBoidBehaviour = f.GetComponent<BoidBehaviour>();
                    gSpeed = gSpeed + newBoidBehaviour.velocity;
                }
           }

        }

        if (groupSize > 0) 
        {
            center = center / groupSize + (bmanager.idlePos - this.transform.position);
            velocity = gSpeed / groupSize;
            Vector3 direction = ((center + avoid) - transform.position);

            GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
            if(foodObjects.Length > 0)
            {
                float closeDistance = float.MaxValue;
                foreach(GameObject food in foodObjects)
                {
                    distance = Vector3.Distance(food.transform.position, this.transform.position);
                    if(distance < closeDistance)
                    {
                        closeDistance = distance;
                        foodDir = food.transform.position - this.transform.position;
                        foodFound = true;
                    }
                }
            }
            if(foodFound)
            {
                Vector3 directionF = foodDir;
                if (directionF != Vector3.zero)
                {
                    //Velocity = FoodBoost here
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionF), bmanager.RotationSpeed * Time.deltaTime);
                }
            }
           if (groupSize >= 30)
           {
                foreach (var f in FOS)
                {
                    if (f != this.gameObject)
                    {
                        distance = Vector3.Distance(f.transform.position, this.transform.position);
                        if (distance <= bmanager.disperseRadius)
                        {
                            disperse += (this.transform.position - f.transform.position) / distance;
                        }
                    }
                }
                direction += disperse;
           }
                
           if (direction != Vector3.zero)
           {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), bmanager.RotationSpeed * Time.deltaTime);
           }
        }
        if (groupSize == 0) return;
    }


    private void SetLimits()
    {
        Bounds b = new Bounds(bmanager.transform.position, new Vector3(bmanager.TankSize, bmanager.TankSize, bmanager.TankSize) * bmanager.TankLimiter);

        if (b.Contains(transform.position))
        {
            return;
        }
        Vector3 turnDirection = (bmanager.transform.position - this.transform.position);
        if (turnDirection.magnitude > 0.001f) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnDirection), bmanager.RotationSpeed * Time.deltaTime);
        }
    }
}
