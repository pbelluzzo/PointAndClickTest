using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointAndClick.CharacterControl 
{
    [CreateAssetMenu(fileName = "NavMeshAgentConfigs", menuName = "Character/NavMeshAgentConfigs")]
    public class NavmeshAgentConfigs : ScriptableObject
    {
        [SerializeField] public float MinAgentSpeed = 2f;
        [SerializeField] public float MaxAgentSpeed = 4f;

        [SerializeField] public float IncrementAmmount = 0.2f;
    }

}
