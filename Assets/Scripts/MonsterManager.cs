﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private class MonsterControl
    {
        public Renderer Monster;
        public bool SetToBeDestroyed;
        public float RemainingTimeToBeDestroyed;
        public float MoveSpeed;
        
        public MonsterControl(GameObject monster, float speed, float timeLeft)
        {
            Monster = monster.GetComponent<Renderer>();
            MoveSpeed = speed;
            RemainingTimeToBeDestroyed = timeLeft;
        }
    }

    private readonly List<MonsterControl> _monsterControls = new();
    private int _aliveMonsters = 0;

    [Space] [Header("MONSTER")] [Space]
    [SerializeField] private GameObject monsterPrefab;

    [Space] [Header("MONSTER SPAWN VARIABLE")] [Space]
    [SerializeField] private float timeUntilMonsterDestroyed = 1f;
    
    [SerializeField] private float minSpeed = 0.3f;
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float minZRotation = 0f;
    [SerializeField] private float maxZRotation = 360f;
 
    public void Update()
    {
        var time = Time.deltaTime;
        for (int i = 0; i < _monsterControls.Count; i++)
        {
            var monsterControl = _monsterControls[i];
            
            if (monsterControl.SetToBeDestroyed)
            {
                if (!CheckDestruction(monsterControl, time)) continue;
                
                RemoveMonster(i);
                i--;
                
                continue;
            }
            
            //This may not work in editor if you keep the scene camera in the monster
            if (!monsterControl.Monster.isVisible)
            {
                monsterControl.SetToBeDestroyed = true;
                _aliveMonsters--;
                continue;
            }
            
            monsterControl.Monster.transform.Translate(Vector3.right * (monsterControl.MoveSpeed * Time.deltaTime));
        }
    }

    public void AddMonster(Quaternion rotation)
    {
        //Instantiate with pooling
        var go = Spawner.Spawn(monsterPrefab,Vector3.zero, rotation, transform);

        //Add to monster list
        _monsterControls.Add(new MonsterControl(go, GetRandomSpeed(),timeUntilMonsterDestroyed));
        _aliveMonsters++;
    }

    private bool CheckDestruction(MonsterControl monsterControl, float time)
    {
        if (monsterControl.RemainingTimeToBeDestroyed < 0)
        {
            return true;
        }

        monsterControl.RemainingTimeToBeDestroyed -= time;
        return false;
    }

    private void RemoveMonster(int index)
    {
        Spawner.DeSpawn(_monsterControls[index].Monster.gameObject);
        _monsterControls.RemoveAt(index);
    }

    public bool HasAllMonsterBeenDestroyed()
    {
        return _aliveMonsters == 0;
    }

    public int AliveMonsters()
    {
        return _aliveMonsters;
    }
    
    public float GetRandomZRotation()
    {
        return UtilRandom.GetRandomFloatInRange(minZRotation, maxZRotation);
    }

    public float GetRandomSpeed()
    {
        return UtilRandom.GetRandomFloatInRange(minSpeed, maxSpeed);
    }
}