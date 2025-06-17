using UnityEngine;
using UnityEngine.UI; // ou TMPro se estiver usando TextMeshPro
using UnityEngine.SceneManagement;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    private int distance = 0;
    private float timer = 0f;
    private float timePerMeter = 0.1f;

    void Start()
    {
        distance = 0;
        timer = 0f;
        UpdateUI();
    }

    void Update()
    {
        timer += Time.deltaTime;

        while (timer >= timePerMeter)
        {
            timer -= timePerMeter;
            distance += 1;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        distanceText.text = distance + " m";
    }
}
