using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolWaitTime = 1f;
    public float chaseWaitSpeed = 1f;
    public Transform[] patrolWayPoints;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private PlayerHealth playerHeadlth;
    private LastPlayerSighting lastPlayerSighting;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIdex;       //index of patrolWayPoints

    void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHeadlth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

    }

    void Update()
    {
        if (enemySight.playerInSight && playerHeadlth.health > 0f)
            Shooting();
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetposition && playerHeadlth.health > 0)
            Chasing();
        else
            Patrolling();
    }

    void Shooting()
    {
        nav.Stop();
    }

    void Chasing()
    {
        Vector3 sightDelteaPos = enemySight.personalLastSighting - transform.position;
        if (sightDelteaPos.sqrMagnitude > 4f)
           nav.destination = enemySight.personalLastSighting;

        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;

            if (chaseTimer >= chaseWaitSpeed)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetposition;
                enemySight.personalLastSighting = lastPlayerSighting.resetposition;
                chaseTimer = 0f;
            }
        }
        else
            // 如果敌人没有靠近玩家最后出现的位置 那么重置计时器（因为追踪位置不是固定的 玩家可能还会在其他位置触发警报 从而刷新追踪位置）
            chaseTimer = 0f;
        
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighting.resetposition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIdex == patrolWayPoints.Length - 1)
                    wayPointIdex = 0;
                else
                    wayPointIdex++;

                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;

        nav.destination = patrolWayPoints[wayPointIdex].position;
    }
}
