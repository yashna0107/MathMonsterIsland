using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private int amountOfMonsters=10;
    [SerializeField] Transform monsterSpawnPoint;
    [SerializeField] private GameObject[]  monsterPrefabs;
    [SerializeField] private float waveDifficulty = 0; //determines the difficulty

    public List<GameObject> monsters;

    // Start is called before the first frame update
    void Start()
    {
        //intantiating monsters
        for (int i=0; i< amountOfMonsters; i++)
        {
            int monsterIndex = Random.Range(0, monsterPrefabs.Length);
           GameObject monster = Instantiate(monsterPrefabs[monsterIndex],monsterSpawnPoint.position, monsterSpawnPoint.rotation);
            monsters.Add(monster);
         }

        CalculateDifficulty(ref waveDifficulty);
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
}
