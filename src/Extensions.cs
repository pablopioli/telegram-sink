using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace TelegramSink
{
    public static class Extensions
    {
        public static LoggerConfiguration TelegramSink(
            this LoggerSinkConfiguration config,
            string telegramApiKey,
            string telegramChatId,
            IFormatProvider formatProvider = null,
            LogEventLevel minimumLevel = LogEventLevel.Verbose)
        {
            return config.Sink(new TelegramSink(telegramApiKey, telegramChatId, minimumLevel, formatProvider));
        }
    }
}