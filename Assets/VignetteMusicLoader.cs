﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VignetteMusicLoader : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SDName = "event:/SD/Vignette/Vignette_";
    [FMODUnity.EventRef]
    public string MusicName = "event:/Music";

    [FMODUnity.EventRef]
    FMOD.Studio.EventDescription OutEvent;
    public StudioEventEmitter SD_Event;
    public StudioEventEmitter Music_Event;

    public string LoadSDString(string vignetteCategory)
    {
        string loadSD = SDName + "Vignette_" + vignetteCategory;
        return loadSD;
    }

    public string LoadMusicString(string vignetteCategory)
    {
        string loadmusic = MusicName + "" + vignetteCategory;
        return loadmusic;
    }

    public void SetEvent(string vignetteCategory)
    {
        //print();
        RuntimeManager.StudioSystem.getEvent(LoadSDString(vignetteCategory),out OutEvent);
        if (OutEvent.isValid())
        {
            FMODUnity.RuntimeManager.CreateInstance(LoadSDString(vignetteCategory)).start();
        }
        
        //SD_Event.Event = LoadSDString(vignetteCategory);
        //Music_Event.Event = LoadMusicString(vignetteCategory);
    }

    public void PlayEvents()
    {
        if (OutEvent.isValid())
        {
            SD_Event.Play();
        }
        Music_Event.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
