using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorList : MonoBehaviour
{
    [Header("Node")]
    [SerializeField] private List<BehaviorNode> nodes = new List<BehaviorNode>();
    private BehaviorNode NowBehavior = null;
    [Header("Property")]
    [SerializeField] private PlayerState state;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NowBehavior == null)
        {
            if(nodes.Count == 0)
            {
                Debug.Log("Node Empty!");
                NowBehavior = null;
                return;
            }
            for(int i=0;i<nodes.Count;i++)
            {
                if (nodes[i].OnUapdate(state, rigidbody, Player))
                {
                    NowBehavior = nodes[i];
                    return;
                }
            }
        }
        else
        {
            if (NowBehavior != null)
                Debug.Log(NowBehavior.ToString());
            if (!NowBehavior.OnUapdate(state, rigidbody, Player))
            {
                NowBehavior = null;
                return;
            }
            
        }
    }
}
