using System;

[Serializable]
public class UIModel
{
    public PlayerController Player;
    public AIController Enemy;
    public Action ShootAction;
    public Action SwitchCharacter;
    public float playerHealth = 0;
    public float playerMaxHealth;

    public float EnemyHealth = 0;
    public float EnemyMaxHealth;

    public event Action<UIModel> OnDataChanged;

    public void UpdatePlayerHealthUI(float currentHealth, float maxHealth)
    {
        playerHealth = currentHealth;
        playerMaxHealth = maxHealth;
        OnDataChanged.Invoke(this);
    }

    public void UpdateEnemyHealthUI(float currentHealth, float MaxHealth)
    {
        EnemyHealth = currentHealth;
        EnemyMaxHealth = MaxHealth;
        OnDataChanged.Invoke(this);
    }
}