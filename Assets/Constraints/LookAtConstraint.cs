using UnityEngine;
using System.Collections;

public class LookAtConstraint : MonoBehaviour {
    public bool update;
    public Transform target;
	public UpdateMethod updateMethod;
        public enum UpdateMethod
        {
            update,
            lateUpdate,
            dontUpdate
        }
    Transform tr;
    void Awake()
    {
        tr = transform;
    }
	// Update is called once per frame
	void Update () {
        if (updateMethod != UpdateMethod.update)
                return;
        Sync();
	}

    public void Sync()
    {
        tr.LookAt(target);

    }
	 void LateUpdate()
        {
            if (updateMethod != UpdateMethod.lateUpdate)
                return;
            Sync();

        }
}
