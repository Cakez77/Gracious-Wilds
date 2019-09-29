using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Require Component
public class AttackScript : MonoBehaviour {
    private bool _attacking;
    private Animator _animator;

	// Use this for initialization
	void Start () {
        _attacking = false;
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        // TODO: Use other to get their attack damage and calculate the damage done per second.
        _attacking = true;
        if (_animator.GetInteger("Run_Left") == 1)
        {
            _animator.SetInteger("Run_Left", 0);
            _animator.SetInteger("Run_Right", 0);
            _animator.SetInteger("Attack_Left", 1);
            _animator.SetInteger("Attack_Right", 0);
        }
        else
        {
            _animator.SetInteger("Run_Left", 0);
            _animator.SetInteger("Run_Right", 0);
            _animator.SetInteger("Attack_Left", 0);
            _animator.SetInteger("Attack_Right", 1);
        }
    }

    public bool isAttacking()
    {
        return _attacking;
    }
}
