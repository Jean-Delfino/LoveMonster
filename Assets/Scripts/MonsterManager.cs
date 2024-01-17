using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private class MonsterControl
    {
        public Renderer Monster;
        public bool SetToBeDestroyed;
        public float RemainingTimeToBeDestroyed;
    }

    private readonly List<MonsterControl> _monsterControls = new();

    [SerializeField] private float timeUntilMonsterDestroyed = 1f;
    public void Update()
    {
        var time = Time.deltaTime;
        foreach (var monsterControl in _monsterControls)
        {
            if (monsterControl.SetToBeDestroyed)
            {
                CheckDestruction(monsterControl, time);
                return;
            }
            
            monsterControl.SetToBeDestroyed = !monsterControl.Monster.isVisible;
        }
    }

    public void AddMonster()
    {
        //Instantiate with pooling
        //Add to monster array
    }

    private void CheckDestruction(MonsterControl monsterControl, float time)
    {
        if (monsterControl.RemainingTimeToBeDestroyed < 0)
        {
            //Destroy by pooling it
            return;
        }

        monsterControl.RemainingTimeToBeDestroyed -= time;
    }
    
}