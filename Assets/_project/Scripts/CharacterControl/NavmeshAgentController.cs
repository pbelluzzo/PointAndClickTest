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

        private NavMeshAgent _navmeshAgent;

        private void Start()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent += HandleWorldPointClicked;
            UIEvents.OnIncreaseSpeedButtonClicked += IncreaseNavmeshAgentSpeed;
            UIEvents.OnDecreaseSpeedButtonClicked += DecreaseNavmeshAgentSpeed;

        }

        private void OnDisable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent -= HandleWorldPointClicked;
            UIEvents.OnIncreaseSpeedButtonClicked -= IncreaseNavmeshAgentSpeed;
            UIEvents.OnDecreaseSpeedButtonClicked -= DecreaseNavmeshAgentSpeed;
        }

        private void HandleWorldPointClicked(RaycastHit hit) 
        {
            _navmeshAgent.destination = hit.point;
        }

        private void IncreaseNavmeshAgentSpeed() 
        {
            _navmeshAgent.speed = Mathf.Clamp(_navmeshAgent.speed + _configs.IncrementAmmount,
                _configs.MinAgentSpeed, _configs.MaxAgentSpeed);
        }

        private void DecreaseNavmeshAgentSpeed() 
        {
            _navmeshAgent.speed = Mathf.Clamp(_navmeshAgent.speed - _configs.IncrementAmmount,
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

            float speed = transform.InverseTransformDirection(_navmeshAgent.velocity).z;
            _animator.SetFloat("movementSpeed", speed);
        }
    }

}
