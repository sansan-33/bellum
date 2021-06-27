using TMPro;
using UnityEngine;

/// <summary>
/// License: You are free to modify this file for your personal or commercial 
/// use. You may not sell or re-sell these scripts, or their derivatives, in 
/// any form other than their implementation as a system into your Unity project 
/// game/application.
/// </summary>

public class FontManager : MonoBehaviour
{
    public TMP_FontAsset defaultFontJp;    // centralized font asset for JP lang
    public TMP_FontAsset defaultFontCn;    // for Simplified Chinese
    public TMP_FontAsset defaultFontHk;    // for Traditional Chinese
    public TMP_FontAsset defaultFontEn;    // for all other latin/germanic langs

    // simple singleton declaration
    private static FontManager _instance;
    public static FontManager Instance
    {
        get
        {
            // must get default font from FontManager since default font set in MainMenuDisplay
            // StaticClass is a temporary place to store the default font
            if (_instance == null)
            {

                FontManager tmpObject = GameObject.FindObjectOfType<FontManager>();
                //Debug.Log($"FontManager Createion:{tmpObject}");         

                if (tmpObject != null)
                {
                    _instance = tmpObject;
                    StaticClass.defaultFontJp = tmpObject.defaultFontJp;
                    StaticClass.defaultFontCn = tmpObject.defaultFontCn;
                    StaticClass.defaultFontHk = tmpObject.defaultFontHk;
                    StaticClass.defaultFontEn = tmpObject.defaultFontEn;
                    //Debug.Log($"FontManager Createion Font:{tmpObject.defaultFontJp}, {tmpObject.defaultFontCn}, {tmpObject.defaultFontHk}, {tmpObject.defaultFontEn}");
                }
                else {
                    GameObject coreGameObject = new GameObject(typeof(FontManager).Name);
                    _instance = coreGameObject.AddComponent<FontManager>();
                }
            }

            if (_instance.defaultFontJp== null)
            {
                _instance.defaultFontJp = StaticClass.defaultFontJp;

            }
            if (_instance.defaultFontCn == null)
            {
                _instance.defaultFontCn = StaticClass.defaultFontCn;

            }
            if (_instance.defaultFontHk == null)
            {
                _instance.defaultFontHk = StaticClass.defaultFontHk;

            }
            if (_instance.defaultFontEn == null)
            {
                _instance.defaultFontEn = StaticClass.defaultFontEn;

            }
            //Debug.Log($"FontManager return:{_instance}");
            //Debug.Log($"FontManager return Font:{_instance.defaultFontJp}, {_instance.defaultFontCn}, {_instance.defaultFontHk}, {_instance.defaultFontEn}");
            return _instance;
        }
    }

    /* Change language handled by LanguageSelectionManager.
     * 
    // language change event definition
    public delegate void LanguageMgrHandler();
    public static event LanguageMgrHandler LanguageChanged;

    // call this method to properly fire the lang changed event
    private static void LanguageChangeHasOccurred()
    {
        if (LanguageChanged != null) LanguageChanged();
    }

    // specific to the M2H Localization Package -- pass standard
    // language codes.
    public void SetLanguage(string lang)
    {
        Language.SwitchLanguage(lang);

        // inform systems and components, the language has been changed.
        LanguageChangeHasOccurred();
    }*/
}
