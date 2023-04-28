using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject BoidManagerToSpawn;
    public GameObject [] SelectedBoid;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(BoidManagerToSpawn,new Vector3(10f,10f,25f),Quaternion.identity);
        }
    }
}
