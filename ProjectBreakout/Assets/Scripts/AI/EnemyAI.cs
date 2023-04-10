using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    public int waypointIndex;
    Vector3 target;
    private bool caughtPlayer;

    [Header("Searching")]
    [SerializeField] private float searchInterval = 5;
    [SerializeField] private float searchTimer;
    [SerializeField] private float lookRadiusY = -90;


    public void DisableRobot()
    {
        enabled = false;
    }

    private void EnableRobot()
    {
        enabled = true;
    }


    public enum AIState { Patrolling, Chasing, Attacking, Searching }
    bool enabled = true;
    [SerializeField] AIState currentState;

    private EnemyVision eVision;

    private void Start()
    {
        eVision = GetComponent<EnemyVision>();
        agent = GetComponent<NavMeshAgent>();

        //Makinayi baslat
        UpdateDestination();
        currentState = AIState.Patrolling;
    }

    private void Update()
    {
        if (enabled)
        {

            //Playeri gorurse
            if (eVision.detected == true)
            {
                currentState = AIState.Chasing;
            }
            else if (currentState != AIState.Patrolling)
            {
                currentState = AIState.Searching;
            }




            //State'e göre aksiyon uygular
            switch (currentState)
            {
                case AIState.Patrolling:
                    Patrolling();
                    break;
                case AIState.Chasing:
                    Chasing();
                    break;
                case AIState.Attacking:
                    Attack();
                    break;
                case AIState.Searching:
                    Search();
                    break;
                default:
                    break;
            }
        }

    }

    private void Attack()
    {
        if (searchTimer <= searchInterval)
        {
            searchTimer += Time.deltaTime;
        }
        else
        {
            currentState = AIState.Patrolling;
        }
    }


    #region Searching

    private void Search()
    {
        if (searchTimer <= searchInterval)
        {
            float lerpValue = Mathf.LerpAngle(lookRadiusY, -lookRadiusY, searchTimer / searchInterval);
            gameObject.transform.rotation = Quaternion.Euler(0f, lerpValue, 0f);
            searchTimer += Time.deltaTime;
        }
        else
        {
            currentState = AIState.Patrolling;

        }
    }

    #endregion

    #region Chasing

    private void Chasing()
    {

        target = eVision.lastSeenPos;
        agent.SetDestination(target);
        if (Vector3.Distance(transform.position, target) < 2)
        {
            if (caughtPlayer == false)
            {
                searchTimer = 0;
                currentState = AIState.Searching;
            }
        }
    }



    //Eger playeri yakalarsa burasi cagirilir
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtPlayer = true;
            Destroy(collision.gameObject);

            searchTimer = 0;
            currentState = AIState.Attacking;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtPlayer = false;
        }
    }

    #endregion

    #region Patrolling
    private void Patrolling()
    {
        //Eger waytpointe yakinsa indexi arttirir ve yenisini bulur
        if (Vector3.Distance(transform.position, target) < 2)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        if (waypoints.Length < 1)
        {
            Debug.LogError(this.gameObject.name + "'in waypointleri assignlanmamis.");
        }
        //Sonraki waypointi bul
        target = waypoints[waypointIndex].position;

        //Oraya doðru ilerle
        agent.SetDestination(target);
    }

    private void IterateWaypointIndex()
    {
        //Waypoint indexi arttýrýr
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    #endregion





}
