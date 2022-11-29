using UnityEngine;
using UnityEngine.AI;
using PointAndClick.PlayerInputControl;
using PointAndClick.UI;
using PointAndClick.CollectibleSystem;

namespace PointAndClick.CharacterControl 
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavmeshAgentController : MonoBehaviour, ICollector
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavmeshAgentConfigs _configs;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent += HandleWorldPointClicked;
            UIEvents.OnIncreaseSpeedButtonClicked += IncreaseSpeed;
            UIEvents.OnDecreaseSpeedButtonClicked += DecreaseSpeed;

        }

        private void OnDisable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent -= HandleWorldPointClicked;
            UIEvents.OnIncreaseSpeedButtonClicked -= IncreaseSpeed;
            UIEvents.OnDecreaseSpeedButtonClicked -= DecreaseSpeed;
        }

        private void HandleWorldPointClicked(RaycastHit hit) 
        {
            _agent.destination = hit.point;
        }

        private void IncreaseSpeed() 
        {
            _agent.speed = Mathf.Clamp(_agent.speed + _configs.IncrementAmmount,
                _configs.MinAgentSpeed, _configs.MaxAgentSpeed);
        }

        private void DecreaseSpeed() 
        {
            _agent.speed = Mathf.Clamp(_agent.speed - _configs.IncrementAmmount,
                _configs.MinAgentSpeed, _configs.MaxAgentSpeed);
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            if (_animator == null) 
            {
                return;
            }

            float speed = transform.InverseTransformDirection(_agent.velocity).z;
            _animator.SetFloat("movementSpeed", speed);
        }
    }

}
