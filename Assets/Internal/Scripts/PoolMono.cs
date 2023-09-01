using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }

    private List<T> pool;

    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
            this.CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (AutoExpand)
            return this.CreateObject(true);

        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }
}