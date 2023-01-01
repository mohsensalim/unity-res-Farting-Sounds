using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdsContoller : MonoBehaviour
{
    Button But;

    private void Start()
    {
        But = GetComponent<Button>();
        But.onClick.AddListener(ShowIt);
    }



    public void ShowIt() 
    {
        AdsManager.instance.ShowInterstitial();
    
    }

}
