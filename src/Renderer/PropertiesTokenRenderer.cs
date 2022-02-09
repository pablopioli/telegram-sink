using Serilog.Events;
using Serilog.Parsing;

namespace TelegramSink.Renderer
{
    class PropertiesTokenRenderer : OutputTemplateTokenRenderer
    {
        private readonly MessageTemplate _outputTemplate;
        private readonly IFormatProvider? _formatProvider;

        public PropertiesTokenRenderer(MessageTemplate outputTemplate, IFormatProvider? formatProvider)
        {
            _outputTemplate = outputTemplate;
            _formatProvider = formatProvider;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            var included = logEvent.Properties
                .Where(p => !TemplateContainsPropertyName(logEvent.MessageTemplate, p.Key) &&
                            !TemplateContainsPropertyName(_outputTemplate, p.Key))
                .Select(p => new LogEventProperty(p.Key, p.Value));

            var value = new StructureValue(included);
            value.Render(output, formatProvider: _formatProvider);
        }

        static bool TemplateContainsPropertyName(MessageTemplate template, string propertyName)
        {
            foreach (var token in template.Tokens)
            {
                if (token is PropertyToken namedProperty &&
                    namedProperty.PropertyName == propertyName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}