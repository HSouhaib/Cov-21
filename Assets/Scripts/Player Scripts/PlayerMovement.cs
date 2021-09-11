using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;

    private Camera mainCam;

    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private Animator anim;

    private PlayerWeaponManager playerWeaponManager;

    private CharacterHealth playerHealth;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;

        anim = GetComponent<Animator>();

        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();
    }

    private void FixedUpdate()
    {
        //if (!playerHealth.IsAlive())
        //    return;

        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        HandleMovement(moveX, moveY);
        HandlePlayerTurning();
    }

    //Using the mouse to control the player rotaions 
    void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction = new Vector2(mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y).normalized;

        HandlePlayerAnimation(direction.x, direction.y);
    }

    //controlling the player Movements : Side, Up, Down , Diagonal_up, Diagonal_down
    void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;

        if (x > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (x < 0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;

        x = Mathf.Abs(x);


        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMTER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMTER, y);

        ActivateWeaponForSide(x, y);
    }

    // We align the weapons and them with the player movements especially while moving diagonally
    void ActivateWeaponForSide(float x, float y)
    {
        // side
        if (x == 1f && y == 0f)
            playerWeaponManager.ActivateGun(0);

        //up
        if (x == 0f && y == 1f)
            playerWeaponManager.ActivateGun(1);

        //down
        if (x == 0f && y == -1f)
            playerWeaponManager.ActivateGun(2);

        //Diagonal_Up
        if (x == 1f && y == 1f)
            playerWeaponManager.ActivateGun(3);

        //Diagonal_Down
        if (x == 1f && y == -1f)
            playerWeaponManager.ActivateGun(4);
    }

}//class
