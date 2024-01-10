using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}



[Serializable]

public class StageEvent
{
    public StageEventType eventType;

    public float time;
    public string message;

    public GameObject objectToSpawn;
    public EnemyData enemyToSpawn;
    public int count;

    public bool isRepeatedEvent;
    public float repeatEverySeconds;
    public int repeatCount;
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
    public string stageID;
    public List<string> stageCompletionToUnlock;
}
