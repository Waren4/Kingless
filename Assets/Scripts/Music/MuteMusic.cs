using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public Image icon;
    public Sprite mutedIcon;
    public Sprite unmutedIcon;

    private bool muted = false;

    private void Start() {
        if(AudioListener.volume == 0)
        {
            muted = true;
            icon.sprite = mutedIcon;
        }
    }
    public void ToggleMute() {
        if(muted) {
            //unmute
            muted = false;
            icon.sprite = unmutedIcon;
            AudioListener.volume = 1;
        }
        else {
            //mute
            muted = true;
            icon.sprite = mutedIcon;
            AudioListener.volume = 0;
        }
    }
}
