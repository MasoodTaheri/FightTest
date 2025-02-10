using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private HealthBar _playerHealthBar;
    [SerializeField] private HealthBar _enemyHealthBar;
    [SerializeField] private Button _switchCharacter;
    [SerializeField] private Button _shoot;


    public void Initialize(UIModel uiModel)
    {
        _shoot.onClick.AddListener(() => { uiModel.ShootAction(); });
        _switchCharacter.onClick.AddListener(() => { uiModel.SwitchCharacter(); });
        uiModel.OnDataChanged += UpdateUI;
    }

    public void UpdateUI(UIModel uiModel)
    {
        _playerHealthBar.UpdateHealthBar(uiModel.playerHealth, uiModel.playerMaxHealth);
        _enemyHealthBar.UpdateHealthBar(uiModel.EnemyHealth, uiModel.EnemyMaxHealth);
    }
}