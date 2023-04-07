using ShapesGame.Services.Input;

namespace ShapesGame.Factories
{
    public interface IGameFactory
    {
        DesktopInputService CreateDesktopInput();
    }
}