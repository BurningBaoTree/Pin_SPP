using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Inst => instance;

    /// <summary>
    /// Pool 구조체
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        /// <summary>
        /// 풀의 이름
        /// </summary>
        public string name;

        /// <summary>
        /// 풀의 크기
        /// </summary>
        public int size;

        /// <summary>
        /// 풀의 부모 트랜스폼
        /// </summary>
        public Transform poolParent;

        /// <summary>
        /// 풀의 오브젝트 프리펩
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// 게임 오브젝트를 담을 리스트
        /// </summary>
        public List<GameObject> PooledObjectList;

        /// <summary>
        /// Pool생성 함수
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
        /// Pool 확장 함수 (1개씩 증가)
        /// </summary>
        /// <returns>확장하고 난 풀의 GameObject 반환</returns>
        private GameObject ExtendPoolSize()
        {
            size++;
            GameObject AddObject = Instantiate(prefab, poolParent);
            AddObject.name = $"{name}_{size}";
            AddObject.SetActive(false);

            return AddObject;
        }

        /// <summary>
        /// BallPool에서 비활성화된 Ball 가져오기
        /// </summary>
        /// <returns>현재 List에서 비활성화가 된 Ball (null 리턴시 풀에 남아있는 비활성화 오브젝트 없음)</returns>
        public GameObject SpawnObjectin()
        {
            GameObject spawnObject = null;

            foreach (GameObject objecttive in PooledObjectList)
            {
                //리스트 뒤져서 활성화 되지 않은 오브젝트를 반환
                if (!objecttive.gameObject.activeSelf)
                {
                    spawnObject = objecttive;
                    return spawnObject;
                }
            }

            //전부 활성화 된 상태일때 
            if (!spawnObject)
            {
                //풀사이즈 확장 1개만 그리고 그 오브젝트를 spawnObject에 대입
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
    /// 외부에서 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="spawnIndex"></param>
    /// <param name="spawnPos"></param>
    public void SpawnObject(int spawnIndex, Vector3 spawnPos)
    {
        if (spawnIndex < pools.Count)
        {
            GameObject spawnobj = pools[spawnIndex].SpawnObjectin();
            spawnobj.gameObject.transform.position = spawnPos;
            spawnobj.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("존재하지 않는 Index");
        }
    }
    public GameObject SpawnObjectinject(int spawnIndex, Vector3 spawnPos)
    {
        if (spawnIndex < pools.Count)
        {
            GameObject spawnobj = pools[spawnIndex].SpawnObjectin();
            spawnobj.gameObject.transform.position = spawnPos;
            spawnobj.gameObject.SetActive(true);
            return spawnobj;
        }
        else
        {
            Debug.Log("존재하지 않는 Index");
            return null;
        }
    }
}