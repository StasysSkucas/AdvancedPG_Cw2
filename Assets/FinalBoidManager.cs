
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FinalBoidManager: MonoBehaviour
{   
    public FoodSpawn FS;
    [SerializeField] BoidBehaviour BB;
    UIManager uiManager;
    public GameObject[] BoidArray;
    public float numBoids = 20;
    public int TankSize = 6;
    public float TankLimiter = 2f;
    public bool foodactive = false;
    GameObject Boid;
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
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        numBoids = uiManager.BoidSpawnNumb;
        MinSpeed = uiManager.BoidSpeedNumb;
        MaxSpeed = uiManager.BoidSpeedNumb +1;
        nDistance = uiManager.BoidNeighbourNumb;
        RotationSpeed = uiManager.BoidRotationSpeedNumb;
        avoidanceStrength = uiManager.BoidAvoidanceNumb;
        disperseRadius = uiManager.BoidDispereseNumb;
    }
    private void Start()
    {

    
        BoidArray = new GameObject[Mathf.FloorToInt(numBoids)];
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-TankSize, TankSize),
                                                                Random.Range(-TankSize, TankSize),
                                                                Random.Range(-TankSize, TankSize));

            BoidArray[i] = (GameObject)Instantiate(uiManager.SelectedBoid[uiManager.SelectionNumb], pos, Quaternion.identity);

            BoidArray[i].GetComponent<FinalBoidBehaviour>().bmanager = this;
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

