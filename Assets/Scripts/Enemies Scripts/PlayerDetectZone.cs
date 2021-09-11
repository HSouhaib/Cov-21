using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectZone : MonoBehaviour
{
    private EnemyShooter enemeyShooter;

    private void Awake()
    {
        enemeyShooter = GetComponentInParent<EnemyShooter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
            enemeyShooter.SetPlayerInRange(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
            enemeyShooter.SetPlayerInRange(false);
    }
}

