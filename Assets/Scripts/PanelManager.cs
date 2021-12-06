using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject Window;

    public void Enable()
    {
        Window.SetActive(true);  
    }
    public void Disable()
    {
        Window.SetActive(false);
    }
}
