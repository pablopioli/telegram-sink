using Serilog.Events;
using System.IO;

namespace TelegramSink.Renderer
{
    class TextTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly string _text;

        public TextTokenRenderer(string text)
        {
            _text = text;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            output.Write(_text);
        }
    }
}