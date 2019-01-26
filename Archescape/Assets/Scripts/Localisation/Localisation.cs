using UnityEngine;
using UnityEngine.UI;

public class Localisation : MonoBehaviour {

    private Text text;
    private string defaultText;

    private void Start()
    {
        text = GetComponent<Text>();

        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }

        if(text == null)
        {
            text.text = "MISSING!!!";
        }

        defaultText = text.text;

        FindObjectOfType<SettingsUI>().onLanguageChangedCallback += Localise;
        Localise();
    }

    public void Localise()
    {
        text.text = LocalisedTextManager.GetLocalised(defaultText);
    }
}
