using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{


    public void StartGame() => SceneManager.LoadSceneAsync("Gameplay");
    public void QuitGame() =>Application.Quit();
}
