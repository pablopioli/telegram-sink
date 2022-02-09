using Serilog.Events;
using Serilog.Parsing;
using TelegramSink.Renderer;

namespace Serilog.Sinks.SystemConsole.Output
{
    class LevelTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly PropertyToken _levelToken;

        public LevelTokenRenderer(PropertyToken levelToken)
        {
            _levelToken = levelToken;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            var moniker = LevelOutputFormat.GetLevelMoniker(logEvent.Level, _levelToken.Format);
            output.Write(moniker);
        }
    }
}