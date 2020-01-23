namespace BSGame.Model
{
    public class BaseComponent<T> : BaseComponent where T : BaseModel
    {
        protected T Model { get; }

        public BaseComponent(T model)
        {
            Model = model;
        }

        public override void Start()
        {
            Enabled = true;
        }
    }

    public class BaseComponent
    {
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                    Activate();
                else
                    Disable();
                
                _enabled = value;
            }
        }

        private bool _enabled = false;

        public virtual void Start()
        {
        }
        
        public virtual void Disable()
        {
        }

        public virtual void Activate()
        {
        }
        public virtual void Update(float deltaTime)
        {
        }
        public virtual void FixedUpdate(float deltaTime)
        {
        }

    }
}