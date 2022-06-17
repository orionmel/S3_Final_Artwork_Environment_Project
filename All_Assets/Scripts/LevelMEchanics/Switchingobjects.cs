using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchingobjects : MonoBehaviour
{
    public GameObject theObject;

    public ButtomController theButton;

    public bool revealWhenPressed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(theButton.isPressed){
            theObject.SetActive(revealWhenPressed);
        }else{
            theObject.SetActive(!revealWhenPressed);
        }
        
    }
}
