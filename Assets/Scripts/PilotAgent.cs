using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Barracuda;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Debug = UnityEngine.Debug;

public class PilotAgent : Agent
{   
    
   
    private Enemy ship;
    private Transform spawnPosition;
    public TargetingSystem targetingSystem;
    public AgentProjectileController agentProjectileController;
    public GameObject target;
    public GameObject explosionPrefab;
    private GameObject explosion;
    private bool isRunning;
    
    private void Awake()
    {
        ship = GetComponent<Enemy>();
        spawnPosition = GameObject.Find("SpawnPosition").transform;
    }

    void Start()
    {
        ship.onCollision += Enemy_OnCollision;
        targetingSystem.onTargetLeave += TargetingSystem_OnTargetLeave;
        agentProjectileController.onEnemyHit += AgentProjectileController_OnEnemyHit;
        CheckTarget();
    }

    private void Enemy_OnCollision(object sender, Enemy.CollisionEventArgs e)
    {
        GameObject collision = e.eventArgsCollision;
        //set counter for hits as a metric for damaged animation and ultimately destruction of ship.
        //if(collision.gameObject.CompareTag("projectile"))
        if (collision != null)
        {
            AddReward(-1f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // Debug.Log(collision == null ? "wall" : "object");
            target = null;
            StartCoroutine(Explode());
            EndEpisode();
        }
    }
    private void TargetingSystem_OnTargetLeave(object sender, TargetingSystem.TargetEventArgs e)
    {
        if (e.targetLeft == target)
            target = null;
    }

    private void AgentProjectileController_OnEnemyHit(object sender, AgentProjectileController.EnemyHitEventArgs e)
    {
        if(e.enemyHit == target)
            AddReward(1f);
    }

    IEnumerator Explode()
    {
        explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(explosion);
    }

    private void CheckTarget()
    {
        if (target == null)
        {
            target = targetingSystem.PickTarget();
            //Debug.Log(target == null ? "target not found" : "target chosen");
            float time = target == null ? 1f : 0.5f;
            Invoke(nameof(CheckTarget),time);
            return;
        }
        
        float range = Vector3.Dot(transform.forward, (target.transform.position - transform.position).normalized);
        float reward = range * 0.1f;
        //Debug.Log("reward given: " + reward);
        AddReward(reward);

        if (range > 0.8f)
        {
            if (!isRunning)
            {
                StartCoroutine(Shoot());
            }
        }

        Invoke(nameof(CheckTarget),0.5f);
    }
    
    IEnumerator Shoot()
    {
        isRunning = true;
        agentProjectileController.Shoot();
        yield return new WaitForSeconds(0.16f);
        agentProjectileController.Shoot();
        isRunning = false;
    }

    public override void OnEpisodeBegin() {
         transform.position = spawnPosition.position + new Vector3(UnityEngine.Random.Range(-400,400f),UnityEngine.Random.Range(-400,400f),UnityEngine.Random.Range(-400,400f));
         transform.forward = new Vector3(UnityEngine.Random.Range(-100,100f),UnityEngine.Random.Range(-100,100f),UnityEngine.Random.Range(-100,100f)).normalized;
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(ship.GetPitch());
        //sensor.AddObservation(ship.GetYaw());
        sensor.AddObservation(ship.GetRoll());
    }


    public override void OnActionReceived(ActionBuffers actions){
        
        float pitch=actions.ContinuousActions[0];
        float roll=actions.ContinuousActions[1];
        
        ship.RotateShip(pitch,roll);
        
    }
    //disabled to not interfere with agent movement while moving the player.
    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    //     continuousActions[0] = Input.GetAxisRaw("Vertical");
    //     continuousActions[1] = Input.GetAxisRaw("Horizontal");
    // }

    private void OnApplicationQuit()
    {
        ship.onCollision -= Enemy_OnCollision;
        targetingSystem.onTargetLeave -= TargetingSystem_OnTargetLeave;
        agentProjectileController.onEnemyHit -= AgentProjectileController_OnEnemyHit;
    }
}
