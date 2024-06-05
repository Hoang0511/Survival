using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectSlider;

    

    public void MasterVolume()
    {
        masterSlider.value = SoundManager.Instance.dropItemSound.volume;
    }

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }
}
