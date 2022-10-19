using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Paint,
    BluePaint,
    Water, 
    Confetti
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject[] prefabs;
    public GameObject container;

    public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    List<PoolInfo> listOfPool;

    private Vector3 defaultPos = new Vector3(0, 0, 0);
    private Quaternion defaultRotPaint = Quaternion.Euler(new Vector3(0, 0, 0));

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listOfPool.Count; i++)
            FillPool(listOfPool[i]);
    }

    void FillPool(PoolInfo info)
    {
        for( int i = 0; i < info.amount; i++)
        {
            GameObject prefab = info.prefabs[UnityEngine.Random.Range(0, info.prefabs.Length)];
            GameObject obInstance = null;
            obInstance = Instantiate(prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);

            // If paint it initialises the object with random scale for each
            if (info.type == PoolObjectType.Paint)
            {
                obInstance.transform.localScale = new Vector3(UnityEngine.Random.Range(3.5f, 5.5f), 1, UnityEngine.Random.Range(3.5f, 5.5f));
            } else if (info.type == PoolObjectType.Water)
            {
                obInstance.transform.localScale = new Vector3(UnityEngine.Random.Range(10.5f, 12.5f), 1, UnityEngine.Random.Range(10.5f, 12.5f));
            }

            obInstance.transform.position = defaultPos;
            info.pool.Add(obInstance);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type, Vector3 pos, Vector3 relativeRot)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        GameObject obInstance = null;
        if(pool.Count >0)
        {
            obInstance = pool[pool.Count - 1];
            pool.Remove(obInstance);
            
            obInstance.transform.rotation = Quaternion.Euler(relativeRot + obInstance.transform.localRotation.eulerAngles);
            obInstance.transform.position = pos;
        }
        else
        {
            GameObject prefab = selected.prefabs[UnityEngine.Random.Range(0, selected.prefabs.Length)];
            obInstance = Instantiate(prefab, pos, Quaternion.Euler(relativeRot + prefab.transform.localRotation.eulerAngles), selected.container.transform);
        }
        return obInstance;
    }

    public void ReleasePoolObject(GameObject ob, PoolObjectType type)
    {
        ob.SetActive(false);
        ob.transform.position = defaultPos;

        if (type == PoolObjectType.Paint)
            ob.transform.rotation = defaultRotPaint;

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        if (!pool.Contains(ob))
            pool.Add(ob);
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if (type == listOfPool[i].type)
                return listOfPool[i];
        }
        return null;
    }
}
