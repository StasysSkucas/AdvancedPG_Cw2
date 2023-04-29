using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    public BoidManager bmanager;
    private float velocity;
    public FoodSpawn FS;

    void Start()
    {
        velocity = Random.Range(bmanager.MinSpeed, bmanager.MaxSpeed);
        bmanager = GameObject.Find("L1FishManager").GetComponent<BoidManager>();
        FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
    }


    void LateUpdate()
    {
        SetLimits();

        TrackFood();

        if(Random.Range(0, 100) < 10f)
        BoidBehave();

        transform.Translate(0, 0, velocity * Time.deltaTime);
    }

    public void BoidBehave()
    {
        bmanager = GameObject.Find("L1FishManager").GetComponent<BoidManager>();
        GameObject[] FOS;
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

                    BoidBehaviour newBoidBehaviour = f.GetComponent<BoidBehaviour>();
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
                        BoidBehaviour newBoidBehaviour = f.GetComponent<BoidBehaviour>();
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
        if (FS.FoodSpawned && bmanager.foodactive)
        {
            bmanager = GameObject.Find("L1FishManager").GetComponent<BoidManager>();
            FS = GameObject.Find("L1FoodSpawner").GetComponent<FoodSpawn>();
            float distanceToFood = Vector3.Distance(bmanager.foodPos, this.transform.localPosition);
            if (distanceToFood <= bmanager.nDistance)
            {
                Vector3 foodDirection = (bmanager.foodPos - this.transform.localPosition).normalized;
                if (foodDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foodDirection.normalized), bmanager.RotationSpeed * Time.deltaTime);
                }
                transform.localPosition += Time.deltaTime * velocity * transform.forward;
            }
        }

    }
}
