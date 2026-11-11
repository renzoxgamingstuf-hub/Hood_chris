using UnityEngine;
using TMPro;

public class ApartmentUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + GameData.Money.ToString("F0");
        }

        if (dayText != null)
        {
            dayText.text = "Day " + GameData.CurrentDay;
        }
    }
}
