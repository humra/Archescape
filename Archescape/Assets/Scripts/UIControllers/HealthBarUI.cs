using UnityEngine;

public class HealthBarUI : MonoBehaviour {

    [SerializeField]
    private Transform healthBar;

    public void SetHealthBarValue(float newValue)
    {
        healthBar.localScale = new Vector3(newValue, 1f, 1f);
    }
}
