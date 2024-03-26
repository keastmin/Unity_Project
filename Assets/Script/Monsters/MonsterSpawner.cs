using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform monsters;
    [SerializeField] Transform monsterSpawnPlace;
    List<Transform> monsterList;

    private void Awake()
    {
        SetMonsterList();
        Spawn();
    }

    // 몬스터 리스트 생성
    void SetMonsterList()
    {
        monsterList = new List<Transform>();

        foreach (Transform child in monsters)
        {
            monsterList.Add(child);
        }
    }

    // 몬스터 스폰
    void Spawn()
    {
        int randomMonsterNum = Random.Range(0, monsterList.Count);

        GameObject monster = Instantiate(monsterList[randomMonsterNum].gameObject);
        monster.transform.position = monsterSpawnPlace.transform.position;
    }
}
