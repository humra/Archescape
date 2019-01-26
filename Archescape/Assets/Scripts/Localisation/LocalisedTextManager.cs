using UnityEngine;

public static class LocalisedTextManager {

	public static string GetLocalised(string text)
    {
        switch(PlayerPrefs.GetString(SettingValues.language))
        {
            case "CRO":
                return CROLocalisation.local[text];
            default:
                return ENGLocalisation.local[text];
        }
    }
}
