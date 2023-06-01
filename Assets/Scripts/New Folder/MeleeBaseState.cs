using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;



    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> collidersDamaged;
    // The Hit Effect to Spawn on the afflicted Enemy
    private GameObject HitEffectPrefab;

    // Input buffer Timer
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitbox;
        HitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }


        if (Input.GetMouseButtonDown(1))
        {
            AttackPressedTimer = 2;
        }

        if (Input.GetMouseButtonDown(1) && animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer >= 0)
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {
            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();
                if (hitTeamComponent != null)
                {
                    HealthManager healthManager = hitTeamComponent.gameObject.GetComponentInChildren<HealthManager>();

                    if (hitTeamComponent.teamIndex == TeamIndex.Enemy && healthManager != null)
                    {
                        GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
                        healthManager.TakeDamage(15);
                        Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
                        collidersDamaged.Add(collidersToDamage[i]);
                    }
                    else if (hitTeamComponent.teamIndex == TeamIndex.Boss && healthManager != null)
                    {
                        HealthManager bossHealthManager = hitTeamComponent.gameObject.GetComponentInChildren<HealthManager>();
                        if (bossHealthManager != null)
                        {
                            GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
                            bossHealthManager.TakeDamage(10);
                            Debug.Log("Boss Has Taken:" + attackIndex + "Damage");
                            collidersDamaged.Add(collidersToDamage[i]);
                        }
                    }
                }
            }
        }
    }

}
