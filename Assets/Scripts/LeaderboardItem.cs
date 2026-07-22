using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI nameText;
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]Image characterImage;
    [SerializeField]Sprite[] charSprites;

    public void UpdateDisplay(string username, string score, int charId)
    {
        nameText.text = username;
        scoreText.text = score;
        characterImage.sprite = charSprites[charId];
    }
}
