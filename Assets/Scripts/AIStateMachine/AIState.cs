using UnityEngine;

public abstract class AIState
{
    public string StateName;
    protected AIController AIController;
    protected AIController Movement;
    public float Duration;

    public AIState(AIController aiControllerController, string stateName)
    {
        AIController = aiControllerController;
        StateName = stateName;
        Movement = AIController.GetComponent<AIController>();
        Duration = 0;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
        Duration += Time.deltaTime;
    }

    public virtual void Exit()
    {
    }
}