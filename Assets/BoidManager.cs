
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public FoodSpawn FS;
    [SerializeField] BoidBehaviour BB;
    public GameObject Boid;
    public GameObject[] BoidArray;
    public int numBoids = 20;
    public int TankSize = 6;
    public float TankLimiter = 2f;
    public bool foodactive = false;

    [Header("Boid Settings")]
    [Range(0.0f, 5.0f)]
    public float MinSpeed;
    [Range(0.0f, 5.0f)]
    public float MaxSpeed;
    [Range(1.0f, 10.0f)]
    public float nDistance;
    [Range(0.1f, 1.0f)]
    public float RotationSpeed;
    [Range(2f, 10.0f)]
    public float avoidanceStrength;
    [Range(2f, 10.0f)]
    public float disperseRadius;

    public Vector3 foodPos = Vector3.zero;
    public Vector3 idlePos = Vector3.zero;
    private void Start()
    {
        BoidArray = new GameObject[numBoids];
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = this.transform.localPosition + new Vector3(Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize));

            BoidArray[i] = (GameObject)Instantiate(Boid, pos, Quaternion.identity);

            BoidArray[i].GetComponent<BoidBehaviour>().bmanager = this;

        }
    }

    private void LateUpdate()
    {
        if (!foodactive) 
        {
            idlePos =  this.transform.localPosition + Random.Range(2f,4f) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize));
        } 
        else if (foodactive)
        {
            SetFoodDestination(foodPos);
        }
    }

    public void SetFoodDestination(Vector3 FoodPos)
    {
        foodactive = true;
        foodPos = FoodPos;
    }
}


