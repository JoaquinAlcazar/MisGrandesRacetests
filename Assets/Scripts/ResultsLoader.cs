using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsLoader : MonoBehaviour
{
    public GameObject horseWinner;
    public GameObject WinnerName;
    public GameObject WinnerStats;
    public GameObject WinnerPortraitContainer;
    public Sprite WinnerPortrait;

    void Start()
    {
        horseWinner = GameObject.FindWithTag("Player");

        HorseBehaviour hb = horseWinner.GetComponent<HorseBehaviour>();
        if (hb != null)
        {
            WinnerName.GetComponent<TextMeshProUGUI>().text = hb.CharacterID;
            WinnerStats.GetComponent<TextMeshProUGUI>().text = $"{hb.victorias} - {hb.derrotas}";
            WinnerPortrait = horseWinner.GetComponent<SpriteRenderer>().sprite;
            WinnerPortraitContainer.GetComponent<SpriteRenderer>().sprite = WinnerPortrait;
            horseWinner.SetActive(false);
        }
        else
        {
            Debug.LogWarning("HorseBehaviour no encontrado en el ganador.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
