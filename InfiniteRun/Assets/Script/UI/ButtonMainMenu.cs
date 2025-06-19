using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonMainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
