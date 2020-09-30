using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttacking : MonoBehaviour
{

public Animator animator;
public Transform attackPoint;
public LayerMask enemyLayers;

public float attackRange=0.5f;

void Update()
{
        if(Input.GetButton ("Fire1"))
        {
            Attack();
        }

}

void Attack()
{
    // Collider [] hitEnemies =Physics.OverlapCircelAll(attackPoint.position,attackRange,enemyLayers);

    // foreach(Collider enemy in hitEnemies)
    // {
    //     Debug.Log("We hit"+enemy.name);
    // }
}

void OnDrawGizmosSelected() 
{
if(attackPoint==null)
return;

Gizmos.DrawWireSphere(attackPoint.position,attackRange);

}



}
