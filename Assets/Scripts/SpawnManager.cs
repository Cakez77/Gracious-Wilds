using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    // Save all enemies here that need to be spawned by the spawner
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    // TODO: Maybe use an int here if possible
    private Dictionary<string, List<string>> _spawnData = new Dictionary<string, List<string>>();
    private bool _spawning;
    private List<string> _entriesToBeRemoved;

	// Use this for initialization
	void Start () {
        ReadSpawnData(LevelManager.Instance.LevelName);
        _entriesToBeRemoved = new List<string>();
        StartCoroutine(CoSpawnEnemies());
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator CoSpawnEnemies()
    {
        while (_spawnData.Count != 0)
        {
            yield return new WaitForSeconds(1f);

            // TODO: Update this to check for the enemy names with the dictionary keys and check them against the prefabs
            foreach (KeyValuePair<string, List<string>> entry in _spawnData)
            {
                // Checking if there are still spawn times left for the monster
                if (entry.Value.Count > 0)
                {
                    // Checking if the time to spawn is reached
                    if (LevelManager.Instance.GameTime > float.Parse(entry.Value[0]))
                    {
                        Instantiate(_enemyPrefabs[0], transform.position, transform.rotation);
                        entry.Value.RemoveAt(0);
                    }
                } else
                {
                    _entriesToBeRemoved.Add(entry.Key);
                }
            }

            // TODO: change this to a list
            if (_entriesToBeRemoved.Count > 0)
            {
                foreach (string keyString in _entriesToBeRemoved)
                {
                    _spawnData.Remove(keyString);
                }
                _entriesToBeRemoved.Clear();
            }
        }
        Debug.Log("Nothing more to spawn here!");
    }


    // TODO: Make a new script later and put the logic in there.
    private void ReadSpawnData(string levelName)
    {
        string[] rawData = Resources.Load<TextAsset>(levelName).text.Split('\n');

        for (int i = 1; i < rawData.Length; i++)
        {
            string[] dataPerMonster = rawData[i].Split(',');
            List<string> spawnTimes = new List<string>(dataPerMonster[1].Split('-'));
            _spawnData.Add(dataPerMonster[0], spawnTimes);
        }
    }
}
