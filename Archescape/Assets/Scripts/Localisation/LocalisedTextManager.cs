using UnityEngine;

public static class LocalisedTextManager {

	public static string GetLocalised(string text)
    {
        switch(PlayerPrefs.GetString(SettingValues.language))
        {
            case "CRO":
                return CROLocalisation.local[text];
            case "GER":
                return GERLocalisation.local[text];
            case "JPN":
                return JPNLocalisation.local[text];
            case "RUS":
                return RUSLocalisation.local[text];
            default:
                return ENGLocalisation.local[text];
        }
    }
}
