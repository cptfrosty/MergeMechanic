using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    private static ContentDataSO GetContent { get => GameManager.Instance.GetContent; }

    private static InteractiveObject _objectA = null;
    private static InteractiveObject _objectB = null;
    private static bool _isMerge = false;


    public static void Init()
    {
        _objectA = null;
        _objectB = null;
    }

    public static void Interactive(InteractiveObject objA, InteractiveObject objB)
    {
        if (objA == null || objB == null ||
            objA.IsMerging || objB.IsMerging) return;
        if (objA.Stage != objB.Stage) return;

        objA.IsMerging = true;
        objB.IsMerging = true;

        Vector3 posObjectA = objA.transform.position;
        Vector3 posObjectB = objB.transform.position;
        int stage = objA.Stage;

        if (stage < GetContent.GetMaxStage - 1)
        {
            Destroy(objA.gameObject);
            Destroy(objB.gameObject);

            SpawnerController.Instance.CreateMerge(GetContent.InteractObject[stage + 1], Vector3.Lerp(posObjectA, posObjectB, 0.5f));
        }
    }
}
