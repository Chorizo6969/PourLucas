using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _currentHealth;
    [SerializeField] int _maxHealth;

    [SerializeField] GameObject _Deathcanvas;

    public event Action<int> OnHealthChange;

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        if (_currentHealth <= 0)
        {
            Death();
        }

        OnHealthChange?.Invoke(_currentHealth);
    }

    public void GetHeal(int heal)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + heal, 0, _maxHealth);
        OnHealthChange?.Invoke(_currentHealth);
    }

    public void Death()
    {
        _Deathcanvas.SetActive(true);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}