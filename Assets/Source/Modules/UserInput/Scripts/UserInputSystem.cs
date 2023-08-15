using UnityEngine;

namespace UserInput
{
    public class UserInputSystem : MonoBehaviour
    {
        private const float MIN_AI_HOLD = 0f;
        private const float MAX_AI_HOLD = 2f;
        private const float AI_DETECT_RADIUS = 30f;

        enum InputType
        { 
            PC,
            Mobile,
            TurretAI,
            TankAI
        }
        [SerializeField] private InputType _platform;
        public IUserInput UserInput { get; private set; }
        public void Initialize()
        {
            switch (_platform)
            {
                case InputType.PC:
                    UserInput = new PCInput(Camera.main);
                    break;
                case InputType.TurretAI:
                    UserInput = new TurretAIInput(transform, "Player", MIN_AI_HOLD, MAX_AI_HOLD, AI_DETECT_RADIUS);
                    break;
                case InputType.TankAI:
                    UserInput = new TankAIInput(transform, "Player", MIN_AI_HOLD, MAX_AI_HOLD, AI_DETECT_RADIUS);
                    break;
                case InputType.Mobile:
                    //TODO
                    break;
                default:
                    Debug.LogError("Platform not identified");
                    break;
            }
        }
        private void Update()
        {
            UserInput?.ProcessInput(Time.deltaTime);
        }
    }
}
