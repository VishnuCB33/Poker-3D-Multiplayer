using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChangeButton : MonoBehaviour
{
    public bool isActive = false;
    public TMP_FontAsset hindFont;     
    public TMP_FontAsset arvoText;
    public TextMeshProUGUI languageText; 


   /* private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape)) {
            ChangeFont();
        }
    }*/

    public void ChangeFont()
    {
        languageText.font = hindFont;
    }
    public void ChangeLocale(int localeID)
    {
        if (isActive)
        {
            return;
        }

       
        if (localeID == 2)
        {
            languageText.font = hindFont;
            Debug.Log("Hindi font applied.");
        }
        else
        {
            languageText.font = arvoText;
            Debug.Log("Default font applied.");
        }

        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        isActive = false;
    }
}
