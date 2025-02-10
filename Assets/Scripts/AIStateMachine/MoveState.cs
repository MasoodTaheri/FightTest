using UnityEngine;
using UnityEngine.AI;

public class MoveState : AIState
{
    private Vector3 _target;
    private float _range = 45.0f;

    public MoveState(AIController aiController) : base(aiController, "MoveState")
    {
        AIController._currentCharacterPresenter.SetAnimationWalk();
    }

    public override void Enter()
    {
        if (AIController.Target == null)
            return;
        _target = GetRandomPosition();
        Movement.Move(_target);
    }


    public Vector3 GetRandomPosition()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPosition =
                AIController.transform.position + Random.insideUnitSphere * _range;
            if (IsReachable(randomPosition))
                return randomPosition;
        }

        return Vector3.zero;
    }

    private bool IsReachable(Vector3 targetPosition)
    {
        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit targetHit, 1.0f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();

            if (NavMesh.CalculatePath(AIController.transform.position, targetHit.position, NavMesh.AllAreas, path))
            {
                return (path.status == NavMeshPathStatus.PathComplete);
            }
        }

        return false;
    }

    public override void Update()
    {
        base.Update();
        if (AIController.Target == null)
        {
            AIController.ChangeState(new IdleState(AIController));
            return;
        }

        if (Vector3.Distance(AIController.transform.position, _target) < 1.0f)
        {
            AIController.ChangeState(new IdleState(AIController));
        }

        float distance = Vector3.Distance(AIController.transform.position, AIController.Target.position);
        if (distance < AIController.attackRange)
        {
            AIController.ChangeState(new AttackState(AIController));
        }
    }
}