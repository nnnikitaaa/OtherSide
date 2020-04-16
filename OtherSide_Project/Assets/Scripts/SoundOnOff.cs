using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundOnOff : MonoBehaviour
{
    Button button;
    public Color on, off;
    private void Start()
    {
        button = GetComponent<Button>();
        if (SoudManager.mute)
        {
            button.targetGraphic.color = off;
        }
    }
    public void Change()
    {
        SoudManager.mute = !SoudManager.mute;
        if (button.targetGraphic.color == on)
        {
            button.targetGraphic.color = off;
        }
        else
        {
            button.targetGraphic.color = on;
        }
    }
}
