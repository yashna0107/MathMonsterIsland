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

        try
        {
            for (int i = 0; i<amountOfMonsters; i++)
            {
                InstantiateMonsters();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't spawn monsters. Check {gameObject.name} for missing prefabs in the monster prefabs array " +
                $"OR check spawn point assigned properly.  { e.Message}");
        }
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

        StartCoroutine(LerpToPosition(monster, attackPoint.position,attackPoint.rotation, 0.5f));
    }

    public void MoveMonsterToQueue(int monsterIndex)
    {
        if (monsters.Count <= monsterIndex) return;

        Transform monster = monsters[monsterIndex].transform;
        monster.GetComponent<MonsterController>().ChangeState(MonsterState.Queue);

        StartCoroutine(LerpToPosition(monster, queuePoint.position, queuePoint.rotation, 0.3f));
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

    IEnumerator LerpToPosition(Transform objectTransform, Vector3 position, Quaternion rotation, float speed)
    {
        float distToPos = Vector3.Distance(objectTransform.position, position);
        float timer = 0;
        while(distToPos > 0.1f)
        {
            distToPos = Vector3.Distance(objectTransform.position, position);

            objectTransform.position = Vector3.Lerp(objectTransform.position, position, timer*speed);
            objectTransform.rotation = rotation;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
