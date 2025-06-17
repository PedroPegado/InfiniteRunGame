using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMainMenu : MonoBehaviour
{
    [SerializeField] private Button m_button;
    public Canvas mainMenuCanvas;
    public CanvasGroup mainMenuCanvasGroup;
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenuCanvas.enabled = false;
        mainMenuCanvasGroup.DOFade(0, 3);
        Time.timeScale = 1f;
    }
}
