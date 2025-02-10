using UnityEngine;

public class Manager : MonoBehaviour
{
    public UiPresenter MenuHandler;
    public AIController AIController;
    public PlayerController PlayerController;

    void Start()
    {
        var uiModel = new UIModel()
        {
            Player = PlayerController,
            Enemy = AIController,
            ShootAction = PlayerController.ShootToTarget,
            SwitchCharacter = PlayerController.NextCharacter
        };
        MenuHandler.Initialize(uiModel);

        PlayerController.Enemy = AIController.gameObject;
        AIController.Target = PlayerController.gameObject.transform;
    }
}