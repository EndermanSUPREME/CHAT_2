using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TrueEndingEvent : MonoBehaviour
{
    [SerializeField] AudioSource doorKnockingSound, TV_Noise;
    [SerializeField] GameObject TV_MessageObject, GameArea, CutsceneObject, PlayerClock;
    [SerializeField] Animator FadeOutAnim;
    [SerializeField] VideoPlayer ClipPlayer;
    [SerializeField] VideoClip Forced, Reg;
    bool openedTheDoor = false;
    [SerializeField] OpenDoorScript playerInteraction;
    [SerializeField] GameClock clockScript;

    void Start()
    {
        TV_MessageObject.SetActive(false);

        // GameArea.SetActive(true);
        CutsceneObject.SetActive(false);
    }

    public void StartCountDown()
    {
        doorKnockingSound.Play();
        TV_MessageObject.SetActive(true);
        TV_Noise.Play();

        Invoke("GoToEndingCutscene", 20);
        playerInteraction.FinalEndingReached();
        clockScript.ReachedTheTrueEnd();
        PlayerClock.SetActive(false);
    }

    public void TransitionToEnd() // interact with door
    {
        playerInteraction.InteractionPeriodEnded();
        openedTheDoor = true;

        FadeOutAnim.Play("FadeIn");
        Invoke("ListenedEnding", 1.25f);
    }

    void GoToEndingCutscene()
    {
        playerInteraction.InteractionPeriodEnded();

        if (!openedTheDoor)
        {
            FadeOutAnim.Play("FadeIn");
            Invoke("ForcedEntryEnding", 1.25f);
        }
    }

    void ForcedEntryEnding()
    {
        // cutscene plays
        // ClipPlayer.clip = Forced;
        ClipPlayer.url = "https://endermansupreme.github.io/my_WEBGL_Cutscenes/TrueEnding_ForcedEntry.mp4";

        TV_Noise.Stop();
        GameArea.SetActive(false);
        CutsceneObject.SetActive(true);
    }

    void ListenedEnding()
    {
        // ClipPlayer.clip = Reg;
        ClipPlayer.url = "https://endermansupreme.github.io/my_WEBGL_Cutscenes/TrueEnding_OpenedDoor.mp4";

        TV_Noise.Stop();
        GameArea.SetActive(false);
        CutsceneObject.SetActive(true);
    }
}//EndScript