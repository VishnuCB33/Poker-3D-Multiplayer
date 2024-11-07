using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LanguageChangeButton : MonoBehaviour
{
    bool isActive=false;
    public void ChangeLocale(int localeID)
    {
        if (isActive == true)
        {
            return;
          
          
           
        }
        StartCoroutine(SetLocale(localeID));
    }
   IEnumerator SetLocale(int _localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale=LocalizationSettings.AvailableLocales.Locales[_localeID];
        isActive = false;
    }
}
