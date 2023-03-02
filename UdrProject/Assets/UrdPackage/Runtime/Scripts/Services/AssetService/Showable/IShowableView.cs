namespace Urd.Asset
{
    public interface IShowableView<TModel>
        where TModel : IShowableModel
    {
        TModel Model { get; }
        void SetModel(TModel model);
    }
}