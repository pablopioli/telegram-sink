using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System.Text;
using System.Text.Json;
using TelegramSink.Renderer;

namespace TelegramSink
{
    public class TelegramSink : ILogEventSink
    {
        private readonly LogEventLevel _minimumLevel;
        private readonly OutputTemplateRenderer _formatter;
        private readonly string _apiKey;
        private readonly string _chatId;

        private const string TelegramApiBaseUrl = "https://api.telegram.org";
        private const string DefaultConsoleOutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message}";

        private static readonly HttpClient HttpClient = new();

        public TelegramSink(
            string apiKey,
            string chatId,
            LogEventLevel minimumLevel = LogEventLevel.Warning,
            IFormatProvider? formatProvider = null,
            string outputTemplate = DefaultConsoleOutputTemplate)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            if (string.IsNullOrEmpty(chatId))
            {
                throw new ArgumentNullException(nameof(chatId));
            }

            if (outputTemplate is null)
            {
                throw new ArgumentNullException(nameof(outputTemplate));
            }

            _minimumLevel = minimumLevel;
            _formatter = new OutputTemplateRenderer(outputTemplate, formatProvider);
            _apiKey = apiKey;
            _chatId = chatId;
        }

        public async void Emit(LogEvent logEvent)
        {
            try
            {
                if (logEvent.Level < _minimumLevel)
                {
                    return;
                }

                var writer = new StringWriter();
                _formatter.Format(logEvent, writer);
                var message = writer.ToString();

                var jsonString = JsonSerializer.Serialize(new
                {
                    chat_id = _chatId,
                    text = message
                });

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var url = TelegramApiBaseUrl + $"/bot{_apiKey}/sendMessage";
                await HttpClient.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.ToString());
            }
        }
    }
}
