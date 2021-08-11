using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cubeCountText;
    [SerializeField] private Slider iterationsSl;
    [SerializeField] private TextMeshProUGUI iterationsText;
    [SerializeField] private MengerSponge mengerSpronge;
    [SerializeField] private Toggle inverseToggle;
    [Range(1, 6)]
    private int iterations = 4;
    public int Iterations
    {
        get { return iterations; }

        set
        {
            if (iterations == value) return;
            iterations = value;

        }
    }
    private int cubeCount = 0;
    public int CubeCount
    {
        get { return cubeCount; }

        set
        {
            if (cubeCount == value) return;
            cubeCount = value;
            if (OnCubeCountChange != null)
            {
                OnCubeCountChange(cubeCount);
            }
        }
    }
    public delegate void OnCubeCountChangeDel(int newCubeCount);
    public event OnCubeCountChangeDel OnCubeCountChange;


  
    private void Awake()
    {
        OnCubeCountChange += UpdateCubeCountUI;
    }

    private void UpdateCubeCountUI(int newCubeCount)
    {
        cubeCountText.text = "Cube Count = " + newCubeCount;
    }

    public void IterationsChanged()
    {
        iterations = Convert.ToInt32(iterationsSl.value);
        iterationsText.text = "Iterations = " + iterations;
    }

    public void CreateNewMengerSponge()
    {
        mengerSpronge.CreateMengerBox(iterations, inverseToggle.isOn);
    }
}
