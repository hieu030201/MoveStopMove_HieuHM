﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooler : MonoBehaviour
{
    static int DEFAULT_AMOUNT = 10;

    //pool tong
    public static Dictionary<GameUnit, Pool> poolObjects = new Dictionary<GameUnit, Pool>();

    //tim pool cha cua thang object
    static Dictionary<GameUnit, Pool> poolParents = new Dictionary<GameUnit, Pool>();


    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (!poolObjects.ContainsKey(prefab))
        {
            poolObjects.Add(prefab, new Pool(prefab, amount, parent));
        }
    }
    public static GameUnit Spawn(GameUnit prefab, Vector3 position, Quaternion rotation)
    {
        GameUnit obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].Spawn(position, rotation);
        return obj;
    }
    public static T Spawn<T>(GameUnit prefab, Vector3 position, Quaternion rotation) where T : GameUnit
    {
        GameUnit obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].Spawn(position, rotation);

        return obj as T;
    }
    public static T Spawn<T>(GameUnit prefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : GameUnit
    {
        GameUnit obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].Spawn(position, rotation);
        obj.transform.localScale = scale;

        return obj as T;
    }

    public static void Despawn(GameUnit obj)
    {
        if (poolParents.ContainsKey(obj))
        {
            poolParents[obj].Despawn(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static void CollectAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Collect();
        }
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Release();
        }
    }

    public class Pool
    {
        Queue<GameUnit> pools = new Queue<GameUnit>();
        List<GameUnit> activeObjs = new List<GameUnit>();
        Transform parent;
        GameUnit prefab;

        public Pool(GameUnit prefab, int amount, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < amount; i++)
            {
                GameUnit obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public GameUnit Spawn(Vector3 position, Quaternion rotation)
        {
            GameUnit obj = null;

            if (pools.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
            }
            else
            {
                obj = pools.Dequeue();
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public void Despawn(GameUnit obj)
        {
            if (obj.gameObject.activeInHierarchy)
            {

                activeObjs.Remove(obj);
                pools.Enqueue(obj);
                obj.gameObject.transform.position = Vector3.zero;
                obj.gameObject.SetActive(false);

            }
        }

        public void Collect()
        {
            while (activeObjs.Count > 0)
            {
                Despawn(activeObjs[0]);
            }
        }

        public void Release()
        {
            Collect();

            while (pools.Count > 0)
            {
                GameUnit obj = pools.Dequeue();
                GameObject.DestroyImmediate(obj);
            }
        }

    }
}

//Mục đích của mã này là tối ưu hóa việc truy cập đến thành phần Transform của đối tượng GameUnit. Thay vì truy cập transform trực tiếp mỗi khi cần, mã này lưu trữ tham chiếu vào biến tf và tái sử dụng nó sau khi đã được khởi tạo
public class GameUnit : MonoBehaviour
{
    public Transform tf;

    public Transform Transform
    {
        get
        {
            if (this.tf == null)
            {
                this.tf = transform;
            }

            return tf;
        }
    }
}

