using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private Image healthBar;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image hydrationBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI hydrationText;
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

        healthText.text = currentHealth.ToString();
        hungerText.text = currentHunger.ToString();
        hydrationText.text = currentHydration.ToString();
        
        healthBar.fillAmount = 100;
        hungerBar.fillAmount = 100;
        hydrationBar.fillAmount = 100;
    }

    public void IncreaseHealth(int point) {
        currentHealth = Increase(0, maxHealth, currentHealth, point, healthBar, healthText);
    }

    public void IncreaseHunger(int point) {
        currentHunger = Increase(0, maxHunger, currentHunger, point, hungerBar, hungerText);
    }
    
    public void IncreaseHydration(int point) {
        currentHydration = Increase(0, maxHydration, currentHydration, point, hydrationBar, hydrationText);
    }
    
    public void DecreaseHealth(int point) {
        currentHealth = Decrease(0, maxHealth, currentHealth, point, healthBar, healthText);
    }
    
    public void DecreaseHunger(int point) {
        currentHunger = Decrease(0, maxHunger, currentHunger, point, hungerBar, hungerText);
    }
    
    public void DecreaseHydration(int point) {
        currentHydration = Decrease(0, maxHydration, currentHydration, point, hydrationBar, hydrationText);
    }

    private int Increase(int min, int max, int current, int val, Image bar, TextMeshProUGUI text) {
        current += val;
        current = Mathf.Clamp(current, min, max);
        bar.fillAmount = Mathf.InverseLerp(min, max, current);
        text.text = current.ToString();
        return current;
    }

    private int Decrease(int min, int max, int current, int val, Image bar, TextMeshProUGUI text) {
        current -= val;
        current = Mathf.Clamp(current, min, max);
        bar.fillAmount = Mathf.InverseLerp(min, max, current);
        text.text = current.ToString();
        return current;
    }


    private void Die() {}
}