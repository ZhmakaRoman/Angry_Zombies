using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField] 
    private AudioClip _deathSound;
    [SerializeField]
    private UnityEvent _playerDied;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
       //collision.relativeVelocity.y - это свойство, предоставляющее значение скорости столкновения тел вдоль оси Y.
        if (collision.gameObject.tag == GlobalConstants.SKULL_TAG || collision.relativeVelocity.y > 1 )
        {
            _playerDied.Invoke();
            Die();
        }
    }
    

    private void Die()
    {
        // Создаем эффект "взрыв" на месте убитого зомби.
        CreateExplosion();
        // ПРоигрываем звук смерти зомби.
        PlayDeathSound();
        // Разрушаем объект зомби.
        Destroy(gameObject);
            // EnemyDamage();
        
    }
    
    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
    
    private void CreateExplosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
    
}