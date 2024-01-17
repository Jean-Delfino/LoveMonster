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
        public float MoveSpeed;
        
        public MonsterControl(GameObject monster, float timeLeft)
        {
            Monster = monster.GetComponent<Renderer>();
            RemainingTimeToBeDestroyed = timeLeft;
        }
    }

    private readonly List<MonsterControl> _monsterControls = new();

    [SerializeField] private float timeUntilMonsterDestroyed = 1f;

    [SerializeField] private GameObject monsterPrefab;
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
            
            monsterControl.SetToBeDestroyed = !monsterControl.Monster.isVisible;
            monsterControl.Monster.transform.Translate(Vector3.right * monsterControl.MoveSpeed * Time.deltaTime);

        }
    }

    public void AddMonster()
    {
        //Instantiate with pooling
        var go = Spawner.Spawn(monsterPrefab);
        
        if(go == null) return;
        
        //Add to monster list
        _monsterControls.Add(new MonsterControl(go, timeUntilMonsterDestroyed));
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
        return _monsterControls.Count == 0;
    }
}