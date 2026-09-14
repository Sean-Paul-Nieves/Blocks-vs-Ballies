using TMPro;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{

    [SerializeField] GameObject deathPanel;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI resultText;

    void Start()
    {
        deathPanel.SetActive(false);
    }

    void OnDisable()
    {
        resultText.text = System.TimeSpan.FromSeconds(timer._timerData).ToString(@"mm\:ss");
        Time.timeScale = 0f;
        deathPanel.SetActive(true);

        if (timer._timerData > PlayerPrefs.GetFloat("High_Score"))
        {
            PlayerPrefs.SetFloat("High_Score", timer._timerData);
        }
    }
}
