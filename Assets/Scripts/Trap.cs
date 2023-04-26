using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    [SerializeField] private int damage;
    [SerializeField] private float delay;
    private bool takingDamage = false;
    private void OnTriggerStay(Collider other) {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (!playerHealth) return;
        if (takingDamage) return;

        takingDamage = true;
        StartCoroutine(TakeDamage(playerHealth, damage, delay));
    }

    IEnumerator TakeDamage(PlayerHealth _playerHealth, int _damage, float _delay) {
        _playerHealth.DecreaseHealth(_damage);

        yield return new WaitForSeconds(_delay);
        takingDamage = false;
    }
}