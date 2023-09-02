using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts.Manager_Controller
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemies;
        public event Action OnEnemiesAreNull;

        public void AddEnemy(GameObject enemy) => _enemies.Add(enemy);

        public void RemoveEnemy()
        {
            _enemies.Remove(_enemies[_enemies.Count - 1]);
            if (_enemies.Count <= 0)
                OnEnemiesAreNull?.Invoke();
        }


    }
}