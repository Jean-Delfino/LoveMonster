using TMPro;
using UnityEngine;

public class RoundControlUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundCounter;
    [SerializeField] private TextMeshProUGUI monsterCounter;
    [SerializeField] private TextMeshProUGUI remainingMonsterCounter;
    [SerializeField] private TextMeshProUGUI roundTimeCounts;
    
    public void Setup(int rounds, int monsterAmount)
    {
        monsterCounter.text = $"{monsterAmount}";
        roundCounter.text = $"{rounds}";
    }

    public void SetRemainingMonster(int remainingMonsters)
    {
        remainingMonsterCounter.text = $"{remainingMonsters}";
    }

    public void UpdateTimer(float totalTime)
    {
        roundTimeCounts.text = $"{totalTime:F2}";
    }
}