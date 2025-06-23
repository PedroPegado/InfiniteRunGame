using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject m_objectsSpawner;

    [SerializeField] private GameObject m_upgradePrefab;
    private GameObject m_currentUpgradeUI;

    private bool isPaused = false;

    public Camera mainCamera;

    void Awake()
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

            if ((distance + 30) % m_upgradeAppear == 0)
            {
                m_objectsSpawner.SetActive(false);
            }

            if (distance % m_upgradeAppear == 0)
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
        Time.timeScale = 0f;

        m_currentUpgradeUI = Instantiate(m_upgradePrefab);

        var canvas = m_currentUpgradeUI.GetComponentInChildren<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
        {
            canvas.worldCamera = mainCamera;
        }

        var randomUpgrade = m_currentUpgradeUI.GetComponent<RandomUpgrade>();
        foreach (var button in randomUpgrade.m_buttonList)
        {
            button.m_distanceCounter = this;
        }
    }




    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (m_currentUpgradeUI != null)
            Destroy(m_currentUpgradeUI);

        m_objectsSpawner.SetActive(true);
    }

}
