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

        public List<Transform> targets;
        private int current_target;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, targets[current_target].position) < 1)
            {
                current_target++;
                if (current_target >= targets.Count)
                {
                    current_target = 0;
                }
            }
            
            if (current_target <= targets.Count)
            {
                agent.speed = 4;
                agent.SetDestination(targets[current_target].position);
            }

            if (Vector3.Distance(transform.position, target.position) < 5)
            {
                agent.SetDestination(target.position);
                agent.speed = 4;
            }
        }
    }
}