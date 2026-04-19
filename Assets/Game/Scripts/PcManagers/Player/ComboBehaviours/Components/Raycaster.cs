using Game.Scripts.PcManagers.Pacient;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.Impl.Components
{
    public class Raycaster
    {
        public bool TryGetPatient(out APatientBehaviour patientBehaviour, Transform origin)
        {
            patientBehaviour = null;
            var hit = Physics2D.Raycast(origin.position, Vector2.right, 2.1f);

            if (hit.collider == null)
            {
                return false;
            }

            if (hit.collider.TryGetComponent(out patientBehaviour))
            {
                return true;
            }

            return false;
        }
    }
}