
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    private void LateUpdate()
    {
        if (foodactive == false) 
        {
            idlePos = this.transform.position + Random.Range(3, 4) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize),
                                                                                 Random.Range(-TankSize, TankSize));
        } 
        else if (foodactive)
        {
            SetFoodDestination(idlePos);
        }
    }

    public void SetFoodDestination(Vector3 FoodPos)
    {
        foodactive = true;
        idlePos = FoodPos;
    }
}





//if (Boid.CompareTag("L1Fish"))
//{
//    L1Fish = true;
//    idlePos = this.transform.position + Random.Range(2, 3) * new Vector3(Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize));
//    if (FS.FoodA == true)
//    {
//        L1Fish = false;
//        idlePos = FS.foodPrefabA.transform.position;
//    }
//}

//if (Boid.CompareTag("L2Fish"))
//{
//    L2Fish = true;
//    idlePos = this.transform.position + Random.Range(4, 5) * new Vector3(Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize));
//    if (FS.FoodB == true)
//    {
//        L2Fish = false;
//        idlePos = FS.foodPrefabB.transform.position;
//    }
//}

//if (Boid.CompareTag("L3Fish"))
//{
//    L3Fish = true;
//    idlePos = this.transform.position + Random.Range(5, 6) * new Vector3(Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize),
//                                                                         Random.Range(-TankSize, TankSize));
//    if (FS.FoodC == true)
//    {
//        L3Fish = false;
//        idlePos = FS.foodPrefabC.transform.position;
//    }
//}
