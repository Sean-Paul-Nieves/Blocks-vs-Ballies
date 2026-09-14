using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerScore;
    public void StartGame() {

        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1f;
    }

    public void MainMenu() => SceneManager.LoadSceneAsync("Main_Menu");
    public void QuitGame() =>Application.Quit();

    void Start()
    {
        if (!PlayerPrefs.HasKey("High_Score"))
        {
            PlayerPrefs.SetFloat("High_Score", 0f);
        }
        else
        {
            _timerScore.text = System.TimeSpan.FromSeconds(PlayerPrefs.GetFloat("High_Score")).ToString(@"mm\:ss");
        }

        Time.timeScale = 1f;
        
    }
}
