using Serilog.Events;
using System.IO;

namespace TelegramSink.Renderer
{
    class NewLineTokenRenderer : OutputTemplateTokenRenderer
    {
        public override void Render(LogEvent logEvent, TextWriter output)
        {
            output.WriteLine();
        }
    }
}