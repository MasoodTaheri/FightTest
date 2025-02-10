using UnityEngine;

public class AttackState : AIState
{
    private float attackTimer = 0f;

    public AttackState(AIController aiController) : base(aiController, "AttackState")
    {
    }

    public override void Enter()
    {
        attackTimer = 0f;
        AIController.Stop();
        AIController._currentCharacterPresenter.SetAnimationIdle();
    }

    public override void Update()
    {
        base.Update();
        if (AIController.Target == null)
        {
            AIController.ChangeState(new IdleState(AIController));
            return;
        }

        Vector3 direction = (AIController.Target.position - AIController.transform.position).normalized;
        AIController.transform.rotation = Quaternion.RotateTowards(AIController.transform.rotation,
            Quaternion.LookRotation(direction), AIController.rotationSpeed * Time.deltaTime);

        attackTimer += Time.deltaTime;
        if (attackTimer >= AIController.attackDelay)
        {
            AIController.Shoot(AIController.ProjectilePrefab, AIController.bulletShootPos.transform.position);
            attackTimer = 0f;
        }

        float distance = Vector3.Distance(AIController.transform.position, AIController.Target.position);
        if (distance < AIController.evadeRange)
        {
            AIController.ChangeState(new EvadeState(AIController));
        }

        if (distance > AIController.attackRange)
        {
            AIController.ChangeState(new IdleState(AIController));
        }
    }
}