using UnityEngine;

public static class GameData
{
    public static float Money
    {
        get => PlayerPrefs.GetFloat("PlayerMoney", 0f);
        set
        {
            PlayerPrefs.SetFloat("PlayerMoney", value);
            PlayerPrefs.Save();
        }
    }

    public static int CurrentDay
    {
        get => PlayerPrefs.GetInt("CurrentDay", 1);
        set
        {
            PlayerPrefs.SetInt("CurrentDay", value);
            PlayerPrefs.Save();
        }
    }

    public static int ApartmentLevel
    {
        get => PlayerPrefs.GetInt("ApartmentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("ApartmentLevel", Mathf.Clamp(value, 0, 5));
            PlayerPrefs.Save();
        }
    }
    
    public static string GetCurrentApartmentSceneName()
    {
        if (ApartmentLevel <= 0)
        {
            return "Appartment";
        }

        return "Appartment " + ApartmentLevel;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey("PlayerMoney");
        PlayerPrefs.DeleteKey("CurrentDay");
        PlayerPrefs.DeleteKey("ApartmentLevel");
        PlayerPrefs.Save();
    }
}
