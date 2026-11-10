using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [Header("Match Settings")]
    public float rewardMoney = 50f;

    private bool matchEnded = false;

    public void OnCharacterDied(bool isPlayer)
    {
        if (matchEnded) return;

        if (isPlayer)
        {
            // Player died -> Defeat
            EndMatch(playerWon: false);
        }
        else
        {
            // Enemy died -> Victory
            EndMatch(playerWon: true);
        }
    }

    private void EndMatch(bool playerWon)
    {
        matchEnded = true;

        if (playerWon)
        {
            GameData.Money += rewardMoney;
            Debug.Log("You Won! Money earned: " + rewardMoney + " | Total Money: " + GameData.Money);
        }
        else
        {
            Debug.Log("You Lost! No money earned. | Total Money: " + GameData.Money);
        }

        GameData.CurrentDay++;
        Debug.Log("A day passed. Current Day: " + GameData.CurrentDay);

        Invoke(nameof(ReturnToApartment), 2.0f);
    }

    private void ReturnToApartment()
    {
        string targetScene = GameData.GetCurrentApartmentSceneName();
        SceneManager.LoadScene(targetScene);
    }
}