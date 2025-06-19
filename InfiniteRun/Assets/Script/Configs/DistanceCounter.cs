using UnityEngine;
using UnityEngine.UI; // ou TMPro se estiver usando TextMeshPro
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.XR;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    private int distance = 0;
    private float timer = 0f;
    private float timePerMeter = 0.1f;

    [SerializeField] private Canvas m_upgradeCanvas;
    [SerializeField] private GameObject m_objectsSpawner;

    private bool isPaused = false;

    void Start()
    {
        distance = 0;
        timer = 0f;
        UpdateUI();
    }

    void Update()
    {
        if (isPaused) return;

        timer += Time.deltaTime;

        while (timer >= timePerMeter)
        {
            timer -= timePerMeter;
            distance += 1;
            UpdateUI();

            if(distance % 150 == 0)
            {
                TriggerUpgradePause();
                break;
            }
        }
    }

    void UpdateUI()
    {
        distanceText.text = distance + " m";
    }

    public void TriggerUpgradePause()
    {
        isPaused = true;
        m_upgradeCanvas.enabled = true;
        m_objectsSpawner.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        m_upgradeCanvas.enabled = false;
        m_objectsSpawner.SetActive(true);
    }
}
