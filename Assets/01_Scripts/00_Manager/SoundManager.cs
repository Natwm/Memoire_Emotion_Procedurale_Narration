using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Space]
    [Header("UI Sound Negociation")]
    FMOD.Studio.EventInstance uiSelectedPlayerEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedPlayerSound;

    FMOD.Studio.EventInstance uiSelectedObjectEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedObjectSound;

    FMOD.Studio.EventInstance uiSelectedNegociationEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedNegociationSound;

    FMOD.Studio.EventInstance uiSelectedCantUseNegociationEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedCantUseNegociationSound;

    FMOD.Studio.EventInstance uiSelectedRepartitionEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedRepartitionSound;

    FMOD.Studio.EventInstance uiSelectedStartAdventureEffect;
    [FMODUnity.EventRef] [SerializeField] private string uiSelectedStartAdventureSound;

    [Space]
    [Header("UI Sound Vignette")]
    FMOD.Studio.EventInstance drawVignetteEffect;
    [FMODUnity.EventRef] [SerializeField] private string drawVignetteSound;

    [Space]
    [Header("UI Sound Resolution")]
    FMOD.Studio.EventInstance resolutionAvailableEffect;
    [FMODUnity.EventRef] [SerializeField] private string resolutionAvailableSound;

    [Space]
    [Header("Sound Fmod Resolution")]
    FMOD.Studio.EventInstance gainObjectEffect;
    [FMODUnity.EventRef] [SerializeField] private string gainObjectSound;

    FMOD.Studio.EventInstance endResolutionEffect;
    [FMODUnity.EventRef] [SerializeField] private string endResolutionSound;

    FMOD.Studio.EventInstance nextPersonnageEffect;
    [FMODUnity.EventRef] [SerializeField] private string nextPersonnageSound;

    FMOD.Studio.EventInstance endExpeditionEffect;
    [FMODUnity.EventRef] [SerializeField] private string endExpeditionSound;


    public static SoundManager instance;
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : SoundManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        print(this.gameObject);
        SetUpSound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaySound_SelectedPlayer();
        }
    }

    void SetUpSound()
    {
        uiSelectedPlayerEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedPlayerSound);
        uiSelectedObjectEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedObjectSound);
        uiSelectedNegociationEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedNegociationSound);
        uiSelectedCantUseNegociationEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedCantUseNegociationSound);
        uiSelectedRepartitionEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedRepartitionSound);
        uiSelectedStartAdventureEffect = FMODUnity.RuntimeManager.CreateInstance(uiSelectedStartAdventureSound);

        drawVignetteEffect = FMODUnity.RuntimeManager.CreateInstance(drawVignetteSound);
        resolutionAvailableEffect = FMODUnity.RuntimeManager.CreateInstance(resolutionAvailableSound);

        gainObjectEffect = FMODUnity.RuntimeManager.CreateInstance(gainObjectSound);
        endResolutionEffect = FMODUnity.RuntimeManager.CreateInstance(endResolutionSound);
        nextPersonnageEffect = FMODUnity.RuntimeManager.CreateInstance(nextPersonnageSound);
        endExpeditionEffect = FMODUnity.RuntimeManager.CreateInstance(endExpeditionSound);
    }


    //Negociation
    void PlaySound_SelectedPlayer(){uiSelectedPlayerEffect.start();}
    void PlaySound_SelectedObject() { uiSelectedObjectEffect.start(); }
    void PlaySound_SelectedNegociation() { uiSelectedNegociationEffect.start(); }
    void PlaySound_CantUseNegociation() { uiSelectedCantUseNegociationEffect.start(); }
    void PlaySound_Repartition() { uiSelectedRepartitionEffect.start(); }
    void PlaySound_StartAdventure() { uiSelectedStartAdventureEffect.start(); }

    //Vignette
    void PlaySound_DrawVignette() { drawVignetteEffect.start(); }
    void PlaySound_ResolutionAvailable() { resolutionAvailableEffect.start(); }


    //Resolution
    void PlaySound_GainObject() { gainObjectEffect.start(); }
    void PlaySound_EndResolution() { endResolutionEffect.start(); }
    void PlaySound_NextPersonnage() { nextPersonnageEffect.start(); }
    void PlaySound_EndExpedition() { endExpeditionEffect.start(); }

}
