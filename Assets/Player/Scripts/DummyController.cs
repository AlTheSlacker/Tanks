using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(Rigidbody))]

    public class DummyController : MonoBehaviour
    {
        [SerializeField] float turnSpeed = 3f;
        [SerializeField] float straightSpeed = 6f;
        [SerializeField] Vector3 jumpVector = new Vector3(0, 20000, 0);
        [SerializeField] bool allowControlInAir = false;

        [SerializeField] bool jumpNow = false;
        [SerializeField] bool forward = false;
        [SerializeField] bool reverse = false;
        [SerializeField] bool left = false;
        [SerializeField] bool right = false;

        Rigidbody rb;
        int lossofControl = 0;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (lossofControl > 0) lossofControl--;
            ProcessJump();
            if ((IsGrounded() || allowControlInAir) && lossofControl == 0)
            {
                RemovePreviousRotationVel();
                RemovePreviousTranslationVel();
                RotationalControl();
                TranslationalControl();
            }
        }

        void RemovePreviousTranslationVel()
        {
            Vector2 projectedCorrection = new Vector2(rb.velocity.x, rb.velocity.z) * -1;
            Vector2 correction = Vector2.ClampMagnitude(projectedCorrection, straightSpeed);
            Vector3 brake = new Vector3(correction.x, 0, correction.y);
            rb.AddForce(brake, ForceMode.VelocityChange);
        }

        void RemovePreviousRotationVel()
        {
            float correction = Mathf.Clamp(Mathf.Abs(rb.angularVelocity.y), 0, turnSpeed);
            int direction = (int)Mathf.Sign(rb.angularVelocity.y) * -1;
            Vector3 brake = new Vector3(0, correction * direction, 0);
            rb.AddTorque(brake, ForceMode.VelocityChange);
        }

        void RotationalControl()
        {
            if (left) rb.AddTorque(Vector3.up * -turnSpeed, ForceMode.VelocityChange);
            if (right) rb.AddTorque(Vector3.up * turnSpeed, ForceMode.VelocityChange);
        }

        void TranslationalControl()
        {
            Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z);
            if (forward) rb.AddForce(direction * straightSpeed, ForceMode.VelocityChange);
            if (reverse) rb.AddForce(direction * -straightSpeed, ForceMode.VelocityChange);
        }

        void ProcessJump()
        {
            if (jumpNow && IsGrounded())
            {
                rb.AddForce(jumpVector);
                jumpNow = false;
            }
        }

        bool IsGrounded()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _, 1.05f)) return true;
            return false;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player")) lossofControl = 60;
        }

    }
}