using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipName
{
    public class PlayerShip : Ship
    {
        #region joystick
        [SerializeField] protected Joystick joystick;
        [SerializeField] protected JoyButton joyButton;
        [SerializeField] protected JoyButton joyButton2;
        private float joyHorizontal;
        private float joyVertical;
        protected bool jump;
        #endregion
        float pointrot = 0f;
        [SerializeField] bool MouseHeel=true;
        [SerializeField] float speed = 0.5f;
        [SerializeField] Fin Finish;
        private float pointx;
        [SerializeField]LayerMask layerMask;
        public float AmountInt(float amount)
        {
            return amount;
        }
        Vector2 _moveDirection;
        Vector2 _mouseDirection;
        private Vector3 inputRotation;
        private Vector3 mousePlacement;
        private Vector3 screenCentre;
        void FindCrap()
        {
            screenCentre = new Vector3(Screen.width * 0.5f, 0, Screen.height * 0.5f);
            mousePlacement = Input.mousePosition;
            mousePlacement.z = mousePlacement.y;
            mousePlacement.y = 0;
            inputRotation = mousePlacement - screenCentre;
        }
        float yrot = 0;
        float rotationHorizontal;
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            //movementSystem.LateralMovement(1 * Time.deltaTime);
            _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            pointrot += Input.GetAxis("Horizontal") * Time.deltaTime * -100;
            
            if (MouseHeel)
            {
                joyHorizontal = joystick.Horizontal;
                joyVertical = joystick.Vertical * 100;
                rotationHorizontal += joyHorizontal*Time.deltaTime*100;
                movementSystem.LateralRotate(rotationHorizontal);
                //transform.rotation = Quaternion.Euler(0, 0, joyHorizontal);
                if (joyButton.Pressed)
                {
                    movementSystem.LongMovement(-1 * Time.deltaTime);
                    //jump = true;
                }
                if (joyButton2.Pressed)
                {
                    movementSystem.LongMovement(1 * Time.deltaTime);
                    //jump = false;
                }
            }
            else { movementSystem.LateralRotate(pointrot); }
            movementSystem.LongMovement(_moveDirection.y * Time.deltaTime);
            //transform.Translate(Vector3.right*speed);
            if (
               transform.position.x > GetComponent<CollShip>().limitx)
            {
                //print("x");
                transform.position = new Vector3(GetComponent<CollShip>().limitx - 2, transform.position.y, transform.position.z);
            }

            if (transform.position.x < GetComponent<CollShip>().limitx1
               )
            {
                //print("x1");
                transform.position = new Vector3(GetComponent<CollShip>().limitx1 + 2, transform.position.y, transform.position.z);
            }

        }
        
        private void Update()
        {
            PlayerHandle();
        }
        Vector3 awakePosition;
        private void Awake()
        {
            awakePosition = transform.position;
        }

        protected override void ProcessFire(WeaponSystem fireSystem)
        {
            if (Input.GetKey(KeyCode.Space)|| Input.GetMouseButton(0))
            {
                fireSystem.TriggerFire();
                var source = GetComponent<AudioSource>();
                if(source != null) source.PlayOneShot(source.clip);
            }
            if (Input.GetMouseButtonDown(0))
            {
                MouseHeel = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                MouseHeel = false;
            }
        }
       
    }
}
