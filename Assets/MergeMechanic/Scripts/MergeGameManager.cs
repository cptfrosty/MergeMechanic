using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MergeGameManager : MonoBehaviour
{
    public static MergeGameManager Instance;
    public static event UnityAction<int> OpeningStage;
    public Transform GetSpawnPoint { get => _spawnPoint; }
    public ContentDataSO GetContent { get => _content; }

    [SerializeField] private ContentDataSO _content;
    [SerializeField] private Transform _spawnPoint;

    [Header("Перемещение")]
    public float MinMoveX = -5.0f;
    public float MaxMoveX = 5.0f;


    public int CurrentMaxStage { 
        get { return _currentMaxStage; } 
        set { 
            if(value > _currentMaxStage)
            {
                _currentMaxStage = value;
            }
        }
    }
    private int _currentMaxStage = 0;

    private void Awake()
    {
        Instance = this;

        Merge.Init();
        //Spawn.InitContent(_content);
        //Spawn.CreateObjectRnd(Vector3.zero);
    }

    private void Start()
    {
        SpawnerController.Instance.SetSpawnPosition(_spawnPoint.position);
        SpawnerController.Instance.Create(_content.InteractObject[0]);
        SpawnerController.Instance.ObjectCreated += SpawnerController_ObjectCreated;

        _content.SetStage();
    }

    private void SpawnerController_ObjectCreated(InteractiveObject arg0)
    {
        CurrentMaxStage = arg0.Stage;
    }
}
