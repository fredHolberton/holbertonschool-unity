using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    private float timer;    // Variable pour compter le temps
    private int minutes;    // Minutes du timer
    private int seconds;    // Secondes du timer
    private int centiseconds; // Centièmes de seconde (1/100e)

    void Start()
    {
        timer = 0f;   // Initialiser le timer
        minutes = 0;   // Initialiser les minutes à 0
        seconds = 0;   // Initialiser les secondes à 0
        centiseconds = 0;  // Initialiser les centièmes à 0
    }

    void Update()
    {
        // Incrémenter le timer à chaque frame
        timer += Time.deltaTime;

        // Calculer les minutes et secondes
        minutes = Mathf.FloorToInt(timer / 60);  // Diviser le temps total par 60 pour obtenir les minutes
        seconds = Mathf.FloorToInt(timer % 60);  // Utiliser le reste pour obtenir les secondes
        centiseconds = Mathf.FloorToInt((timer * 100) % 100);  // Multiplier par 100 pour obtenir les centièmes

        // Mettre à jour le texte avec le format "M:SS:CS"
        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
