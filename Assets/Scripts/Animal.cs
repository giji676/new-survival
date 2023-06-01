using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Animal : MonoBehaviour {
    public enum AnimalType { Friendly, Neutral, Aggressive}
    public enum AIState { Idle, Walking, Eating, Running, Flee, Chasing, Attacking }
    [SerializeField] private AnimalType animalType = AnimalType.Neutral;
    [SerializeField] private AIState currentState = AIState.Idle;
    [SerializeField] private int awarenessArea = 15; // How far the deer should detect the enemy
    [SerializeField] private float walkingSpeed = 3.5f;
    [SerializeField] private float runningSpeed = 7f;
    [SerializeField] private Vector2 eatingTimeRange = new Vector2(10, 15);
    [SerializeField] private float stuckDistanceThreshold = 0.01f; // Distance used to check if agent has moved or is stuck

    [Header("Neutral and Aggressive only")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackDistance = 5f; // Distance for agent to start attacking enemy
    [SerializeField] private float attackDelay = 1.5f; // Time between hits
    [SerializeField] private float timeToStopChasing = 60f; // How long the animal should chase before it stops chasing
    private float timeToStopChasingCurrent = 0f; // Keep track of time passed since chasing started
    private bool isAttacking = false;

    private SphereCollider awarenessCollider; // Trigger collider that represents the awareness area
    private NavMeshAgent agent;
    private Animator animator;

    private bool switchAction = false;
    private float actionTimer = 0; // Timer duration till the next action
    private Transform enemy;
    private float range = 20; // How far the Deer have to run to resume the usual activities
    private float multiplier = 1;

    private bool reverseFlee = false; // In case the AI is stuck, send it to one of the original Idle points
    private Vector3 closestEdge; // Detect NavMesh edges to detect whether the AI is stuck
    private float distanceToEdge;
    private float distance; // Squared distance to the enemy
    private float timeStuck = 0; // How long the AI has been near the edge of NavMesh, if too long, send it to one of the random previousIdlePoints
    private List<Vector3> previousIdlePoints = new List<Vector3>(); // Store previous idle points for reference
    private List<Vector3> previousPoints = new List<Vector3>(); // Store previous points for reference

    private void Start() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.autoBraking = true;
        agent.autoRepath = true;

        awarenessCollider = gameObject.AddComponent<SphereCollider>();
        awarenessCollider.isTrigger = true;
        awarenessCollider.radius = awarenessArea;

        GetComponent<Dummy>().onHealthChangeCallback += Hit;

        // Initialize the AI state
        currentState = AIState.Idle;
        actionTimer = Random.Range(0.1f, 2.0f);
        SwitchAnimationState(currentState);
    }

    private void Hit() {
        // Set NavMesh Agent Speed
        agent.speed = runningSpeed;
        if (enemy) {
            agent.SetDestination(enemy.position);
            currentState = AIState.Chasing;
            SwitchAnimationState(currentState);
        }
        else {
            agent.destination = RandomNavSphere(transform.position, Random.Range(15, 40));
            currentState = AIState.Running;
            SwitchAnimationState(AIState.Running);
        }
    }

    private void Update() {
        // Wait for the next course of action
        if (actionTimer > 0) {
            actionTimer -= Time.deltaTime;
        }
        else {
            switchAction = true;
        }

        if (currentState == AIState.Idle) {
            previousPoints.Clear();

            if(switchAction) {
                if (enemy) {
                    if (animalType == AnimalType.Friendly) {
                        // Run away
                        agent.SetDestination(RandomNavSphere(transform.position, Random.Range(1, 2.4f)));
                        currentState = AIState.Running;
                        SwitchAnimationState(currentState);
                    }
                    else if (animalType == AnimalType.Neutral) {
                        // Do nothing unless player hits

                        // If player hits
                        // Chase the player, and attack when in range
                        // ? flee if low on health
                        agent.SetDestination(enemy.position);
                        currentState = AIState.Chasing;
                        SwitchAnimationState(currentState);
                    }
                    else if (animalType == AnimalType.Aggressive) {
                        // Chase the player, and attack when in range
                        // ? flee if low on health
                        agent.SetDestination(enemy.position);
                        currentState = AIState.Chasing;
                        SwitchAnimationState(currentState);
                    }
                }
                else {
                    // No enemies nearby, start eating
                    actionTimer = Random.Range(eatingTimeRange.x, eatingTimeRange.y);

                    currentState = AIState.Eating;
                    SwitchAnimationState(currentState);

                    // Keep last 5 Idle positions for future reference
                    previousIdlePoints.Add(transform.position);
                    if (previousIdlePoints.Count > 5) {
                        previousIdlePoints.RemoveAt(0);
                    }
                }
            }
        }
        else if (currentState == AIState.Walking) {
            previousPoints.Add(transform.position);
            if (previousPoints.Count > 50) {
                previousPoints.RemoveAt(0);
            }

            if (previousPoints.Count == 50 && CheckIfStuck()) {
                if(previousIdlePoints.Count > 0) {
                    Vector3 goTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                    agent.SetDestination(goTo);
                } 
            }
            // Set NavMesh Agent Speed
            agent.speed = walkingSpeed;

            // Check if we've reached the destination
            if (DoneReachingDestination()) {
                currentState = AIState.Idle;
            }
        }
        else if (currentState == AIState.Eating) {
            previousPoints.Clear();

            if (switchAction) {
                // Wait for current animation to finish playing
                if(!animator || animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 0.99f) {
                    // Walk to another random destination
                    agent.destination = RandomNavSphere(transform.position, Random.Range(5, 10));
                    currentState = AIState.Walking;
                    SwitchAnimationState(currentState);
                }
            }
        }
        else if (currentState == AIState.Running) {
            // Set NavMesh Agent Speed
            agent.speed = runningSpeed;

            // Run away
            if (enemy) {
                if (reverseFlee) {
                    if (DoneReachingDestination() && timeStuck < 0) {
                        reverseFlee = false;
                    }
                    else {
                        timeStuck -= Time.deltaTime;
                    }
                }
                else {
                    Vector3 runTo = transform.position + ((transform.position - enemy.position) * multiplier);
                    distance = (transform.position - enemy.position).sqrMagnitude;

                    // Find the closest NavMesh edge
                    NavMeshHit hit;
                    if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas)) {
                        closestEdge = hit.position;
                        distanceToEdge = hit.distance;
                        // Debug.DrawLine(transform.position, closestEdge, Color.red);
                    }

                    previousPoints.Add(transform.position);
                    if (previousPoints.Count > 50) {
                        previousPoints.RemoveAt(0);
                    }

                    if (distanceToEdge < 1f) {
                        if(timeStuck > 1.5f) {
                            if(previousIdlePoints.Count > 0) {
                                runTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                                reverseFlee = true;
                            } 
                        }
                        else {
                            timeStuck += Time.deltaTime;
                        }
                    }

                    if (previousPoints.Count == 50 && CheckIfStuck()) {
                        if(previousIdlePoints.Count > 0) {
                            runTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                            reverseFlee = true;
                        } 
                    }

                    if (distance < range * range) {
                        agent.SetDestination(runTo);
                    }
                    else {
                        enemy = null;
                    }
                }
                
                // Temporarily switch to Idle if the Agent stopped
                if(agent.velocity.sqrMagnitude < 0.1f * 0.1f) {
                    SwitchAnimationState(AIState.Idle);
                }
                else {
                    SwitchAnimationState(AIState.Running);
                }
            }
            else {
                // Check if we've reached the destination then stop running
                if (DoneReachingDestination()) {
                    actionTimer = Random.Range(1.4f, 3.4f);
                    currentState = AIState.Eating;
                    SwitchAnimationState(AIState.Idle);
                }
            }
        }
        else if (currentState == AIState.Flee) {
            // Set NavMesh Agent Speed
            agent.speed = runningSpeed;
            
            previousPoints.Add(transform.position);
            if (previousPoints.Count > 50) {
                previousPoints.RemoveAt(0);
            }

            if (previousPoints.Count == 50 && CheckIfStuck()) {
                if(previousIdlePoints.Count > 0) {
                    Vector3 goTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                    agent.SetDestination(goTo);
                } 
            }

            currentState = AIState.Running;
            SwitchAnimationState(AIState.Running);
            agent.destination = RandomNavSphere(transform.position, Random.Range(5, 10));

        }
        else if (currentState == AIState.Chasing) {
            // Set NavMesh Agent Speed
            agent.speed = runningSpeed;

            // Chase
            if (enemy) {
                timeToStopChasingCurrent += Time.deltaTime;
                if (timeToStopChasingCurrent >= timeToStopChasing) {
                    timeToStopChasingCurrent = 0f;
                    // Walk to another random destination
                    agent.destination = RandomNavSphere(transform.position, Random.Range(5, 10));
                    currentState = AIState.Walking;
                    SwitchAnimationState(currentState);
                }
                agent.SetDestination(enemy.position);

                
                previousPoints.Add(transform.position);
                if (previousPoints.Count > 50) {
                    previousPoints.RemoveAt(0);
                }

                if (previousPoints.Count == 50 && CheckIfStuck()) {
                    if(previousIdlePoints.Count > 0) {
                        Vector3 goTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                        agent.SetDestination(goTo);
                    } 
                }

                if (agent.remainingDistance <= attackDistance) {
                    agent.SetDestination(agent.transform.position);
                    currentState = AIState.Attacking;
                    SwitchAnimationState(currentState);
                }
            }
            else {
                timeToStopChasingCurrent = 0f;
                // Walk to another random destination
                agent.destination = RandomNavSphere(transform.position, Random.Range(5, 10));
                currentState = AIState.Walking;
                SwitchAnimationState(currentState);
            }
        }
        else if (currentState == AIState.Attacking) {
            if (enemy) {
                if (Vector3.Distance(transform.position, enemy.position) - 0.3f >= attackDistance) {
                    agent.SetDestination(enemy.position);
                    currentState = AIState.Chasing;
                    SwitchAnimationState(currentState);
                    StopAllCoroutines();
                    isAttacking = false;
                }
                else {
                    PlayerHealth playerHealth = enemy.gameObject.GetComponent<PlayerHealth>();
                    if (playerHealth && !isAttacking) {
                        StartCoroutine(Attack(playerHealth));
                    }
                }
            }
        }
        switchAction = false;
    }

    IEnumerator Attack(PlayerHealth playerHealth) {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        playerHealth.DecreaseHealth(damage);
        isAttacking = false;
    }

    private bool DoneReachingDestination() {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    // Done reaching the Destination
                    return true;
                }
            }
        }

        return false;
    }

    private void SwitchAnimationState(AIState state) {
        // Animation control
        if (animator) {
            animator.SetBool("isEating", state == AIState.Eating);
            animator.SetBool("isRunning", state == AIState.Running || state == AIState.Chasing);
            animator.SetBool("isWalking", state == AIState.Walking);
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance) {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }

    private void OnTriggerEnter(Collider other) {
        // Make sure the Player instance has a tag "Player"
        if (!other.CompareTag("Player")) return;

        enemy = other.transform;

        actionTimer = Random.Range(0.24f, 0.8f);
        currentState = AIState.Idle;
        SwitchAnimationState(currentState);
    }

    private Vector3 GetAveragePosition() {
        Vector3 sum = Vector3.zero;

        for (int i = 0; i < previousPoints.Count; i++) {
            sum += previousPoints[i];
        }

        return sum / previousPoints.Count;
    }

    private bool CheckIfStuck() {
        if (DoneReachingDestination()) return false;

        Vector3 averagePosition = GetAveragePosition();
        // If the average of the previous 50 positions is less than stuckDistanceThreshold then agent's stuck
        if (Vector3.Distance(transform.position, averagePosition) <= stuckDistanceThreshold) return true;

        return false;
    }
}