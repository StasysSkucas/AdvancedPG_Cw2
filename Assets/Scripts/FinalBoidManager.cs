
using System.Collections.Generic;
using UnityEngine;
public class FinalBoidManager : MonoBehaviour
{
    public FoodSpawn FS;
    [SerializeField] FinalBoidBehaviour BB;
    UIManager uiManager;

    public List<GameObject> BoidArray = new List<GameObject>();
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
    }

    private void Update()
    {
        numBoids = uiManager.BoidSpawnNumb;
        MinSpeed = uiManager.BoidSpeedNumb;
        MaxSpeed = uiManager.BoidSpeedNumb;
        nDistance = uiManager.BoidNeighbourNumb;
        RotationSpeed = uiManager.BoidRotationSpeedNumb;
        avoidanceStrength = uiManager.BoidAvoidanceNumb;
        disperseRadius = uiManager.BoidDispereseNumb;

        if (!foodactive)
        {
            idlePos = this.transform.localPosition + Random.Range(2f, 4f) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize));
        }
    }

    public void SpawnBoids()
    {
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = this.transform.localPosition + new Vector3(Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize));
            GameObject boid = (GameObject)Instantiate(uiManager.SelectedBoid[uiManager.SelectionNumb], pos, Quaternion.identity);
            BoidArray.Add(boid);
            BoidArray[i].GetComponent<FinalBoidBehaviour>().bmanager = this;
        }
    }
}

