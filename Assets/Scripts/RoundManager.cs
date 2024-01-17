using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private RoundControlUI roundUI;

    [SerializeField] private float waitTimeAfterRoundOver = 0.5f;
    private int _currentRoundCountdown = 0;
    private float _currentRoundElapsedTimer = 0;
    private void Start()
    {
        StartCoroutine(ProcessRounds());
    }

    private IEnumerator ProcessRounds()
    {
        while (enabled)
        {
            yield return null;
            _currentRoundCountdown++; //This makes it start in the Fibonacci = 1
            
            var monsterAmount = UtilMath.GetFibonacciRecursively(_currentRoundCountdown);
            var rotation = Quaternion.Euler(new Vector3(0, 0, MonsterManager.Instance.GetRandomZRotation()));
            
            RecursivelySpawnAllCreatures(rotation, monsterAmount);
            RoundStartUIUpdate(monsterAmount);

            var elapsedTime = 0f;

            while (!AllCreaturesAreNotVisible())
            {
                UpdateRoundUI();
                
                yield return null;
            }

            yield return new WaitForSeconds(waitTimeAfterRoundOver);
        }
    }

    private void UpdateRoundUI()
    {
        _currentRoundElapsedTimer += Time.deltaTime;
        roundUI.SetRemainingMonster(MonsterManager.Instance.AliveMonsters());
        roundUI.UpdateTimer(_currentRoundElapsedTimer);
    }
    private void RoundStartUIUpdate(int monsterAmount)
    {
        roundUI.Setup(_currentRoundCountdown, monsterAmount);  
        roundUI.UpdateTimer(0);
        roundUI.SetRemainingMonster(monsterAmount);
    }

    private void RecursivelySpawnAllCreatures(Quaternion rotation, int n)
    {
        if(n < 1) return;

        MonsterManager.Instance.AddMonster(rotation);
        RecursivelySpawnAllCreatures(rotation, n - 1);
    }

    private bool AllCreaturesAreNotVisible()
    {
        return MonsterManager.Instance.HasAllMonsterBeenDestroyed();
    }
}