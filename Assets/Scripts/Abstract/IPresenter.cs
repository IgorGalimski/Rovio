public interface IPresenter<in T> where T : IView
{
    void Inject(T view);
}