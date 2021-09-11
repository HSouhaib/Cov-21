using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponManager[] playerWeapons;

    private int weaponIndex;

    [SerializeField]
    private GameObject[] weaponBullets;

    private Vector2 targetPos;

    private Vector2 direction;

    private Camera mainCam;

    private Vector2 bulletSpawnPosition;

    private Quaternion bulletRotation;

    private CameraShake cameraShake;

    [SerializeField]
    private float cameraShakeCoolDown = 0.2f;


    // in the start of the game we need to intialize our first weapon.
    private void Awake()
    {
        weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);
        mainCam = Camera.main;

        cameraShake = mainCam.GetComponent<CameraShake>();
    }
    private void Update()
    {
        ChangeWeapon();
    }

    // Activate The Side of the current weapon UP/ DOWN /SIDE/DIAGONAL_UP/DIAGONAL_DOWN
    public void ActivateGun(int gunIndex)
    {
        playerWeapons[weaponIndex].ActivateGun(gunIndex);
    }

        // when we press the Q button we will deactivate the previous weapon and activate the next one within the array boundries 
    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerWeapons[weaponIndex].gameObject.SetActive(false);
            weaponIndex++;

            if (weaponIndex == playerWeapons.Length)
                weaponIndex = 0;

            playerWeapons[weaponIndex].gameObject.SetActive(true);
        }
    }

    public void Shoot (Vector3 spawnPos)
    {
        targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        bulletSpawnPosition = new Vector2(spawnPos.x, spawnPos.y);

        direction = (targetPos - bulletSpawnPosition).normalized;

        bulletRotation = Quaternion.Euler(0, 0,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        BullletPool.instance.FireBullet(weaponIndex, spawnPos, bulletRotation, direction);

        cameraShake.ShakeCamera(cameraShakeCoolDown);

        //GameObject newBullet = Instantiate(weaponBullets[weaponIndex], spawnPos, bulletRotation);

        //newBullet.GetComponent<Bullet>().MoveInDirection(direction);
    }








} // class
