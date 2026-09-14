using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    public float _timerData;

    // Update is called once per frame
    void Update()
    {
        _timerData = Time.timeSinceLevelLoad;
        _timerText.text = System.TimeSpan.FromSeconds(_timerData).ToString(@"mm\:ss");
    }

    public void FlushData() {
        _timerData = 0f;
        _timerText.text = string.Empty;
    }
}
