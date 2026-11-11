using UnityEngine;

public static class GameData
{
    private const string LastApartmentScene = "CurrentApartmentScene";

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
        if (PlayerPrefs.HasKey(LastApartmentScene))
        {
            return PlayerPrefs.GetString(LastApartmentScene);
        }

        if (ApartmentLevel <= 0)
        {
            return "Appartment";
        }

        return "Appartment " + ApartmentLevel;
    }

    public static void SetCurrentApartmentSceneName(string sceneName)
    {
        PlayerPrefs.SetString(LastApartmentScene, sceneName);
        PlayerPrefs.Save();
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey("PlayerMoney");
        PlayerPrefs.DeleteKey("CurrentDay");
        PlayerPrefs.DeleteKey("ApartmentLevel");
        PlayerPrefs.DeleteKey(LastApartmentScene);
        PlayerPrefs.Save();
    }
}
