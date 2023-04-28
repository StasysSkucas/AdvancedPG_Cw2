using UnityEngine;

public class FinalBoidBehaviour : MonoBehaviour
{
    public FinalBoidManager bmanager;
    private float velocity;
    public FoodSpawn FoodLoc;

    void Start()
    {
        velocity = Random.Range(bmanager.MinSpeed, bmanager.MaxSpeed);
          
    }


    void Update()
    {
        SetLimits();

        if(Random.Range(0, 100) < 10f)
        BoidBehave();

        transform.Translate(0, 0, velocity * Time.deltaTime);
    }

    public void BoidBehave()
    {
        //bmanager = GameObject.Find("L1FishManager").GetComponent<FinalBoidManager>();
        //bmanager = GameObject.Find("L2FishManager").GetComponent<FinalBoidManager>();
        bmanager = GameObject.Find("FinalBoidManager(Clone)").GetComponent<FinalBoidManager>();
        GameObject[] FOS;
        FOS = bmanager.BoidArray;

        float gSpeed = 0f; 
        float distance;
        int groupSize = 0;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        Vector3 disperse = Vector3.zero;
        bool foodFound = false;
        Vector3 foodDir = Vector3.zero;  

        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
        if (foodObjects.Length > 0)
        {
            float closeDistance = float.MaxValue;
            foreach (GameObject food in foodObjects)
            {
                distance = Vector3.Distance(food.transform.position, this.transform.position);
                if (distance < closeDistance)
                {
                    closeDistance = distance;
                    foodDir = (food.transform.position - this.transform.position).normalized;
                    foodFound = true;
                }
            }
        }
        if (foodFound)
        {
            if (foodDir.magnitude > 0.001f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foodDir), bmanager.RotationSpeed * Time.deltaTime);
            }
        }

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

                    FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>();
                    gSpeed += newBoidBehaviour.velocity;
                }
           }
        }

        if (groupSize > 0) 
        {
            center = center / groupSize + (bmanager.idlePos - this.transform.position);
            velocity = gSpeed / groupSize;
            Vector3 direction = ((center + avoid) - transform.position);



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
                        FinalBoidBehaviour newBoidBehaviour = f.GetComponent<FinalBoidBehaviour>();
                        gSpeed -= gSpeed + newBoidBehaviour.velocity; //I removed += For Dispersion Test 
                    }
                }
                direction += disperse;
            }
                
            if (direction.magnitude > 0.001f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), bmanager.RotationSpeed * Time.deltaTime);
            }
        }
        if (groupSize == 0) return;
    }


    private void SetLimits()
    {
        Bounds b = new(bmanager.transform.position, new Vector3(bmanager.TankSize, bmanager.TankSize, bmanager.TankSize) * bmanager.TankLimiter);

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





//GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
//if (foodObjects.Length > 0)
//{
//    float closeDistance = float.MaxValue;
//    foreach (GameObject food in foodObjects)
//    {
//        distance = Vector3.Distance(food.transform.position, this.transform.position);
//        if (distance < closeDistance)
//        {
//            closeDistance = distance;
//            foodDir = bmanager.idlePos - this.transform.position;
//            foodFound = true;
//        }
//    }
//}
//if (foodFound)
//{
//    Vector3 directionF = foodDir;
//    if (directionF != Vector3.zero)
//    {
//        //Velocity = FoodBoost here
//        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionF), bmanager.RotationSpeed * Time.deltaTime);
//    }
//}
//GameObject[] foodObjectsA = GameObject.FindGameObjectsWithTag("L1Food");
//GameObject[] foodObjectsB = GameObject.FindGameObjectsWithTag("L2Food");
//GameObject[] foodObjectsC = GameObject.FindGameObjectsWithTag("L3Food");

//if (foodObjectsA.Length > 0 && gameObject.CompareTag("L1Fish"))
//{
//    float closeDistance = float.MaxValue;
//    foreach (GameObject food in foodObjectsA)
//    {
//        distance = Vector3.Distance(food.transform.position, this.transform.position);
//        if (distance < closeDistance)
//        {
//            closeDistance = distance;
//            foodDir = food.transform.position - this.transform.position;
//            foodFound = true;
//        }
//    }
//}
//else if (foodObjectsB.Length > 0 && gameObject.CompareTag("L2Fish"))
//{
//    float closeDistance = float.MaxValue;
//    foreach (GameObject food in foodObjectsB)
//    {
//        distance = Vector3.Distance(food.transform.position, this.transform.position);
//        if (distance < closeDistance)
//        {
//            closeDistance = distance;
//            foodDir = food.transform.position - this.transform.position;
//            foodFound = true;
//        }
//    }
//}
//else if (foodObjectsC.Length > 0 && gameObject.CompareTag("L3Fish"))
//{
//    float closeDistance = float.MaxValue;
//    foreach (GameObject food in foodObjectsC)
//    {
//        distance = Vector3.Distance(food.transform.position, this.transform.position);
//        if (distance < closeDistance)
//        {
//            closeDistance = distance;
//            foodDir = food.transform.position - this.transform.position;
//            foodFound = true;
//        }
//    }
//}