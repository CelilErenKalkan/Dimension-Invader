using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class PoolItem
{
    public string poolName;
    public List<GameObject> prefabs = new List<GameObject>();
    public int amount;
    public bool expandable;
    public PoolItemType poolItemType;
    [HideInInspector] public GameObject parent;
}

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }
    
    public List<PoolItem> pooledItems;
    [HideInInspector] public List<GameObject> poolObjects;
    private GameObject _parentObject;

    /// <summary>
    /// Calls a member from the selected pool
    /// </summary>
    /// <param name="poolItemType"></param>
    /// <returns></returns>
    private GameObject GetFromPool(PoolItemType poolItemType)
    {
        foreach (var item in pooledItems)
        {
            if (item.poolItemType == poolItemType)
            {
                var prefabCount = item.prefabs.Count;
                var prefabIndex = 0;
                
                if (prefabCount > 0)
                { 
                    prefabIndex = Random.Range(0, prefabCount);
                }
                
                foreach (Transform child in item.parent.transform)
                {
                    if (!child.gameObject.activeInHierarchy && child.name == item.prefabs[prefabIndex].name)
                    {
                        return child.gameObject;
                    }
                }
                    
                if (item.expandable)
                {
                    var randomObjectNo = Random.Range(0, item.prefabs.Count);
                    var newItem = Instantiate(item.prefabs[randomObjectNo], item.parent.transform);
                    newItem.name = item.prefabs[randomObjectNo].name;
                    return newItem;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Calls a specified member from the selected pool
    /// </summary>
    /// <param name="poolItemType"></param>
    /// <param name="childIndex"></param>
    /// <returns></returns>
    private GameObject GetFromPool(PoolItemType poolItemType, int childIndex)
    {
        foreach (var item in pooledItems)
        {
            if (item.poolItemType == poolItemType)
            {
                foreach (Transform child in item.parent.transform)
                {
                    if (!child.gameObject.activeInHierarchy && item.prefabs[childIndex].name == child.name)
                    {
                        return child.gameObject;
                    }
                }
                    
                if (item.expandable)
                {
                    var newItem = Instantiate(item.prefabs[childIndex], item.parent.transform);
                    newItem.name = item.prefabs[childIndex].name;
                    return newItem;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Spawns an item from the selected pool
    /// </summary>
    /// <param name="position"></param>
    /// <param name="poolItemType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject SpawnObject(Vector3 position, PoolItemType poolItemType, Transform parent)
    {
        var b = GetFromPool(poolItemType);
        if (b != null)
        {
            if (parent != null) b.transform.SetParent(parent);
            if (position != null) b.transform.position = position;
            b.SetActive(true);
        }

        return b;
    }

    /// <summary>
    /// Spawns an item from the selected pool with specified member
    /// </summary>
    /// <param name="position"></param>
    /// <param name="poolItemType"></param>
    /// <param name="parent"></param>
    /// <param name="childIndex"></param>
    /// <returns></returns>
    public GameObject SpawnObject(Vector3 position, PoolItemType poolItemType, Transform parent, int childIndex)
    {
        var b = GetFromPool(poolItemType, childIndex);
        if (b != null)
        {
            if (parent != null) b.transform.SetParent(parent);
            if (position != null) b.transform.position = position;
            b.SetActive(true);
        }

        return b;
    }
    
    /// <summary>
    /// Spawns an item from the selected pool for a limited time
    /// </summary>
    /// <param name="position"></param>
    /// <param name="poolItemType"></param>
    /// <param name="parent"></param>
    /// <param name="activeTime"></param>
    /// <returns></returns>
    public GameObject SpawnObject(Vector3 position, PoolItemType poolItemType, Transform parent, float activeTime)
    {
        var b = GetFromPool(poolItemType);
        if (b != null)
        {
            if (parent != null) b.transform.SetParent(parent);
            if (position != null) b.transform.position = position;
            b.SetActive(true);
            StartCoroutine(DeactivationTimer());
        }

        return b;

        IEnumerator DeactivationTimer()
        {
            yield return new WaitForSeconds(activeTime);
            DeactivateObject(b, poolItemType);
        }
    }
        
    /// <summary>
    /// Spawns an item from the selected pool with specified member for a limited time
    /// </summary>
    /// <param name="position"></param>
    /// <param name="poolItemType"></param>
    /// <param name="parent"></param>
    /// <param name="childIndex"></param>
    /// <param name="activeTime"></param>
    public GameObject SpawnObject(Vector3 position, PoolItemType poolItemType, Transform parent, int childIndex,
        float activeTime)
    {
        var b = GetFromPool(poolItemType, childIndex);
        if (b != null)
        {
            if (parent != null) b.transform.SetParent(parent);
            if (position != null) b.transform.position = position;
            b.SetActive(true);
            StartCoroutine(DeactivationTimer());
        }

        return b;

        IEnumerator DeactivationTimer()
        {
            yield return new WaitForSeconds(activeTime);
            DeactivateObject(b, poolItemType);
        }
    }

    /// <summary>
    /// Deactivates the given item and returns it to its' pool
    /// </summary>
    /// <param name="member"></param>
    /// <param name="poolItemType"></param>
    /// <returns></returns>
    public void DeactivateObject(GameObject member, PoolItemType poolItemType)
    {
        foreach (var item in pooledItems)
        {
            if (item.poolItemType == poolItemType)
            {
                member.transform.SetParent(item.parent.transform);
                member.transform.position = item.parent.transform.position;
                member.transform.rotation = item.parent.transform.rotation;
                member.SetActive(false);
            }
        }
    }
    
    /// <summary>
    /// Deactivates the given item and returns it to its' pool
    /// </summary>
    /// <param name="member"></param>
    /// <param name="poolItemType"></param>
    /// <returns></returns>
    public void DeactivateObject(GameObject member, PoolItemType poolItemType, float time)
    {
        StartCoroutine(DeactivationTimer());
        IEnumerator DeactivationTimer()
        {
            yield return new WaitForSeconds(time);
            DeactivateObject(member, poolItemType);
        }
    }

    /// <summary>
    /// Randomizes the selected pool
    /// </summary>
    /// <param name="pool"></param>
    /// <returns></returns>
    private void RandomizeSiblings(Transform pool)
    {
        for (var i = 0; i < pool.childCount; i++)
        {
            var random = Random.Range(i, pool.childCount);
            pool.GetChild(random).SetSiblingIndex(i);
        }
    }
        
    /// <summary>
    /// Creates a pool object at runtime
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private void CreatePoolItems(PoolItem item)
    {
        var amount = item.amount / item.prefabs.Count;
        
        if (item.amount % item.prefabs.Count != 0)
        {
            var extension = item.amount - amount * item.prefabs.Count;
            for (var i = 0; i < extension; i++)
            {
                var random = Random.Range(0, item.prefabs.Count);
                var obj = Instantiate(item.prefabs[random], item.parent.transform, true);
                obj.name = item.prefabs[random].name;
                obj.SetActive(false);
            }
        }

        foreach (var itemObject in item.prefabs)
        {
            for (var i = 0; i < amount; i++)
            {
                var obj = Instantiate(itemObject, item.parent.transform, true);
                obj.name = itemObject.name;
                obj.SetActive(false);
            }
        }

        RandomizeSiblings(item.parent.transform);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        _parentObject = new GameObject
        {
            name = "Pools",
            transform =
            {
                position = Vector3.zero
            }
        };
            
        var count = pooledItems.Count;
        for (var i = 0; i < count; i++)
        {
            var name = pooledItems[i].poolName == "" ? pooledItems[i].prefabs[0].name : pooledItems[i].poolName;
            
            var go = new GameObject
            {
                name = name + "Pool",
                transform =
                {
                    position = Vector3.zero,
                    parent = _parentObject.transform
                }
            };

            pooledItems[i].parent = go;
            poolObjects.Add(go);
        }

        foreach (var item in pooledItems)
        {
            CreatePoolItems(item);
        }
    }
        
/*#if UNITY_EDITOR        
    private void OnValidate()
    {
        UpdateEnum();
    }
#endif */

    public void UpdateEnum()
    {
        if (pooledItems.Count <= 0) return;
            
        var enumList = new List<string>();
            
        foreach (var item in pooledItems)
        {
            if (item.prefabs.Count > 0)
            {
                if (item.poolName != "" && !enumList.Contains(item.poolName))
                {
                    enumList.Add(item.poolName);
                }
                else if (!enumList.Contains(item.prefabs[0].name))
                {
                    enumList.Add(item.prefabs[0].name);
                }
            }
        }

        const string filePathAndName = "Assets/PoolItemType.cs";
        
        using ( var streamWriter = new StreamWriter( filePathAndName ) )
        {
            streamWriter.WriteLine( "public enum PoolItemType");
            streamWriter.WriteLine( "{" );
            for (var i = 0; i < enumList.Count; i ++)
            {
                streamWriter.WriteLine( "\t" + enumList[i] + " = " + i + ",");
            }
            streamWriter.WriteLine( "}" );
        }

        for (var i = 0; i < pooledItems.Count; i++)
        {
            pooledItems[i].poolItemType = (PoolItemType)i;
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}

[CustomEditor(typeof(Pool))]
public class PoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Pool pool = (Pool)target;
        DrawDefaultInspector();
        
        if(GUILayout.Button("Save"))
        {
            pool.UpdateEnum();
        }
    }
}