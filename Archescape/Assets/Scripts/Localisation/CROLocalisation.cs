using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CROLocalisation : MonoBehaviour {

    public static Dictionary<string, string> local = new Dictionary<string, string>()
    {
        {"New Game", "Nova Igra" },
        {"Quit Application", "Zatvori Aplikaciju" },
        {"Quit Game", "Zatvori Igru" }
    };
}
