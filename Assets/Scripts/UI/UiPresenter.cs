using UnityEngine;

public class UiPresenter : MonoBehaviour
{
    [SerializeField] private UIView _uiView;
    private UIModel _uiModel;
    public void Initialize(UIModel data)
    {
        _uiModel = data;
        data.Player.OnHealthChanged += data.UpdatePlayerHealthUI;
        data.Enemy.OnHealthChanged += data.UpdateEnemyHealthUI;
        _uiView.Initialize(data);
    }
}