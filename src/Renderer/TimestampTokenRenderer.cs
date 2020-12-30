using Serilog.Events;
using Serilog.Parsing;
using System;
using System.IO;

namespace TelegramSink.Renderer
{
    class TimestampTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly PropertyToken _token;
        readonly IFormatProvider _formatProvider;

        public TimestampTokenRenderer(PropertyToken token, IFormatProvider formatProvider)
        {
            _token = token;
            _formatProvider = formatProvider;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            // We need access to ScalarValue.Render() to avoid this alloc; just ensures
            // that custom format providers are supported properly.
            var sv = new ScalarValue(logEvent.Timestamp);
            sv.Render(output, _token.Format, _formatProvider);
        }
    }
}
