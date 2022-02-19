using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using UnityEngine;

namespace Gameplay.ShipName
{
    public abstract class Ship : MonoBehaviour
    {
        
        private ISpaceship _spaceship;

        public void Init(ISpaceship spaceship)
        {
            _spaceship = spaceship;
        }

        private void Start(){

        }
        public void PlayerHandle()
        {
            ProcessHandling(_spaceship.MovementSystem);
            ProcessFire(_spaceship.WeaponSystem);
        }

        protected abstract void ProcessHandling(MovementSystem movementSystem);
        protected abstract void ProcessFire(WeaponSystem fireSystem);
    }
}
