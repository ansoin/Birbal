using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledCandy;
    public GameObject candyPrefab;
    public int m_AmountToPool = 15;
    public int amountToPool { get { return m_AmountToPool; } }

    // Assign static object pooler to this object to be accessed by game manager
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // Create prefabs, deactivate, store them in a list and set them as children of candy pool
        pooledCandy = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(candyPrefab);
            obj.SetActive(false);
            pooledCandy.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    // Activate prefab from list if inactive, return null if none available
    public GameObject ActivateCandy()
    {
        for (int i = 0; i < pooledCandy.Count;i++)
        {
            // Check for inactive candy
            if (!pooledCandy[i].activeInHierarchy)
            {
                return pooledCandy[i];
            }
        }

        // Otherwise, return null (e.g. no inactive candy available)
        return null;
    }
}