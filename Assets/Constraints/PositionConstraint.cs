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
        private Transform _tr;

        public Transform tr { get { if (!_tr) _tr = GetComponent<Transform>(); return _tr; } }

        void OnEnable()
        {
            cachedoffset = targetTransform.position - tr.position;
        }

        void Update()
        {
            if (updateMethod != UpdateMethod.update)
                return;
            Sync();
        }

        public void Sync()
        {
            if (targetTransform != null)
            {
                targetPosition = startOffset ? targetTransform.position - cachedoffset : targetTransform.position;
            }
            
            Vector3 pos = tr.position;
            Vector3 localoffsetpos = Vector3.zero;
            if(localOffset)
            {
                localoffsetpos = targetTransform.TransformPoint(offset);
            }
            
            if (X)
            {
                pos.x = localOffset ?  localoffsetpos.x : targetPosition.x ;
            }
            if (Y)
            {
                pos.y = localOffset ? localoffsetpos.y : targetPosition.y ;
            }
            if (Z)
            {
                pos.z = localOffset ? localoffsetpos.z : targetPosition.z ;
            }

                tr.position = Lerp ? Vector3.Lerp(tr.position, pos, LerpVal) : pos;
            if(!localOffset)
                tr.position += offset;

        }

        void LateUpdate()
        {
            if (updateMethod != UpdateMethod.lateUpdate)
                return;
            Sync();

        }
    }
