using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace UnityLib.Constraints
{
    public class RotationConstraint : MonoBehaviour
    {
        /* to keep the rotation on one axis at zero, remove the target transform and enable that axis*/

        public Vector3 targetRotation; // The current rotation to constraint to, can be set manually if the target Transform is null
        public Vector3 resultEuler { get; private set; } // to actually see the rotation since unity doesnt 
        public Quaternion resultQuaternion { get; private set; }
        public Transform targetTransform; // Current target transform to constrain to, can be left null for use of provided Vector3
        public Vector3 offset;
        public float Damp;
        public bool Lerp, X, Y, Z;
        public bool nonLocal;
        public UpdateMethod updateMethod;
        public enum UpdateMethod
        {
            update,
            lateUpdate,
            dontUpdate
        }

        Vector3 initRotation;
        void OnEnable()
        {
            initRotation = transform.rotation.eulerAngles;
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
                targetRotation = targetTransform.rotation.eulerAngles;
            }
            if (Lerp)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, Damp * Time.deltaTime);
                resultEuler = transform.rotation.eulerAngles;
                resultQuaternion = transform.rotation;

                return;
            }

            if (Y && X && Z)
            {
                transform.localRotation = targetTransform.rotation * Quaternion.Euler(offset);
                resultEuler = transform.rotation.eulerAngles;
                resultQuaternion = transform.rotation;
                return;
            }
            Vector3 rot = initRotation;
            if (X)
            {
                rot.x = targetRotation.x + offset.x;
            }
            if (Y)
            {
                rot.y = targetRotation.y + offset.y;
            }
            if (Z)
            {
                rot.z = targetRotation.z + offset.z;
            }
            resultEuler = rot;
            resultQuaternion = Quaternion.Euler(rot);
            transform.rotation = resultQuaternion;
        }
        void LateUpdate()
        {
            if (updateMethod != UpdateMethod.lateUpdate)
                return;

            Sync();
        }
    }
}