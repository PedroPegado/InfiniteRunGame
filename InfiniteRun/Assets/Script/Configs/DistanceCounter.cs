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
    public int m_upgradeAppear = 150;

    [SerializeField] private Canvas m_upgradeCanvas;
    [SerializeField] private GameObject m_objectsSpawner;

    private bool isPaused = false;

    void Awake()
    {
        distance = 0;
        timer = 0f;
        UpdateUI();
        m_upgradeCanvas.gameObject.SetActive(false);
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

            if(distance % 30 == 0)
            {
                m_objectsSpawner.SetActive(false);
            }

            if(distance % 50 == 0)
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
        m_upgradeCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        m_upgradeCanvas.gameObject.SetActive(false);
        m_objectsSpawner.SetActive(true);
    }
}
