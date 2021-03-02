using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SpaceBum
{
    [RequireComponent(typeof(InputController))]
    public class CinemachineInputGainDampener : MonoBehaviour
    {
        [SerializeField]
        float acceleration = 0.1f;

        [SerializeField]
        float deceleration = 0.1f;

        [SerializeField]
        float xAxisMultipler = 5f;

        [SerializeField]
        float yAxisMultipler = 0.05f;

        InputController Input;

        void Awake()
        {
            Input = GetComponent<InputController>();
        }

        void OnEnable()
        {
            CinemachineCore.GetInputAxis += LookInputOverride;
        }

        void OnDisable()
        {
            CinemachineCore.GetInputAxis -= LookInputOverride;
        }

        float LookInputOverride(string axis)
        {
            Debug.Log(axis);
            return 0;
        }
    }
}