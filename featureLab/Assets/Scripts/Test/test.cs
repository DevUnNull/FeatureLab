using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Vector3 offset;
    bool isDragging;

    private void OnMouseDown()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mouseWorld;
        offset.z = 0;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
        {
            return;
        }
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = mouseWorld + offset;
        mousePos.z = 0;
        transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        isDragging= false;
    }

}
