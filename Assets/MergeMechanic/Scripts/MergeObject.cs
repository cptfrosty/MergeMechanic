using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObject : MonoBehaviour
{
    public int Stage = -1;
    public bool IsCanMove { set => _isCanMove = value; }
    protected bool _isCanMove = true;

    [HideInInspector]
    public bool IsMerging = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsMerging) return;
        if (collision.transform.TryGetComponent<InteractiveObject>(out InteractiveObject obj))
        {
            Merge.Interactive(this, obj);
        }
    }
}
