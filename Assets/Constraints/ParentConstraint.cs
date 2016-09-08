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

    
    private Vector3 childLocalPos;
    
    private Quaternion _childRotOffset;

    
    public Transform _parent;

    void OnEnable()
    {
        childLocalPos = _parent.InverseTransformPoint(transform.position);
        _childRotOffset = Quaternion.FromToRotation(_parent.forward, transform.forward);
    }
    void Update()
    {
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
        if (updateMethod != UpdateMethod.lateUpdate)
            return;

        Sync();
    }


}

