using Game.Scripts.PcManagers.Pacient;
using Game.Scripts.PcManagers.Player.View;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.Impl.Components
{
    public class Raycaster
    {
        public bool TryGetPatient(out APatientBehaviour patientBehaviour, Transform origin)
        {
            patientBehaviour = null;
            var hit = Physics2D.Raycast(origin.position, Vector2.right, 2.1f, LayerMask.GetMask("Patient"));

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
        
        public bool TryGetZombie(out ZombiePatientBehaviour patientBehaviour, Transform origin)
        {
            patientBehaviour = null;
            var hit = Physics2D.Raycast(origin.position, Vector2.right, 2.1f, LayerMask.GetMask("Patient"));

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
        
        public bool TryGetPlayer(out PlayerView playerView, Transform origin)
        {
            playerView = null;
            var hit = Physics2D.Raycast(origin.position, Vector2.left, 1.5f, LayerMask.GetMask("Player"));

            if (hit.collider == null)
            {
                return false;
            }

            if (hit.collider.TryGetComponent(out playerView))
            {
                return true;
            }

            return false;
        }
        
    }
}