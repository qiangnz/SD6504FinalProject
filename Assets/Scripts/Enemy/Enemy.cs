using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : EnemyManager
{


    public override void Run() {
        SetBattle(true);
        anim.SetInteger("moving", 2);
    }

    public override void Walk() {
        SetBattle(false);
        anim.SetInteger("moving", 1);
    }
    IEnumerator reset()
    {   
        yield return new WaitForSeconds(0.01f);
        anim.SetInteger ("moving", 0);
    }
    IEnumerator restore()
    {   
        yield return new WaitForSeconds(0.01f);
        isHurted = false;
    }

    public override void Injuried() {
        Debug.Log("Injuried");
        int n = UnityEngine.Random.Range(0,2);
        if (n == 0) {
            anim.SetInteger("moving", 10);
        } else {
            anim.SetInteger("moving", 11);
        }
        StartCoroutine(reset());
        StartCoroutine(restore());
        audio.PlayOneShot(injurySound, volume);
    }
    private bool isDeath = false;
    public override void Death() {
        if (!isDeath) {
            isDeath = true;
            int n = UnityEngine.Random.Range(0,2);
            Debug.Log("Death" + n);
            if (n == 0) {
                anim.SetInteger("moving", 12);
            } else {
                anim.SetInteger("moving", 13);
            }
            StartCoroutine(reset());
            audio.PlayOneShot(dieSound, volume);
        }
    }

    private void AttackAction() {
        if (!battle_state) {
            SetBattle(true);
        }
        int n = UnityEngine.Random.Range(0,3);
        if (n == 0) {
            anim.SetInteger("moving", 3);
        } else if (n == 1){
            anim.SetInteger("moving", 4);
        } else {
            anim.SetInteger("moving", 5);
        }
        audio.PlayOneShot(attackSound, volume);
    }
    public override void SetTarget(GameObject gameObject) {
        if (gameObject != null) {
            tempTarget = gameObject.transform;
        } else {
            tempTarget = movetempTarget;
        }
    }

    public override void Attack() {
        // Reset the timer.
        Debug.Log("Attack");
        timer = 0f;
        AttackAction();


    }
    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }




    public override void MoveToTarget() {
        // nav.Warp(tempTarget.position);
        nav.SetDestination (tempTarget.position);
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject == player) {

        }
    }
    private bool isHurted = false;
    public override void TakeDamage(int damage)  {
        if (!isHurted) {
            isHurted = true;
            currentHealth -= damage;
            Debug.Log("CurrentEnemy:" + currentEnemy.name + "be hurted" + damage + "%%%%" + currentHealth);
            healthBar.SetHealth(currentHealth < 0 ? 0 : currentHealth);   
            if(currentHealth <= 0)
            {
                // enemy die.
                Death();
            } else {
                Injuried();
            }
        }
    }

}
