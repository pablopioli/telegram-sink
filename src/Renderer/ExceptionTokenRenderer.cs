using Serilog.Events;

namespace TelegramSink.Renderer
{
    class ExceptionTokenRenderer : OutputTemplateTokenRenderer
    {
        public override void Render(LogEvent logEvent, TextWriter output)
        {
            if (logEvent.Exception is null)
            {
                return;
            }

            var lines = new StringReader(logEvent.Exception.ToString());

            string? nextLine;
            while ((nextLine = lines.ReadLine()) != null)
            {
                output.WriteLine(nextLine);
            }
        }
    }
}