using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    protected List<Transform> endPoints = new List<Transform>();
    protected Transform middlePoint;
    protected Transform startPoint;
    public GameObject currentEnemy;

    public HealthBarManager healthBar;

    protected Animator anim;
	protected CharacterController controller;
	protected bool battle_state;
	public float speed = 6.0f;
	public float runSpeed = 1.7f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
    public int maxHealth = 20;
    protected int currentHealth;
    protected DirectionManager directionManager;
    protected UnityEngine.AI.NavMeshAgent nav;
    protected Transform tempTarget;
    protected Transform movetempTarget;

    protected AudioSource audio;
	public AudioClip injurySound, dieSound, attackSound;
	public float volume; 

    protected bool isSinking;   
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead. 
    protected GameObject player;                          // Reference to the player GameObject.
    protected bool playerInRange = false;
    public int attackDamage = 10;               // The amount of health taken away from hero per attack.
    protected float timer;  
    public float timeBetweenAttacks = 0.5f;
    protected HeroHealth playerHealth; 

    public string enemyType;
    public string enemyColor;


    private void Awake()
    {   
        init();
        GameObject middle = GameObject.FindWithTag("MiddlePoint");
        middlePoint = middle.transform;
        movetempTarget = middlePoint;
        tempTarget = middlePoint;
        directionManager = middle.GetComponent<DirectionManager>();
        GameObject[] ends = GameObject.FindGameObjectsWithTag("EndPoint");
        foreach (GameObject element in ends)
        {
            if (element != null) {
                endPoints.Add(element.transform);
            }
        }
        anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        audio = GetComponent<AudioSource>();
        directionManager.OnReDirection += OnRedirection;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    private void init() {
        startPoint = GameObject.FindWithTag("StartPoint").transform;
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <HeroHealth> ();
    }
    public void Spawn() {
        init();
        if(playerHealth.currentHealth <= 0)
        {
            // ... exit the function.
            return;
        }
        Vector3 startPos = startPoint.position;
        int x = UnityEngine.Random.Range(10, 60);
        int z = UnityEngine.Random.Range(-60, 60);
        Vector3 newPosition = new Vector3(startPos.x - x, startPos.y, startPos.z + z);
        Instantiate (currentEnemy, newPosition, startPoint.rotation);
        
    }
    
    // Start is called before the first frame update
    protected void Start()
    {
        // id = UnityEngine.Random.Range(0, 1000);
        Run();

    }
    private void OnRedirection(object sender, EventBusManager e) {
        if (e.MEnemyManager == currentEnemy)
        {
            int index = UnityEngine.Random.Range(0, 2);
            tempTarget = endPoints[index];
            movetempTarget = endPoints[index];
        }
    }
    protected void SetBattle(bool isBattle) {
        anim.SetInteger("battle", isBattle ? 1 : 0);
        battle_state =  isBattle;
    }

    public void StartSinking ()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent <Rigidbody> ().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy (gameObject, 2f);
    }

    // Update is called once per frame
    protected void Update()
    {
        Debug.Log("Health:" + currentHealth + "=====" + playerHealth.currentHealth);
        timer += Time.deltaTime;
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }
        // If the enemy and the player have health left...
        if(currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            MoveToTarget();
        } else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;

        }
        // If the enemy should be sinking...
        if(isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }


    }
    public abstract void Run();
    public abstract void Walk();
    public abstract void Injuried();
    public abstract void Death();
    public abstract void Attack();
    public abstract void MoveToTarget();
    public abstract void SetTarget(GameObject gameObject);
    public abstract void TakeDamage(int damage) ;

}
