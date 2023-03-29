using HighlightTextApp.Services;
using Microsoft.Maui.Controls.Platform;

[assembly: Dependency(typeof(HighlightTextApp.Windows.Services.TextService))]
namespace HighlightTextApp.Windows.Services
{
    public class TextService : ITextService
    {
        public int GetCharacterIndex(string text, string fontName, double fontSize, double x, double y, double labelWidth, double labelHeight)
        {
            int textLength = text.Length;
            double approxCharWidth = labelWidth / textLength;
            int rowIndex = (int)(y / fontSize);
            int columnIndex = (int)(x / approxCharWidth);

            int totalColumns = (int)(labelWidth / approxCharWidth);
            int index = (rowIndex * totalColumns) + columnIndex;

            return index < textLength ? index : -1;
        }
    }
}
