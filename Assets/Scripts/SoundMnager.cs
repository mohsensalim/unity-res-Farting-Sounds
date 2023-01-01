using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundMnager : MonoBehaviour
{
    
    private AudioSource HornSource;
    private Button BTSound;
    public Slider SoundSlider;
    public Toggle TGG;
   
    

    // Start is called before the first frame update
    void Start()
    {
        BTSound = GetComponent<Button>();
        HornSource=GetComponent<AudioSource>();
        BTSound.onClick.AddListener(SoundEffect);
        
        
        SoundSlider.value = HornSource.volume;
       
    }

    // Update is called once per frame
    void Update()
    {

        HornSource.volume = SoundSlider.value;
        
    }

  public void SoundEffect()
    {
        HornSource.Play();
            
    }
    
    public void Loopi()
    {
        if (TGG.isOn == true) 
        {
            HornSource.loop = true;
        
        } 
        else
        {
            HornSource.loop = false;
        }
    }


}
