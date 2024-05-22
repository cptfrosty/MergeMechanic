using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public int Stage = -1;
    public bool IsCanMove { set => _isCanMove = value; }
    private bool _isCanMove = true;

    private Rigidbody2D _rb2d;
    private Vector3 screenPoint;

    //[HideInInspector]
    public bool IsMerging = false;

    void OnMouseDown()
    {
        if (!_isCanMove) return;
        // Сохраняем позицию мыши в момент нажатия
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseDrag()
    {
        if (!_isCanMove) return;

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        curPosition.x = Mathf.Clamp(curPosition.x, GameManager.Instance.MinMoveX, GameManager.Instance.MaxMoveX);
        curPosition.y = transform.position.y;
        curPosition.z = 0;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        if (!_isCanMove) return;

        GetComponent<Rigidbody2D>().gravityScale = 1;
        SpawnerController.Instance.BaseCreate();
        _isCanMove = false;
    }

    public void GravityEnable()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsMerging) return;
        if (collision.transform.TryGetComponent<InteractiveObject>(out InteractiveObject obj))
        {
            Merge.Interactive(this, obj);
        }
    }
}
