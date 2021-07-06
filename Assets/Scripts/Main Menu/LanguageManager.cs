using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    // Exposed text fields for inspector
    [SerializeField] private Text selectedLanguageText;
    [SerializeField] private Text mainText;
    [SerializeField] private Text changeLanguageText;
    [SerializeField] private Text mapText;
    [SerializeField] private Text exitText;

    // By default english is selected
    private bool englishIsSelected = true;

    private void Awake()
    {
        if (GlobalManager.SELECTED_LANG_CODE == GlobalManager.ENGLISH_LANG_CODE)
        {
            englishIsSelected = true;
            ChangeUIToEnglish();
        }

        else
        {
            englishIsSelected = false;
            ChangeUIToGreek();
        }
    }

    public void ChangeLanguage()
    {
        englishIsSelected = !englishIsSelected;

        if (!englishIsSelected)
        {
            ChangeUIToGreek();
            GlobalManager.SelectGreekLanguage();
        }

        else
        {
            ChangeUIToEnglish();
            GlobalManager.SelectEnglishLanguage();
        }
    }

    private void ChangeUIToGreek()
    {
        selectedLanguageText.text = "Επιλεγμένη Γλώσσα: GR";
        mainText.text = "Κύρια Σκηνή";
        changeLanguageText.text = "Αλλαγή Γλώσσας";
        mapText.text = "Χάρτης";
        exitText.text = "Έξοδος";
    }

    private void ChangeUIToEnglish()
    {
        selectedLanguageText.text = "Selected Language: EN";
        mainText.text = "Main Scene";
        changeLanguageText.text = "Change Language";
        mapText.text = "Map";
        exitText.text = "Exit";
    }
}
