using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    [SerializeField] TextMeshProUGUI _volumeText;

    public void OnSliderChanged()
    {
        _volumeText.text = Mathf.RoundToInt(_volumeSlider.value * 100).ToString();
        AudioSource audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        audioSource.volume = _volumeSlider.value;
    }
}
