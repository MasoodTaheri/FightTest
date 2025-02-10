using UnityEngine;

public class EvadeState : AIState
{
    private float evadeTimer = 0f;

    public EvadeState(AIController aiController) : base(aiController, "EvadeState")
    {
    }

    public override void Enter()
    {
        evadeTimer = 0f;
    }

    public override void Update()
    {
        base.Update();
        if (AIController.Target == null)
        {
            AIController.ChangeState(new IdleState(AIController));
            return;
        }

        Vector3 direction = (AIController.transform.position - AIController.Target.position).normalized;
        Movement.Move(direction);

        evadeTimer += Time.deltaTime;
        if (evadeTimer >= AIController.evadeDuration)
        {
            AIController.ChangeState(new MoveState(AIController));
        }
    }
}