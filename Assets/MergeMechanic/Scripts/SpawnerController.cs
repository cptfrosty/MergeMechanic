using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController Instance;
    public event UnityAction<MergeObject> ObjectCreated;

    private ContentDataSO _content { get => MergeGameManager.Instance.GetContent; }
    private MergeObject _lastSpawnObject;
    private float _timerSpawn = 1f;
    
    private Vector3 _spawnPosition;
    private List<MergeObject> _allInteractiveObjects = new List<MergeObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetSpawnPosition(Vector3 point)
    {
        _spawnPosition = point;
    }

    /// <summary>
    /// Создаёт объект уже из существующих
    /// </summary>
    public void BaseCreate()
    {
        int rnd = Random.Range(0, MergeGameManager.Instance.CurrentMaxStage);
        Create(_content.InteractObject[rnd]);
    }

    public void Create(MergeObject _obj)
    {
        StartCoroutine(IECreate(_obj));
    }

    public void CreateMerge(MergeObject _obj, Vector3 position)
    {
        _lastSpawnObject = Instantiate(_obj);
        _lastSpawnObject.transform.position = position;
        _lastSpawnObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        _lastSpawnObject.IsCanMove = false;

        _allInteractiveObjects.Add(_lastSpawnObject);
        ObjectCreated?.Invoke(_lastSpawnObject);
    }

    private IEnumerator IECreate(MergeObject _obj)
    {
        yield return new WaitForSeconds(_timerSpawn);
        _lastSpawnObject = Instantiate(_obj);
        _lastSpawnObject.transform.position = _spawnPosition;
        _lastSpawnObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        _allInteractiveObjects.Add(_lastSpawnObject);
        ObjectCreated?.Invoke(_lastSpawnObject);
    }

    private void RemoveObject(MergeObject obj)
    {
        _allInteractiveObjects.Remove(obj);
    }

    //private void Start()
    //{
    //    _content = GameManager.Instance.GetContent;
    //}

    //public static void Create()
    //{
    //    Create(0f);
    //}

    //public static void Create(InteractiveObject obj, Vector3 position, float delay)
    //{
    //    Instance.StartCoroutine(CreateObject(obj, position, delay));
    //}

    //public static void Create(Vector3 position, float delay)
    //{
    //    Create(delay);
    //    _lastSpawnObject.transform.position = position;
    //}

    //public static void Create(Vector3 position)
    //{
    //    Create(0f);
    //    _lastSpawnObject.transform.position = position;
    //}

    //public static void Create(float delay)
    //{
    //    Instance.StartCoroutine(CreateObject(delay));
    //}

    //public static IEnumerator CreateObject(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Vector2 spawnPoint = GameManager.Instance.GetSpawnPoint.position;

    //    _lastSpawnObject = Instantiate(
    //        _content.InteractObject[Random.Range(0, _content.GetMaxStage)], 
    //        GameManager.Instance.GetSpawnPoint.position, 
    //        Quaternion.identity
    //        );
    //}

    //public static IEnumerator CreateObject(InteractiveObject obj, Vector3 position, float delay, ref InteractiveObject result)
    //{
    //    yield return new WaitForSeconds(delay);

    //    Vector2 spawnPoint = position;

    //    _lastSpawnObject = Instantiate(
    //        _content.InteractObject[Random.Range(0, _content.GetMaxStage)],
    //        spawnPoint,
    //        Quaternion.identity
    //        );
    //    result = _lastSpawnObject;
    //}
}
