using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volume;
    public Text volumeDisplay;

    void Awake()
    {


        if (volume != null)
        {

            float wantedVolume = PlayerPrefs.GetFloat("volume", 1f);
            volume.value = wantedVolume;
            AudioListener.volume = wantedVolume;
            volumeDisplay.text = volume.value.ToString("0.#");
            volume.onValueChanged.AddListener(delegate { SetGameVolume(volume.value); });

        }
    }
    public void SetGameVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
        volumeDisplay.text = volume.ToString("F1");


    }
}
