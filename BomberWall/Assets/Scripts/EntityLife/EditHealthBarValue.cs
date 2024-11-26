using UnityEngine;
using UnityEngine.UI;

public class EditHealthBarValue : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Health _targetHealth;

    private void Start()
    {
        _targetHealth.OnHealthChange += HealthBarEdit;
        _healthBar.maxValue = _targetHealth.GetMaxHealth();
        _healthBar.value = _targetHealth.GetMaxHealth();
    }

    public void HealthBarEdit(int health)
    {
        _healthBar.value = health;
    }
}