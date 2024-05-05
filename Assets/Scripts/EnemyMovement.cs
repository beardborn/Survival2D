using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;
    [SerializeField] private SpriteRenderer enemyRend;
    [SerializeField] private SpriteRenderer indicatorRend;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private bool hasSpawned;

    [Header("Effects")]
    [SerializeField] private ParticleSystem deathParticles;


    [Header("Debug")]
    [SerializeField] private bool showDebug;

    void Start()
    {
        player = FindFirstObjectByType<Player>();

        if (player == null )
        {
            Debug.LogWarning("Cannot find player auto-destory activated...");
            Destroy(gameObject);
        }

        // hide renderer
        enemyRend.enabled = false;
        //show spawn indicator
        indicatorRend.enabled = true;
        // scale up and down indicator
        Vector3 scaleDown = indicatorRend.transform.localScale * 0.8f;
        LeanTween.scale(indicatorRend.gameObject, scaleDown, 0.3f).setLoopPingPong(5).setOnComplete(SpawnSequanceComplete);
        // show enemy after 3 seconds

        // prevent following and attacking during spawn
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned) { return; }
        FollowPlayer();
        TryAttack();
    }

    private void SpawnSequanceComplete()
    {
        indicatorRend.enabled = false;
        enemyRend.enabled = true;
        hasSpawned = true;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            DeathAnimation();
        }
    }

    private void OnDrawGizmos()
    {
        if (showDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
        }
    }

    private void DeathAnimation()
    {
        deathParticles.transform.SetParent(null); // makes child of scene
        deathParticles.Play();

        Destroy(gameObject);
    }
}
