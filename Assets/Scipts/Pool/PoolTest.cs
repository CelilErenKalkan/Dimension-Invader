using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    private Pool _pool;
    
    // Start is called before the first frame update
    void Start()
    {
        _pool = Pool.Instance;
    }
}
