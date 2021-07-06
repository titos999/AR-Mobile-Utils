using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.Collections;

public class VoiceController : MonoBehaviour
{
    // Reference TTS and SST Text
    [SerializeField] private Text sttResultText;

    // How much time until we clear text
    [SerializeField] private float clearTextTimer = 2f;

    // Welcome and Goodbye texts
    [SerializeField] private string englishWelcomeStr = "Hello";
    [SerializeField] private string englishGoodbyeStr = "Goodbye";
    [SerializeField] private string greekWelcomeStr = "Γειά σου";
    [SerializeField] private string greekGoodbyeStr = "Αντίο";

    // Clearing text coroutine
    private IEnumerator clearTextCoroutine;

    void Start()
    {
        CheckPermission();
        Setup();
        SetupCallbacks();
    }

    // Setup TTS and STT
    private void Setup()
    {
        TextToSpeech.instance.Setting(GlobalManager.SELECTED_LANG_CODE, 1, 1);
        SpeechToText.instance.Setting(GlobalManager.SELECTED_LANG_CODE);
    }

    // Setup callbacks
    private void SetupCallbacks()
    {
    #if UNITY_ANDROID
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
    #endif
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;
    }

    // Check microphone permission on Android Device
    private void CheckPermission()
    {
    #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    #endif
    }

    // TTS
    public void StartWelcomeSpeaking()
    {
        if (GlobalManager.SELECTED_LANG_CODE == GlobalManager.ENGLISH_LANG_CODE)
            TextToSpeech.instance.StartSpeak(englishWelcomeStr);
        else
            TextToSpeech.instance.StartSpeak(greekWelcomeStr);

    }

    public void StartGoodbyeSpeaking()
    {
        if (GlobalManager.SELECTED_LANG_CODE == GlobalManager.ENGLISH_LANG_CODE)
            TextToSpeech.instance.StartSpeak(englishGoodbyeStr);
        else
            TextToSpeech.instance.StartSpeak(greekGoodbyeStr);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    private void OnSpeakStart()
    {
        Debug.Log("TTS started...");
    }

    private void OnSpeakStop()
    {
        Debug.Log("TTS stopped");
    }

    // STT
    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    private void OnFinalSpeechResult(string result)
    {
        sttResultText.text = result;
        ClearText();
    }

   
    private void OnPartialSpeechResult(string result)
    {
        sttResultText.text = result;
    }

    

    // Clear SST text functionality
    private void ClearText()
    {
        StopAllCoroutines();
        StartCoroutine(ClearTextRoutine());
    }


    private IEnumerator ClearTextRoutine()
    {
        yield return new WaitForSeconds(clearTextTimer);
        sttResultText.text = string.Empty;
    }

}
