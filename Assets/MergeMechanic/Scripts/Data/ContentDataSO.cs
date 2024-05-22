using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Content", menuName = "ScriptableObjects/Content")]
public class ContentDataSO :  ScriptableObject
{
    public int GetMaxStage { get => InteractObject.Count; }
    public List<InteractiveObject> InteractObject;

    [ContextMenu("Установить стадии")]
    public void SetStage()
    {
        for(int i = 0; i < InteractObject.Count; i++)
        {
            InteractObject[i].Stage = i;
        }
    }
}
