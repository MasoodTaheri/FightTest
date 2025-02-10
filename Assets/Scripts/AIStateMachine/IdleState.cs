using UnityEngine;

public class IdleState : AIState
{
    public IdleState(AIController aiController) : base(aiController, "IdleState")
    {
    }

    private float switchDuration;

    public override void Enter()
    {
        switchDuration = Random.Range(0.0f, 5.0f);
        AIController._currentCharacterPresenter.SetAnimationIdle();
    }

    public override void Update()
    {
        base.Update();
        if (AIController.Target != null)
        {
            float distance = Vector3.Distance(AIController.transform.position, AIController.Target.position);
            if (distance < AIController.attackRange)
            {
                AIController.ChangeState(new AttackState(AIController));
                return;
            }
            else
            {
                if (Duration > switchDuration)
                    AIController.ChangeState(new MoveState(AIController));
                return;
            }
        }
    }
}