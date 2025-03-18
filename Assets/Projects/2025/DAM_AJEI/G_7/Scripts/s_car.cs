using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntilandVR.DosCinco.DAM_AJEI.G_Siete
{
    public class s_car : MonoBehaviour
    {
        public InputActionProperty palanca_derecha_presionada;
        public InputActionProperty palanca_izquierda_presionada;

        public InputActionProperty palanca_derecha_suelta;
        public InputActionProperty palanca_izquierda_suelta;

        private Rigidbody rb;

        public float speed;
        public float angle;

        public Transform volante;

        public ParticleSystem particles;

        public bool can_move = false;

        private bool adding_speed = false, removing_speed = false;

        private AudioSource audioSource;

        // Sonidos
        public AudioClip car_engine, start_car_engine;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();

            if (palanca_derecha_presionada != null ) { palanca_derecha_presionada.action.Enable(); }
            if (palanca_izquierda_presionada != null ) { palanca_izquierda_presionada.action.Enable(); }

            palanca_derecha_presionada.action.performed += AddSpeed;
            palanca_izquierda_presionada.action.performed += RemoveSpeed;
            
            if (palanca_derecha_suelta != null ) { palanca_derecha_suelta.action.Enable(); }
            if (palanca_izquierda_suelta != null ) { palanca_izquierda_suelta.action.Enable(); }

            palanca_derecha_suelta.action.performed += StopAdding;
            palanca_izquierda_suelta.action.performed += StopRemoving;

            StartStopCar(!can_move);
        }

        private void Update()
        {

            if (adding_speed)
            {
                speed += Time.deltaTime * 4;
            }
            if (removing_speed)
            {
                speed -= Time.deltaTime * 4;
            }

            speed = Mathf.Clamp(speed, -50, 100);
        }
        void FixedUpdate()
        {
            if (!can_move)
            {
                if (speed > 0)
                {
                    speed -= Time.fixedDeltaTime * 2;
                }
                return;
            }

            if (Physics.Raycast(transform.position, Vector2.down, out RaycastHit hit))
            {
                if (hit.distance > 1)
                {
                    can_move = false;
                }
                else
                {
                    can_move = true;
                }
            }

            angle = volante.rotation.z * 90;
            
            transform.rotation = Quaternion.Euler(0, angle, 0);
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }


        public void StartStopCar(bool v)
        {
            can_move = v;
            if (can_move)
            {
                audioSource.loop = false;
                audioSource.clip = start_car_engine;
                audioSource.Play();
                particles.Play();

                StartCoroutine(StartCarMovingSound());
            }
            else
            {
                audioSource.Stop();
                particles.Stop();
            }
        }

        private IEnumerator StartCarMovingSound()
        {
            while (audioSource.isPlaying && audioSource.clip == start_car_engine)
            {
                yield return null;
            }
            audioSource.clip = car_engine;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void AddSpeed(InputAction.CallbackContext con)
        {
            adding_speed = true;
        }
        public void RemoveSpeed(InputAction.CallbackContext con)
        {
            removing_speed = true;
        }

        public void StopAdding(InputAction.CallbackContext con)
        {
            adding_speed = false;
        }
        public void StopRemoving(InputAction.CallbackContext con)
        {
            removing_speed = false;
        }
    }
}