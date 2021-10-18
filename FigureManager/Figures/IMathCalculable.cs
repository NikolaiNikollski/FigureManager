namespace FigureManager.Figures
{
    public interface IMathCalculable
    {
        float Square { get; }

        float Perimeter { get; }

        string GetDescription();
    }
}
