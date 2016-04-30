using UnityEngine;
using System.Collections;

namespace UnityLib.Constraints
{
    /// <summary>
    /// Constraints axis of host object to position of target object.
    /// </summary>
    public class PositionConstraint : MonoBehaviour
    {
        public Vector3 targetPosition; // The current position to constraint to, can be set manually if the target Transform is null
        public Transform targetTransform; // Current target transform to constrain to, can be left null for use of provided Vector3

        public bool X, Y, Z;

        private Transform _tr;
        public Transform tr { get { if (!_tr) _tr = GetComponent<Transform>(); return _tr; } }

        void Update()
        {
            if(targetTransform != null)
            {
                targetPosition = targetTransform.position;
            }
            Vector3 pos = tr.position;
            if (X)
            {
                pos.x = targetPosition.x;
            }
            if (Y)
            {
                pos.y = targetPosition.y;
            }
            if (Z)
            {
                pos.z = targetPosition.z;
            }
            tr.position = pos;
        }   
    }
}