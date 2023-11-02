using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Inst => instance;

    /// <summary>
    /// Pool ����ü
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        /// <summary>
        /// Ǯ�� �̸�
        /// </summary>
        public string name;

        /// <summary>
        /// Ǯ�� ũ��
        /// </summary>
        public int size;

        /// <summary>
        /// Ǯ�� �θ� Ʈ������
        /// </summary>
        public Transform poolParent;

        /// <summary>
        /// Ǯ�� ������Ʈ ������
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// ���� ������Ʈ�� ���� ����Ʈ
        /// </summary>
        public List<GameObject> PooledObjectList;

        /// <summary>
        /// Pool���� �Լ�
        /// </summary>
        public void SetPool()
        {
            PooledObjectList = new List<GameObject>(size);

            for (int i = 0; i < size; i++)
            {
                GameObject PoolObject = Instantiate(prefab, poolParent);
                PoolObject.name = $"{name}_{i}";
                PoolObject.SetActive(false);
                PooledObjectList.Add(PoolObject);
            }
        }

        /// <summary>
        /// Pool Ȯ�� �Լ� (1���� ����)
        /// </summary>
        /// <returns>Ȯ���ϰ� �� Ǯ�� GameObject ��ȯ</returns>
        private GameObject ExtendPoolSize()
        {
            size++;
            GameObject AddObject = Instantiate(prefab, poolParent);
            AddObject.name = $"{name}_{size}";
            AddObject.SetActive(false);

            return AddObject;
        }

        /// <summary>
        /// BallPool���� ��Ȱ��ȭ�� Ball ��������
        /// </summary>
        /// <returns>���� List���� ��Ȱ��ȭ�� �� Ball (null ���Ͻ� Ǯ�� �����ִ� ��Ȱ��ȭ ������Ʈ ����)</returns>
        public GameObject SpawnObject()
        {
            GameObject spawnObject = null;

            foreach (GameObject objecttive in PooledObjectList)
            {
                //����Ʈ ������ Ȱ��ȭ ���� ���� ������Ʈ�� ��ȯ
                if (!objecttive.gameObject.activeSelf)
                {
                    spawnObject = objecttive;
                    return spawnObject;
                }
            }

            //���� Ȱ��ȭ �� �����϶� 
            if (!spawnObject)
            {
                //Ǯ������ Ȯ�� 1���� �׸��� �� ������Ʈ�� spawnObject�� ����
                spawnObject = ExtendPoolSize();
            }

            return spawnObject;
        }
    }

    public List<Pool> pools;

    private void Awake()
    {
        instance = this;
        foreach (Pool pl in pools)
        {
            pl.SetPool();
        }
    }

    /// <summary>
    /// �ܺο��� ������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="spawnIndex"></param>
    /// <param name="spawnPos"></param>
    public void SpawnObject(int spawnIndex, Vector3 spawnPos)
    {
        if (spawnIndex < pools.Count)
        {
            GameObject spawnobj = pools[spawnIndex].SpawnObject();
            spawnobj.gameObject.transform.position = spawnPos;
            spawnobj.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("�������� �ʴ� Index");
        }
    }

}