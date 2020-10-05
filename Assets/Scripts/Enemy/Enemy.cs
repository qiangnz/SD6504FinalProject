using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : EnemyManager
{
    // public List<Transform> endPoints;
    // public Transform middlePoint;
    // public GameObject currentEnemy;

    // public HealthBarManager healthBar;

    // private Animator anim;
	// private CharacterController controller;
	// private bool battle_state;
	// public float speed = 6.0f;
	// public float runSpeed = 1.7f;
	// public float turnSpeed = 60.0f;
	// public float gravity = 20.0f;
    // public int maxHealth = 20;
    // private int currentHealth;
	// private Vector3 moveDirection = Vector3.zero;
    // public DirectionManager directionManager;
    // UnityEngine.AI.NavMeshAgent nav;
    // private Transform tempTarget;
    // private Transform movetempTarget;

    // private AudioSource audio;
	// public AudioClip injurySound, dieSound, attackSound;
	// public float volume; 

    // bool isSinking;   
    // public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead. 
    // GameObject player;                          // Reference to the player GameObject.
    // private bool playerInRange = false;
    // public int attackDamage = 10;               // The amount of health taken away from hero per attack.
    // float timer;  
    // public float timeBetweenAttacks = 0.5f;

    // private void Awake()
    // {
    //     movetempTarget = middlePoint;
    //     tempTarget = middlePoint;
    //     anim = GetComponent<Animator>();
	// 	controller = GetComponent<CharacterController> ();
    //     player = GameObject.FindGameObjectWithTag ("Player");
    //     nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    // }
    // // Start is called before the first frame update
    // void Start()
    // {
    //     audio = GetComponent<AudioSource>();
    //     directionManager.OnReDirection += OnRedirection;
    //     healthBar.SetMaxHealth(maxHealth);
    //     currentHealth = maxHealth;
    //     Run();

    // }

    // private void SetBattle(bool isBattle) {
    //     anim.SetInteger("battle", isBattle ? 1 : 0);
    //     battle_state =  isBattle;
    // }

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


    // private void OnRedirection(object sender, EventBusManager e) {
    //     if (e.Messgae.Equals(currentEnemy.name))
    //     {
    //         int index = UnityEngine.Random.Range(0, 2);
    //         tempTarget = endPoints[index];
    //         movetempTarget = endPoints[index];
    //     }
    // }


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



    // public void StartSinking ()
    // {
    //     // Find and disable the Nav Mesh Agent.
    //     GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

    //     // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
    //     GetComponent <Rigidbody> ().isKinematic = true;

    //     // The enemy should no sink.
    //     isSinking = true;

    //     // Increase the score by the enemy's score value.
    //     //ScoreManager.score += scoreValue;

    //     // After 2 seconds destory the enemy.
    //     Destroy (gameObject, 2f);
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     timer += Time.deltaTime;

    //     // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
    //     if(timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
    //     {
    //         // ... attack.
    //         Attack();
    //     }
    //     // If the enemy and the player have health left...
    //     if(currentHealth > 0)
    //     {
    //         // ... set the destination of the nav mesh agent to the player.
    //         MoveToTarget();
    //     } else
    //     {
    //         // ... disable the nav mesh agent.
    //         nav.enabled = false;

    //     }
    //     // If the enemy should be sinking...
    //     if(isSinking)
    //     {
    //         // ... move the enemy down by the sinkSpeed per second.
    //         transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
    //     }


    // }
}
