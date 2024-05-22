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

    public static void Interactive(InteractiveObject thisObj)
    {
        if (thisObj.IsMerging || _isMerge) return;

        if (_objectA == null)
        {
            _objectA = thisObj;
            _objectA.IsMerging = true;
        }
        else if (_objectB == null)
        {
            _objectB = thisObj;
            _objectB.IsMerging = true;
            _isMerge = true;

            if (_objectA.Stage != _objectB.Stage)
            {
                ClearAB();
            }
            else
            {
                Vector3 posObjectA = _objectA.transform.position;
                Vector3 posObjectB = _objectB.transform.position;
                int stage = _objectA.Stage;

                if (stage < GetContent.GetMaxStage - 1)
                {
                    ClearAB();

                    Destroy(_objectA.gameObject);
                    Destroy(_objectB.gameObject);

                    _objectA = null;
                    _objectB = null;

                    SpawnerController.Instance.CreateMerge(GetContent.InteractObject[stage + 1], Vector3.Lerp(posObjectA, posObjectB, 0.5f));
                }
                else
                {
                    ClearAB();
                }
            }
        }

        //if (thisObj.Stage == GameManager.Instance.GetContent.GetMaxStage-1 ||
        //    thisObj.IsMerging)
        //{
        //    thisObj.IsMerging = false;
        //    _objectA.IsMerging = false;
        //    _objectA = null;

        //    _objectB.IsMerging = false;
        //    _objectB = null;
        //    _objectB = null;
        //    Debug.Log($"Объект А и Б очищены. IsMarging снят с {thisObj.name}");
        //    return;
        //}

        //if (_objectA == null)
        //{
        //    _objectA = thisObj;
        //    _objectA.IsMerging = true;
        //    Debug.Log("Назначен объект A");
        //}
        //else if(_objectB == null)
        //{
        //    _objectB = thisObj;
        //    _objectB.IsMerging = true;

        //    Debug.Log("Назначен объект B");

        //    if (_objectA.Stage == _objectB.Stage)
        //    {
        //        int stage = _objectA.Stage;

        //        Vector3 posObjectA = _objectA.transform.position;
        //        Vector3 posObjectB = _objectB.transform.position;

        //        Destroy(_objectA.gameObject);
        //        Destroy(_objectB.gameObject);

        //        _objectA = null;
        //        _objectB = null;

        //        SpawnerController.Instance.CreateMerge(GetContent.InteractObject[stage + 1], Vector3.Lerp(posObjectA, posObjectB, 0.5f));
        //        Debug.Log("Мерджин!");
        //    }
        //    else
        //    {
        //        _objectA.IsMerging = false;
        //        _objectB.IsMerging = false;
        //        _objectA = null;
        //        _objectB = null;

        //        Debug.Log("Отмена мерджина");
        //    }
        //}
        //else
        //{
        //    Debug.Log("Какая-то поломка");
        //}
    }
    private static void ClearAB()
    {
        _objectA.IsMerging = false;
        _objectB.IsMerging = false;
        _objectA = null;
        _objectB = null;

        _isMerge = false;
    }
}
