
using System.Collections.Generic;
using UnityEngine;
public class FinalBoidManager : MonoBehaviour
{
    public FoodSpawn FS;
    [SerializeField] FinalBoidBehaviour BB;
    UIManager uiManager;

    public List<GameObject> BoidList = new List<GameObject>();
    public float numBoids = 20;
    public int TankSize = 7;
    public float TankLimiter = 2f;
    public bool foodactive = false;
    GameObject Boid;

    //Boid Settings//
    [Header("Boid Settings")]

    public float MinSpeed;

    public float MaxSpeed;

    public float nDistance;

    public float RotationSpeed;

    public float avoidanceStrength;

    public float disperseRadius;

    public Vector3 foodPos = Vector3.zero;
    public Vector3 idlePos = Vector3.zero;
    private void Awake() // Reference to UI Manager
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update() // UI Manager Settings
    {
        numBoids = uiManager.BoidSpawnNumb;
        MinSpeed = uiManager.BoidSpeedNumb;
        MaxSpeed = uiManager.BoidSpeedNumb;
        nDistance = uiManager.BoidNeighbourNumb;
        RotationSpeed = uiManager.BoidRotationSpeedNumb;
        avoidanceStrength = uiManager.BoidAvoidanceNumb;
        disperseRadius = uiManager.BoidDispereseNumb;

        if (!foodactive) //Update the idle position inside the tank while food is inactive
        {
            idlePos = this.transform.localPosition + Random.Range(2f, 4f) * new Vector3(Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize),
                                                                                        Random.Range(-TankSize, TankSize));
        }
    }

    //Function to Spawn Boids//
    public void SpawnBoids()
    {
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = this.transform.localPosition + new Vector3(Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize),
                                                                     Random.Range(-TankSize, TankSize));
            GameObject boid = (GameObject)Instantiate(uiManager.SelectedBoid[uiManager.SelectionNumb], pos, Quaternion.identity);
            BoidList.Add(boid);
            BoidList[i].GetComponent<FinalBoidBehaviour>().bmanager = this; //Reference to the BoidBehavior for each of the boids
        }
    }
}


