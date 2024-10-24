using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalLink : MonoBehaviour
{

    public void openFeedbackForm(){
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfTg6w97nob8buJI2wiRYH67Sd2LhN16ojKnEP35cFtWXq4fg/viewform?usp=pp_url");
    } 
}
