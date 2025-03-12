using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EntilandVR.DosCinco.DAM_AJEI.G_Siete
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class s_EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        public Transform target;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            agent.SetDestination(target.transform.position);
        }
    }
}