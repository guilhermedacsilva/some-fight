using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter moveScript;
        private Vector3 move;
        private bool jump;

        
        private void Start()
        {
            moveScript = GetComponent<ThirdPersonCharacter>();
        }
        
        private void FixedUpdate()
        {
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            
            move = v * Vector3.forward + h * Vector3.right;

#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) move *= 0.5f;
#endif

            // pass all parameters to the character control script
            moveScript.Move(move, crouch, jump);
            jump = false;
        }


        /*
        // direction of char based on camera rotation
        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        m_Move = v*m_CamForward + h*m_Cam.right;
        */

    }
}
