using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParticle : MonoBehaviour
{
    private void Update()
    {
        Destroy(this.gameObject,1.2f);
    }
}
