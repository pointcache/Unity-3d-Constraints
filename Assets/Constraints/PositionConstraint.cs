using UnityEngine;
using System.Collections;


public class PositionConstraint : MonoBehaviour
{
    public Vector3 targetPosition; // The current position to constraint to, can be set manually if the target Transform is null
    public Transform targetTransform; // Current target transform to constrain to, can be left null for use of provided Vector3
    public Vector3 offset;
    public bool
        X,
        Y,
        Z,
        startOffset,
        localOffset,
        Lerp;
    public float LerpVal;

    public UpdateMethod updateMethod;
    public enum UpdateMethod
    {
        update,
        lateUpdate,
        dontUpdate
    }

    Vector3 cachedoffset;
    private Transform tr;

    void OnEnable()
    {
        tr = GetComponent<Transform>();
        if (targetTransform)
            cachedoffset = targetTransform.position - tr.position;
        else
        {
            startOffset = false;
        }
    }

    void Update()
    {
        if (updateMethod != UpdateMethod.update)
            return;
        Sync();
    }

    public void Sync()
    {
        if (targetTransform)
        {
            Vector3 targetPos = targetTransform.position;
            targetPosition = startOffset ? targetPos - cachedoffset : targetPos;
        }

        Vector3 initpos = tr.position;
        Vector3 pos = initpos;
        Vector3 localoffsetpos = Vector3.zero;
        if (localOffset)
        {
            localoffsetpos = targetTransform.TransformPoint(offset);
        }

        if (X)
        {
            pos.x = localOffset ? localoffsetpos.x : targetPosition.x;
        }
        if (Y)
        {
            pos.y = localOffset ? localoffsetpos.y : targetPosition.y;
        }
        if (Z)
        {
            pos.z = localOffset ? localoffsetpos.z : targetPosition.z;
        }
        Vector3 result = Lerp ? Vector3.Lerp(initpos, pos, LerpVal * Time.deltaTime) : pos;
        if (!localOffset)
            result += offset;
        tr.position = result;
    }

    void LateUpdate()
    {
        if (updateMethod != UpdateMethod.lateUpdate)
            return;
        Sync();

    }
}
