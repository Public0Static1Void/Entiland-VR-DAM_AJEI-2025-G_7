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

        public bool player_hit = false;
        private float timer = 0;

        public float detection_range;

        private Rigidbody target_rigidbody;
        private Quaternion target_rotation_on_hit;

        private s_car player_car;
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
                agent.SetDestination(targets[current_target].position);
            }

            if (Vector3.Distance(transform.position, target.position) < detection_range && !player_hit)
            {
                agent.SetDestination(target.position);
            }

            Vector3 dir = (agent.steeringTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2);

            if (player_hit)
            {
                timer += Time.deltaTime;
                if (timer > 30)
                {
                    player_hit = false;
                    target_rigidbody.useGravity = true;
                    target_rigidbody.transform.rotation = target_rotation_on_hit;
                    target_rigidbody.transform.position += new Vector3(0, 2, 0);
                    timer = 0;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !player_hit)
            {
                Debug.Log("hit");
                Vector3 dir = (transform.position - other.transform.position).normalized;

                if (target_rigidbody == null)
                    target_rigidbody = other.GetComponent<Rigidbody>();
                if (player_car == null)
                    player_car = other.GetComponent<s_car>();

                target_rotation_on_hit = other.transform.rotation;

                player_car.can_move = false;

                target_rigidbody.useGravity = true;
                //target_rigidbody.AddForce(dir * 250);
                Debug.Log("Force added");
                
                player_hit = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, detection_range);
        }
    }
}