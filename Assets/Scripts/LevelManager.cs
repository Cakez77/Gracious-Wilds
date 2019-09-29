using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Have the LevelManager hold all the important fields, like waypoints, spawnpoints etc.
// to limit the use of memory
public class LevelManager : Singleton<LevelManager> {

    // TODO: Have a file with all the levels and call CreateLevel(level) to make this field obsolete
    [SerializeField]
    private GameObject level;

    private GameObject _levelInstance;
    private Transform _spawnPoints;
    private List<Transform> _placePoints;

    public float GameTime
    {
        get
        {
            return Time.time;
        }
    }

    public Transform SpawnPoints
    {
        get
        {
            return _spawnPoints;
        }
    }

    // TODO: Add another Field to signal empty PlacePoints
    public List<Transform> PlacePoints
    {
        get
        {
            return _placePoints;
        }
    }

    public string LevelName
    {
        get
        {
            return _levelInstance.name;
        }
    }

    // Use this for initialization
    void Start ()
    {
        // Instantiate the level
        CreateLevel();

        _placePoints = findPlacePoints();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void CreateLevel()
    {
        _levelInstance = Instantiate(level);
        _levelInstance.name = level.name;
    }

    private List<Transform> findPlacePoints()
    {
        List<Transform> placePoints = new List<Transform>();
        // Returns the child at a fixed position of 2 meaning the 
        foreach(Transform placePoint in _levelInstance.transform.GetChild(2))
        {
            placePoints.Add(placePoint);
        }
        return placePoints;
    }

    private Transform findSpawnPoints()
    {
        return _levelInstance.transform.GetChild(1).transform;
    }

    public Transform getWaypoint(int spawnPoint, int waypoint)
    {
        return _spawnPoints.GetChild(spawnPoint).GetChild(waypoint);
    }

    public void RemovePlacePoint(Transform placePoint)
    {
        _placePoints.Remove(placePoint);
    }
}
