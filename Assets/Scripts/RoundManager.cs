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
            _currentRoundCountdown++; //This makes it start in the Fibonacci = 1
            
            var monsterAmount = UtilMath.GetFibonacciRecursively(_currentRoundCountdown);
            var rotation = Quaternion.Euler(new Vector3(0, 0, MonsterManager.Instance.GetRandomZRotation()));
            
            RecursivelySpawnAllCreatures(rotation, monsterAmount);
            yield return new WaitUntil(AllCreaturesAreNotVisible);
        }
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