using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        dayUI.text = $"Day:{dayInGame}";
    }



    public int dayInGame = 1;
    public TextMeshProUGUI dayUI;
    public void TriggerNextDay()
    {
        dayInGame += 1;
        dayUI.text = $"Day:{dayInGame}";
    }

}
