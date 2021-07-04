using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class LanguageSelectionManager : MonoBehaviour
{
    public static string STRING_TEXT_REF = "UI_Text";
    public const int LOCALE_EN = 0;
    public const int LOCALE_JP = 1;
    public const int LOCALE_CN = 2;
    public const int LOCALE_HK = 3;
    public static int Selected_Locale_Index = 0;

    [SerializeField] public GameObject languageSelectionPopup;
    [SerializeField] public Locale defaultLocale = null; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        int langageId = PlayerPrefs.GetInt("Language");
        Debug.Log($"LanguageSelectionManager.start() langageId:{langageId}");
        OnSelectionChanged(langageId);
    }

    public void SelectEnglish()
    {
        OnSelectionChanged(LOCALE_EN);
    }

    public void SelectJapanese()
    {
        OnSelectionChanged(LOCALE_JP);
    }

    public void SelectSimplifiedChinese()
    {
        OnSelectionChanged(LOCALE_CN);
    }

    public void SelectTraditionalChinese()
    {
        OnSelectionChanged(LOCALE_HK);
    }

    void OnSelectionChanged(int index)
    {
        Debug.Log($"LanguageSelectionManager.OnSelectionChanged() Selected index:{index}");
        Selected_Locale_Index = index;

        if (index >= LocalizationSettings.AvailableLocales.Locales.Count)
        {
            Debug.LogError($"LocalizationSettings.AvailableLocales.Locales.Count:{LocalizationSettings.AvailableLocales.Locales.Count} < index:{index}");
            return;
        }

        var locale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = locale;
        Debug.Log($"LanguageSelectionManager.OnSelectionChanged() SelectedLocale:{LocalizationSettings.SelectedLocale}");

        // Instantiate FontManger to get Default Font
        TMP_Asset tempFont = FontManager.Instance.defaultFontEn;

        // save
        PlayerPrefs.SetInt("Language", index);
        Debug.Log($"LanguageSelectionManager.OnSelectionChanged() PlayerPrefs.SetInt(Language, index):{index}");

        // TODO: inactive check for non-selected locale and active check for selected locale

        // TODO: update language in Setting popup
    }

    
}
