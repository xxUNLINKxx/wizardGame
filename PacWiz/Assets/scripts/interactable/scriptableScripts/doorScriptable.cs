using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Door", menuName = "Door")]
public class doorScriptable : ScriptableObject
{
    public bool Locked;
    public bool Sealed;
}
