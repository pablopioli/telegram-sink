using Serilog.Events;
using Serilog.Parsing;
using System;
using System.IO;

namespace TelegramSink.Renderer
{
    class EventPropertyTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly PropertyToken _token;
        readonly IFormatProvider _formatProvider;

        public EventPropertyTokenRenderer(PropertyToken token, IFormatProvider formatProvider)
        {
            _token = token;
            _formatProvider = formatProvider;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            // If a property is missing, don't render anything (message templates render the raw token here).
            if (!logEvent.Properties.TryGetValue(_token.PropertyName, out var propertyValue))
            {
                return;
            }

            var writer = _token.Alignment.HasValue ? new StringWriter() : output;

            // If the value is a scalar string, support some additional formats: 'u' for uppercase
            // and 'w' for lowercase.
            if (propertyValue is ScalarValue sv && sv.Value is string literalString)
            {
                var cased = Casing.Format(literalString, _token.Format);
                writer.Write(cased);
            }
            else
            {
                propertyValue.Render(writer, _token.Format, _formatProvider);
            }

            if (_token.Alignment.HasValue)
            {
                var str = writer.ToString();
                output.Write(str);
            }
        }
    }
}