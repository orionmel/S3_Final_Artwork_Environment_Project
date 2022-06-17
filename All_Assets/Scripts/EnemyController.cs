using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //敌人移动
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    //敌人动画
    public Animator anim;

    //敌人的位置变化
    public enum AIState
    {
        isIdle,isPatrolling,isChasing,isAttacking
    };
    public AIState currentState;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;

    //攻击人物
    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    void Start()
    {
        waitCounter = waitAtPoint;
    }

    
    void Update()
    {
        //确定人物的位置
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            //一开始不动
            case AIState.isIdle:
                anim.SetBool("IsMoving",false);

                if(waitCounter > 0){
                    waitCounter -= Time.deltaTime;
                }else{
                    currentState = AIState.isPatrolling;
                    //敌人移动到新的地点
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }
                //追人物
                if(distanceToPlayer <= chaseRange){
                    currentState = AIState.isChasing;
                    //设置敌人的追逐状态
                    anim.SetBool("IsMoving",true);

                }

                break;

            case AIState.isPatrolling:
        
                

                //敌人一步步移动到固定位置，等两秒
                if(agent.remainingDistance <= 0.2f){

                    currentPatrolPoint++;
                    if(currentPatrolPoint >= patrolPoints.Length){
                        currentPatrolPoint = 0;
                    }

                    //每到一个点停两秒钟
                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                //追人物
                if(distanceToPlayer <= chaseRange){
                    currentState = AIState.isChasing;
                }

                //让敌人动起来
                anim.SetBool("IsMoving",true);

                break;

            case AIState.isChasing:

                //敌人开始追人物
                agent.SetDestination(PlayerController.instance.transform.position);

                //当敌人遇到人物开始的动作
                if(distanceToPlayer <= attackRange){
                    //开始袭击人物
                    currentState = AIState.isAttacking;
                    anim.SetTrigger("Attack");
                    //停止跑步动画
                    anim.SetBool("IsMoving",false);

                    //停止移动
                    agent.velocity = Vector3.zero;
                    agent.isStopped =true;

                    attackCounter = timeBetweenAttacks;
                }

                if(distanceToPlayer > chaseRange){
                    //人物接近敌人，敌人追逐
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        agent.velocity = Vector3.zero;
                        agent.SetDestination(transform.position);
                }

                break;

            case AIState.isAttacking:
                //敌人根据人物转动面向
                transform.LookAt(PlayerController.instance.transform,Vector3.up);
                //防止跳起来，敌人倒下去，无穿模
                transform.rotation = Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0){
                    //表示人物接近敌人，敌人开始袭击
                    if(distanceToPlayer < attackRange){
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }else{

                        //人物接近敌人，敌人追逐
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        agent.isStopped = false;
                    }
                }

                break;
        }
    }
}
