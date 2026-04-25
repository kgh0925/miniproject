using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] private AudioSource ManagerAudioSource;
    [SerializeField] private Slider MySlider;

    private void Start()
    {
        if(SoundManager.Instance != null)
        {
            ManagerAudioSource = SoundManager.Instance.GetComponent<AudioSource>();
            MySlider.value = SoundManager.Instance.Volume;
        }
    }

    public void Volume()
    {
        if (MySlider == null || ManagerAudioSource == null) return;
        ManagerAudioSource.volume = MySlider.value;
        SoundManager.Instance.SetVolume(MySlider.value);
        PlayerPrefs.SetFloat("Volume", MySlider.value);
        PlayerPrefs.Save();

    }
}
