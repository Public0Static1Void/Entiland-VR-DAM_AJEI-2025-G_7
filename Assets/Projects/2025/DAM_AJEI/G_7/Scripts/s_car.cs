using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntilandVR.DosCinco.DAM_AJEI.G_Siete
{
    public class s_car : MonoBehaviour
    {
        private Rigidbody rb;

        public float speed;
        public float angle;

        public Transform volante;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            angle = Quaternion.Angle(transform.rotation, volante.rotation);
            


            transform.rotation = Quaternion.Euler(0, angle, 0);
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }
    }
}