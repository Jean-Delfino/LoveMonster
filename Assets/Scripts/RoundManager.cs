using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int _currentRoundCountdown = 0;

    private void Start()
    {
        
    }

    private IEnumerator ProcessRounds()
    {
        while (enabled)
        {
            yield return null;
            _currentRoundCountdown++;
            
            var monsterAmount = UtilMath.GetFibonacciRecursively(_currentRoundCountdown);
            var monsterRenderers = new Renderer[monsterAmount];
            
            RecursivelySpawnAllCreatures(monsterRenderers, monsterAmount);
            yield return new WaitUntil(AllCreaturesAreNotVisible);
        }
    }

    private void RecursivelySpawnAllCreatures(Renderer[] monster, int n)
    {
        if(n < 0) return;
        
        //Instantiate with pooling
        //Add to monster array
        RecursivelySpawnAllCreatures(monster, n - 1);
    }

    private bool AllCreaturesAreNotVisible()
    {
        return true;
    }
}