using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Language  {

    private static Language currentLanguage;

    public static Language Get()
    {
        if(currentLanguage == null)
        {
            switch(Application.systemLanguage)
            {
                case SystemLanguage.SerboCroatian:
                    currentLanguage = new LanguageENG();
                    break;
                default:
                    currentLanguage = new LanguageENG();
                    break;
            }
        }

        return currentLanguage;
    }

    public abstract string TestString();
}
