using System.IO;
using Serilog.Events;

namespace TelegramSink.Renderer
{
    abstract class OutputTemplateTokenRenderer
    {
        public abstract void Render(LogEvent logEvent, TextWriter output);
    }
}
