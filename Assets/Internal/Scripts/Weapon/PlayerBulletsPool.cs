using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletsPool : MonoBehaviour
{
    private readonly Dictionary<string, List<GameObject>> bullets = new Dictionary<string, List<GameObject>>();

    public void AddBullets(GameObject bulletPrefab, int count)
    {
        if (bullets.ContainsKey(bulletPrefab.name) == false)
            bullets.Add(bulletPrefab.name, new List<GameObject>());

        for (int i = 0; i < count; i++)
        {
            //var bullet = Instantiate(bulletPrefab, transform);
            Create(bulletPrefab);
        }
    }

    private GameObject Create(GameObject bulletPrefab)
    {
        var bullet = Instantiate(bulletPrefab, transform);
        bullet.SetActive(false);
        bullets[bulletPrefab.name].Add(bullet);

        return bullet;
    }

    public GameObject GetBullet(GameObject bulletPrefab)
    {
        if (bullets.ContainsKey(bulletPrefab.name))
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[bulletPrefab.name][i].activeInHierarchy)
                    return bullets[bulletPrefab.name][i];
            }
            return Create(bulletPrefab);
        }
        else
            bullets.Add(bulletPrefab.name, new List<GameObject>());

        return Create(bulletPrefab);
    }
}
