using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxHunger;
    [SerializeField] private int maxHydration;
    private int currentHealth;
    private int currentHunger;
    private int currentHydration;

    private void Start() {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentHydration = maxHydration;
    }

    public void IncreaseHealth(int point) {
        currentHealth = Increase(0, maxHealth, currentHealth, point);
    }

    public void IncreaseHunger(int point) {
        currentHunger = Increase(0, maxHunger, currentHunger, point);
    }
    
    public void IncreaseHydration(int point) {
        currentHydration = Increase(0, maxHydration, currentHydration, point);
    }
    
    public void DecreaseHealth(int point) {
        currentHealth = Decrease(0, maxHealth, currentHealth, point);
    }
    
    public void DecreaseHunger(int point) {
        currentHunger = Decrease(0, maxHunger, currentHunger, point);
    }
    
    public void DecreaseHydration(int point) {
        currentHydration = Decrease(0, maxHydration, currentHydration, point);
    }

    private int Increase(int min, int max, int current, int val) {
        current += val;
        return Mathf.Clamp(current, min, max);
    }

    private int Decrease(int min, int max, int current, int val) {
        current -= val;
        return Mathf.Clamp(current, min, max);
    }


    private void Die() {}
}