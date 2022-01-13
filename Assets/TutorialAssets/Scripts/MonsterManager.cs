using Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using TutorialAssets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;


public class MonsterManager : MonoBehaviour
{
    [SerializeField] private int amountOfMonsters=10;
    [SerializeField] Transform monsterSpawnPoint;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform queuePoint;
    [SerializeField] private GameObject[]  monsterPrefabs;
    [SerializeField] private float waveDifficulty = 0; //determines the difficulty

    public List<GameObject> monsters;

    // Start is called before the first frame update
    void Awake()
    {
        //intantiating monsters
        for (int i=0; i< amountOfMonsters; i++)
        {
            InstantiateMonsters();
        }

        MonsterAttack(0);
        MoveNextMonsterToQueue();

        CalculateDifficulty(ref waveDifficulty);

        Vector3? pos = GetMonsterPosition(typeof(AddSubtractMonster));
        if(pos.HasValue)
        {
            Debug.Log($"Found monster type  at {pos}");
        }
        else
        {
            Debug.Log($"Didn't find monster.");
        }
    }

    private void InstantiateMonsters()
    {
        int monsterIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monster = Instantiate(monsterPrefabs[monsterIndex], monsterSpawnPoint.position, monsterSpawnPoint.rotation);
        monsters.Add(monster);
    }

    public void MonsterAttack(int monsterIndex)
    {
        if (monsters.Count <= monsterIndex) return;

        Transform monster = monsters[monsterIndex].transform;
        monster.GetComponent<MonsterController>().ChangeState(MonsterState.Attack);
        monster.position = attackPoint.position;
        monster.rotation = attackPoint.rotation;
    }

    public void MoveMonsterToQueue(int monsterIndex)
    {
        if (monsters.Count <= monsterIndex) return;

        Transform monster = monsters[monsterIndex].transform;
        monster.GetComponent<MonsterController>().ChangeState(MonsterState.Queue);
        monster.position = queuePoint.position;
        monster.rotation = queuePoint.rotation;
    }

    public void MoveNextMonsterToQueue()
    {
        MoveMonsterToQueue(1);
    }

    //kills the monster that has been answered from the queue as well as from the list of monsters too.
    public void KillMonster(int monsterIndex)
    {
        Destroy(monsters[monsterIndex]);
        monsters.RemoveAt(monsterIndex);
    }

    //ref can be useful if dealing with large collections while copying them into memory. ref allows to reference the memory and not copy.
    void CalculateDifficulty(ref float difficulty)
    {
        foreach (GameObject monster in monsters)
        {
            difficulty += monster.GetComponent<Points>().points;
        }
        difficulty /= (amountOfMonsters * 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3? GetMonsterPosition(Type type)
    {
        foreach(GameObject monster in monsters)
        {
            if(monster.GetComponent<MonsterController>().GetType() == type)
            {
                return monster.transform.position;
            }
        }
        return null;
    }
}
