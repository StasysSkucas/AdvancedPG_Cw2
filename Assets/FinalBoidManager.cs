
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FinalBoidManager : MonoBehaviour
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

    public float MinSpeed;

    public float MaxSpeed;

    public float nDistance;

    public float RotationSpeed;

    public float avoidanceStrength;

    public float disperseRadius;

    public Vector3 foodPos = Vector3.zero;
    public Vector3 idlePos = Vector3.zero;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        numBoids = uiManager.BoidSpawnNumb;
        MinSpeed = uiManager.BoidSpeedNumb;
        MaxSpeed = uiManager.BoidSpeedNumb;
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
            Vector3 pos = this.transform.localPosition + new Vector3(Random.Range(-TankSize, TankSize),
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
            if (!foodactive)
            {
                idlePos = this.transform.localPosition + Random.Range(2, 5) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                          Random.Range(-TankSize, TankSize),
                                                                                          Random.Range(-TankSize, TankSize));
            }
            else if (foodactive)
            {
                SetFoodDestination(foodPos);
            }
        }
    }
    public void SetFoodDestination(Vector3 FoodPos)
    {
        foodactive = true;
        foodPos = FoodPos;
    }
}

