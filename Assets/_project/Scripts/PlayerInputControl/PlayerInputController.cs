using UnityEngine;
using UnityEngine.EventSystems;

namespace PointAndClick.PlayerInputControl
{
    public class PlayerInputController : MonoBehaviour
    {
        private float _rayMaxDistance = 100f;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastScreenPoint();
            }
        }

        private void RaycastScreenPoint()
        {
            int pointerId = Application.isMobilePlatform ? 0 : -1;

            if (EventSystem.current.IsPointerOverGameObject(pointerId)) 
            {
                return;
            }

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, _rayMaxDistance))
            {
                PlayerInputEvents.NotifyWorldPointClicked(hit);
            }
        }
    }
}
