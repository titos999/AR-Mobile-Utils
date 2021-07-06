using UnityEngine;

public class GlobalManager : MonoBehaviour
{

    public static string ENGLISH_LANG_CODE = "en-US";
    public static string GREEK_LANG_CODE = "el-GR";
    public static string SELECTED_LANG_CODE = "en-US";

    public static void SelectGreekLanguage()
    {
        SELECTED_LANG_CODE = GREEK_LANG_CODE;
    }

    public static void SelectEnglishLanguage()
    {
        SELECTED_LANG_CODE = ENGLISH_LANG_CODE;
    }
}
