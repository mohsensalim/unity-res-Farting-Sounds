using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetSoundPage : MonoBehaviour
{
    //public string ObjectName;

    public GameObject gg;
    private Button SetBut;


    private void Awake()
    {
        SetBut = GetComponent<Button>();
        SetBut.onClick.AddListener(Getpage);
        
        
        
    }

    private void Start()
    {
        AdsManager.instance.ShowBanner();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Getpage() 
    {
       
        gg.SetActive(true);

    }

   public void ClosePage(GameObject JJ)
    {
        JJ.SetActive(false);

    }

   
    

    



}
