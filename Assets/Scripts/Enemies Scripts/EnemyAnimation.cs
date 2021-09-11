using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;

    private CharacterMovement enemyMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        enemyMovementAnimation();
    }
    void enemyMovementAnimation()
    {
        if (enemyMovement.GetMoveDelta().magnitude > 0.0f)
        {
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMTER, true);
        } 
        else
        {
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMTER, false);
        }

    }

    public void DeathAnimation()
    {
        anim.SetTrigger(TagManager.DEATH_ANIMATION_PARAMTER);
    }


} //class
