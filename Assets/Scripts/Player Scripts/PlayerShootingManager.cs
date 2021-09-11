using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private float ShootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField]
    private Transform bulletSpawnPos;

    private Animator shootingAnimations;
    private PlayerWeaponManager playerWeaponManager;

    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();

        shootingAnimations = bulletSpawnPos.GetComponent<Animator>();
    }
    private void Update()
    {
        HandleShooting();
    }
    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > shootingTimer)
            {
                shootingTimer = Time.time + ShootingTimerLimit;

                //animate muzzle flash
                shootingAnimations.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMTER);

                CreateBullet();
            }
        }
    }
    void CreateBullet()
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position);
    }
}// class
