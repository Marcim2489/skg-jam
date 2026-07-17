using TMPro;
using UnityEngine;

public class TimeLeftDisplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI displayText;

    void Start()
    {
        if(LevelManager.Instance!= null)
        {
            displayText.text = Mathf.CeilToInt(LevelManager.Instance.TimeLeft).ToString();
        }
        
    }

    void Update()
    {
        if(LevelManager.Instance!= null)
        {
            displayText.text = Mathf.CeilToInt(LevelManager.Instance.TimeLeft).ToString();
        }
    }
}
