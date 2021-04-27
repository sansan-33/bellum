using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PreviewUnit : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    Vector3 mPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag ");
        mPos = Input.touchCount > 0 ? Input.GetTouch(0).position : Mouse.current.position.ReadValue();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"On Drag ");
        mPosDelta = mPos - mPrevPos;
        transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mPrevPos = mPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            mPos = Input.touchCount > 0 ? Input.GetTouch(0).position : Mouse.current.position.ReadValue();
            mPosDelta = mPos - mPrevPos;
            transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
        }
        mPrevPos = mPos;
    }
}
