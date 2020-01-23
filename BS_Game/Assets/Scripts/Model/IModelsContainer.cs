namespace BSGame.Model
{
    public interface IModelsContainer
    {
        void Start();
        void Update(float deltaTime);
        void FixedUpdate(float deltaTime);
        void Clear();
    }
}