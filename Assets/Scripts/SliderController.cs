using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float oldVolume;

    [SerializeField] private Sprite MaxVolume;
    [SerializeField] private Sprite MidVolume;
    [SerializeField] private Sprite NoVolume;
    [SerializeField] private Image volumeImage;

    private void Start(){
        if(!PlayerPrefs.HasKey("volume")) 
        {
            slider.value = 1; 
            PlayerPrefs.SetFloat("volume", slider.value);
            PlayerPrefs.Save(); 
        }
        else slider.value = PlayerPrefs.GetFloat("volume");
        oldVolume = slider.value;
        
    }
    private void Update(){
        if(oldVolume != slider.value){
            PlayerPrefs.SetFloat("volume", slider.value);
            PlayerPrefs.Save();
            oldVolume = slider.value;
        }

        if(slider.value == 1) volumeImage.sprite = MaxVolume;
        else if(slider.value == 0) volumeImage.sprite = NoVolume;
        else volumeImage.sprite = MidVolume;
    }
}
