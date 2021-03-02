using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SpaceBum
{
    [RequireComponent(typeof(CinemachineStateDrivenCamera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        GameObject crossHair;

        CharacterBrain Brain;
        InputController Input;
        CinemachineStateDrivenCamera SDC;

        CinemachineFreeLook LiveFreeLook => SDC.LiveChild as CinemachineFreeLook;

        void Awake()
        {
            SDC = GetComponent<CinemachineStateDrivenCamera>();
            Brain = GetComponentInParent<CharacterBrain>();
            Input = GetComponentInParent<InputController>();
        }

        void Update()
        {
            if (Input.HasAxisInput || Input.LookAt != Vector2.zero)
            {
                DisableRecentering(LiveFreeLook);
            }
        }

        void OnEnable()
        {
            Input.recenterCamera -= OnRecenterCamera;
            Input.recenterCamera += OnRecenterCamera;

            Input.onCameraChange -= OnCameraChange;
            Input.onCameraChange += OnCameraChange;
        }

        void OnDisable()
        {
            Input.recenterCamera -= OnRecenterCamera;
            Input.onCameraChange -= OnCameraChange;
        }

        void OnCameraChange()
        {
            crossHair.SetActive(Input.IsStrafing);
        }

        void OnRecenterCamera()
        {
            if (!Input.HasAxisInput)
            {
                EnableRecentering(LiveFreeLook);
            }
        }

        void EnableRecentering(CinemachineFreeLook freeLook)
        {
            if (freeLook != null)
            {
                freeLook.m_RecenterToTargetHeading.m_enabled = true;
                freeLook.m_YAxisRecentering.m_enabled = true;
            }
        }

        void DisableRecentering(CinemachineFreeLook freeLook)
        {
            if (freeLook != null)
            {
                freeLook.m_RecenterToTargetHeading.m_enabled = false;
                freeLook.m_YAxisRecentering.m_enabled = false;
            }
        }
    }
}