using UnityEngine;
using System;
using System.Collections.Generic;

public class ParentConstraint : MonoBehaviour
{
    public UpdateMethod updateMethod;
    public enum UpdateMethod
    {
        update,
        lateUpdate,
        dontUpdate
    }

    public InitMethod initMethod;
    public enum InitMethod
    {
        OnEnable,
        Start,
        Awake
    }

    public Axis alignAxis;

    private Vector3 childLocalPos;
    private Quaternion _childRotOffset;
    private bool initialized;
    public Transform _parent;

    private void Start()
    {
        if (initMethod == InitMethod.Start)
            Init();
    }

    private void Awake()
    {
        if (initMethod == InitMethod.Awake)
            Init();
    }

    private void OnEnable()
    {
        if (initMethod == InitMethod.OnEnable)
            Init();
    }

    void Init()
    {
        if (!_parent)
            return;
        childLocalPos = _parent.InverseTransformPoint(transform.position);


        switch (alignAxis)
        {
            case Axis.x:
                _childRotOffset = Quaternion.FromToRotation(_parent.right, transform.right);
                break;
            case Axis.y:
                _childRotOffset = Quaternion.FromToRotation(_parent.up, transform.up);
                break;
            case Axis.z:
                _childRotOffset = Quaternion.FromToRotation(_parent.forward, transform.forward);
                break;
            default:
                break;
        }

        initialized = true;
    }

    void Update()
    {
        if (!initialized)
        {
            Init();
            return;
        }
        if (updateMethod != UpdateMethod.update)
            return;
        Sync();
    }

    public void Sync()
    {
        transform.position = _parent.TransformPoint(childLocalPos);
        transform.rotation = _parent.rotation * _childRotOffset;
    }

    void LateUpdate()
    {
        if (!initialized)
        {
            Init();
            return;
        }
        if (updateMethod != UpdateMethod.lateUpdate)
            return;

        Sync();
    }
}

