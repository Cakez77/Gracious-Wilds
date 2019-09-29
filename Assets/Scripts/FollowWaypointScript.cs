using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypointScript : MonoBehaviour
{
    private int _targetWaypoint = 0;
    private Transform _waypoints;
    private Animator _animator;
    private AttackScript _attackScript;

    public float movementSpeed = 0.1f;


	// Use this for initialization
	void Start ()
    {
        Transform enemyPaths = GameObject.Find("Waypoints").transform;
        _waypoints = enemyPaths;
        _animator = GetComponent<Animator>();
        _attackScript = GetComponent<AttackScript>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    // Fixed update
    void FixedUpdate()
    {
        if (!_attackScript.isAttacking())
        {
            HandleWalkWaypoints();
        }
    }


    // Handle walking the waypoints
    private void HandleWalkWaypoints()
    {
        Transform myPos =  transform;
        Transform targetWaypoint = _waypoints.GetChild(_targetWaypoint);
        Vector3 relative = targetWaypoint.position - transform.position;
        float distanceToWaypoint = relative.magnitude;

        if (distanceToWaypoint < 0.2)
        {
            if (_targetWaypoint + 1 < _waypoints.childCount)
            {
                // Set new wapyoint as target
                _targetWaypoint++;
            }
            else
            {
                // Destory the gameObject for now and return
                // TODO: remove one life from the counter
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            // Walk towards waypoint
            transform.position = Vector3.MoveTowards(GetComponent<Transform>().position, targetWaypoint.position, movementSpeed);
        }

        // Face walk direction
        if (transform.position.x < targetWaypoint.position.x)
        {
            // Move right
            _animator.SetInteger("Run_Left", 0);
            _animator.SetInteger("Run_Right", 1);
            _animator.SetInteger("Attack_Left", 0);
            _animator.SetInteger("Attack_Right", 0);
        }
        else
        {
            // Move left
            _animator.SetInteger("Run_Right", 0);
            _animator.SetInteger("Run_Left", 1);
            _animator.SetInteger("Attack_Left", 0);
            _animator.SetInteger("Attack_Right", 0);
        }
    }
}
