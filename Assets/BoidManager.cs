
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

    private bool L1Fish = false;
    private bool L2Fish = false;
    private bool L3Fish = false;
    public Vector3 FoodPos = Vector3.zero;

    public Vector3 idlePos = Vector3.zero;
    private void Start()
    {
        
        BoidArray = new GameObject[numBoids];
        
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-TankSize, TankSize),
                                                                Random.Range(-TankSize, TankSize), 
                                                                Random.Range(-TankSize, TankSize));

            BoidArray[i] = (GameObject)Instantiate(Boid, pos, Quaternion.identity);

            BoidArray[i].GetComponent<BoidBehaviour>().bmanager = this;
        }
    }

    public void Update()
    {


    }
    private void LateUpdate()
    {
        if (Boid.CompareTag("L1Fish"))
        {
            L1Fish = true;
            idlePos = this.transform.position + Random.Range(3, 4) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize));
            if(FS.FoodActive == true)
            {
                L1Fish = false;
                idlePos = FS.Food.transform.position;
            }
        }

        if (Boid.CompareTag("L2Fish"))
        {
            L2Fish = true;
            idlePos = this.transform.position + Random.Range(3, 4) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize));
        }

        if (Boid.CompareTag("L3Fish"))
        {
            L3Fish = true;
            idlePos = this.transform.position + Random.Range(3, 4) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize));
        }
    }

    public void SetFoodDestination( Vector3 FoodPos)
    {
        idlePos = FoodPos;
    }


    


}
