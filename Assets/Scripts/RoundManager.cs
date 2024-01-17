using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int _currentRoundCountdown = 0;

    private void Start()
    {
        StartCoroutine(ProcessRounds());
    }

    private IEnumerator ProcessRounds()
    {
        while (enabled)
        {
            yield return null;
            _currentRoundCountdown++;
            
            var monsterAmount = UtilMath.GetFibonacciRecursively(_currentRoundCountdown);

            RecursivelySpawnAllCreatures(monsterAmount);
            yield return new WaitUntil(AllCreaturesAreNotVisible);
        }
    }

    private void RecursivelySpawnAllCreatures(int n)
    {
        if(n < 0) return;

        MonsterManager.Instance.AddMonster();
        RecursivelySpawnAllCreatures(n - 1);
    }

    private bool AllCreaturesAreNotVisible()
    {
        return MonsterManager.Instance.HasAllMonsterBeenDestroyed();
    }
}