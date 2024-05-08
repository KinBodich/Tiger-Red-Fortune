using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class ResourceItem : GameItem
    {
        [field: SerializeField] public int ResourceCount { get; private set; }
    }
}