namespace HighlightTextApp.Services
{
    public interface ITextService
    {
        int GetCharacterIndex(string text, string fontName, double fontSize, double x, double y, double labelWidth, double labelHeight);
    }
}
