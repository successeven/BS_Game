using BSGame.Model;

namespace BSGame.View
{
    public class ViewWithModel<T> : BaseView where T : BaseModel
    {
        public T Model { get; private set; }

        public virtual BaseView SetModel(T model)
        {
            Model = model;

            return this;
        }
    }
}