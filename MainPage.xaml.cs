using HighlightTextApp.Services;
using HighlightTextApp.Windows.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using Microsoft.UI.Xaml.Media.Animation;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace HighlightTextApp
{
    public partial class MainPage : ContentPage
    {
        private string _originalText;
        private string _highlightedText;
        private int _highlightedIndex;

        public MainPage()
        {
            InitializeComponent();
            _originalText = TextLabel.Text;
            _highlightedText = _originalText;
            _highlightedIndex = -1;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTextTapped;
            TextLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void OnTextTapped(object sender, System.EventArgs e)
        {
            // Clear previous highlighted word
            if (_highlightedIndex != -1)
            {
                TextLabel.Text = _originalText;
                _highlightedIndex = -1;
            }

            // Get tapped point
            var args = (TappedEventArgs)e;
            Point point =(Point)args.GetPosition(TextLabel);

            // Get the index of the tapped character
            int charIndex = GetCharacterIndex(TextLabel, point);

            if (charIndex == -1) return;

            // Get the word containing the tapped character that was clicked
            string word = GetWordAtIndex(TextLabel.Text, charIndex);

            if (word == null) return;

            // Highlight the word
            _highlightedIndex = TextLabel.Text.IndexOf(word);
            var words = Regex.Matches(TextLabel.Text, @"\b\w+\b");
            //remove Spans if any
            FormattedString ft = TextLabel.FormattedText;

            if(ft is not null)
            {

                foreach(Span sp in  ft.Spans)
                {
                   TextLabel.FormattedText.Spans.Remove(sp);
                }
            }
            Color spantextcolor = Colors.Black;
            string wrd;
            FormattedString formattedString = new FormattedString();
            string SpanSector = TextLabel.Text.Substring(0, _highlightedIndex);
            string SpanSector2 = TextLabel.Text.Substring(_highlightedIndex + word.Length);
            if (SpanSector != "")
            {
                Span span = new();
                
                span.TextColor = TextLabel.TextColor;
                span.FontSize = TextLabel.FontSize;
                span.FontFamily = TextLabel.FontFamily;
                span.Text = SpanSector;
                formattedString.Spans.Add(span);

                Span span1 = new();
               
                span1.FontSize = TextLabel.FontSize;
                span1.FontFamily = TextLabel.FontFamily;
                span1.TextColor = Colors.Blue;
                span1.Text = word;
                formattedString.Spans.Add(span1);

                Span span2 = new();
               
                span2.TextColor = TextLabel.TextColor;
                span2.FontSize = TextLabel.FontSize;
                span2.FontFamily = TextLabel.FontFamily;
                span2.Text = SpanSector2;
                formattedString.Spans.Add(span2);


            }
            else
            {
                Span span = new();
               
               
                span.FontSize = TextLabel.FontSize;
                span.FontFamily = TextLabel.FontFamily;
                span.TextColor = Colors.Blue;
                span.Text = word;
                formattedString.Spans.Add(span);

                Span span2 = new();
               
              
                span2.FontSize = TextLabel.FontSize;
                span2.FontFamily = TextLabel.FontFamily;
                span2.TextColor = TextLabel.TextColor;
                span2.Text = SpanSector2;
                formattedString.Spans.Add(span2);

            }
            
            
      
            TextLabel.FormattedText = formattedString;
            DoSomethingWithTheClickedWord(word);
        }

        private int GetCharacterIndex(Label label, Point point)
        {
          
            var textService = DependencyService.Get<ITextService>();

            return textService.GetCharacterIndex(
                label.Text,
                label.FontFamily,
                label.FontSize,
                point.X,
                point.Y,
                label.Width,
                label.Height);
        }



        private string GetWordAtIndex(string text, int index)
        {
            if (index < 0 || index >= text.Length) return null;

            var words = Regex.Matches(text, @"\b\w+\b");
            var word = words.Cast<Match>().FirstOrDefault(m => m.Index <= index && m.Index + m.Length >= index);

            return word?.Value;
        }

        private void DoSomethingWithTheClickedWord(string clickedword)
        {
            DisplayAlert("Alert", "The clicked word is:[ "+ clickedword+" ]", "OK");
        }
    }
}
