using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public int SelectionNumb = 0;

    public Slider SpawnAmountSlider;
    public Slider SwimSpeedSlider;
    public Slider SwimMaxSpeedSlider;
    public Slider RotationSpeedSlider;
    public Slider DisperseRadiusSlider;
    public Slider AvoidanceStrenghtSlider;
    public Slider NeighbourDistanceSlider;

    public TMP_Text SpawnNumb;
    public TMP_Text SpeedNumb;
    public TMP_Text SpeedMaxNumb;
    public TMP_Text RotationSpeedNumb;
    public TMP_Text DispereseNumb;
    public TMP_Text AvoidanceNumb;
    public TMP_Text NeighbourNumb;

    public float BoidSpawnNumb = 5;
    public float BoidSpeedNumb = 1;
    public float BoidMaxSpeedNumb = 2;
    public float BoidRotationSpeedNumb = 1;
    public float BoidDispereseNumb = 5;
    public float BoidAvoidanceNumb = 4;
    public float BoidNeighbourNumb = 3.6f;


    public string[] FishNames;

    public TMP_Text SelectionName;


    public GameObject BoidManagerToSpawn;
    public GameObject[] SelectedBoid;
    void Start()
    {
   


        SelectionNumb = 0;
        SelectionName.text = FishNames[0];
       
        
    }

  

    public void RightArrow()
    {
        if (SelectionNumb < FishNames.Length - 1)
        {
            SelectionNumb++;
        }
        else
        {
            SelectionNumb = 0;
        }
        SelectionName.text = FishNames[SelectionNumb];
    }

    public void LeftArrow()
    {
        if (SelectionNumb == 0)
        {
            SelectionNumb = FishNames.Length - 1;
        }
        else
        {
            SelectionNumb--;
        }
        SelectionName.text = FishNames[SelectionNumb];
    }




    public void ReadSpawnNumb(float value)
    {
        BoidSpawnNumb = value;
        SpawnNumb.text = value.ToString();

    }
    public void ReadSpeedNumb(float value)
    {
        BoidSpeedNumb = value;
        SpeedNumb.text = value.ToString("F1");

    }

    public void ReadMaxSpeedNumb(float value)
    {
        BoidMaxSpeedNumb = value;
        SpeedMaxNumb.text = value.ToString("F1");

    }
    public void ReadRotationSpeedNumb(float value)
    {
        BoidRotationSpeedNumb = value;
        RotationSpeedNumb.text = value.ToString("F1");

    }
    public void ReadDispereseNumb(float value)
    {
        BoidDispereseNumb = value;
        DispereseNumb.text = value.ToString("F1");

    }
    public void ReadAvoidanceNumb(float value)
    {
        BoidAvoidanceNumb = value;
        AvoidanceNumb.text = value.ToString("F1");

    }
    public void ReadNeighbourNumb(float value)
    {
        BoidNeighbourNumb  = value;
        NeighbourNumb.text = value.ToString("F1");

    }


    public void SpawnButton()
    {
        Instantiate(BoidManagerToSpawn, new Vector3(8f, 10f, 25f), Quaternion.identity);
    }

    public void ResetAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



