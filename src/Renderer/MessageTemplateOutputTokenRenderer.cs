using Serilog.Events;
using System;
using System.IO;

namespace TelegramSink.Renderer
{
    class MessageTemplateOutputTokenRenderer : OutputTemplateTokenRenderer
    {
        private readonly IFormatProvider _formatProvider;

        public MessageTemplateOutputTokenRenderer(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            output.Write(logEvent.RenderMessage(_formatProvider));
        }
    }
}