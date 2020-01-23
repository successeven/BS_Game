using BSGame.View;

namespace BSGame.Model
{
    public class ModelWithView<T> : BaseModel where T : BaseView
    {
        public T View { get; set; }
    }
}