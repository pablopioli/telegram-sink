# A Telegram sink for Serilog

One day needed a Telegran sink to use with Serilog. I found two existing projects.

https://github.com/oxozle/serilog-sinks-telegram

https://github.com/carlozamagni/serilog-sinks-telegram

But they didn't offered all the features I needed. I was looking for zero external dependencies and a the capability to use an output template.

So I ended building a new one, wich is this one. That doesn't mean that the other projects aren't good. Please check them out too if you need a Telegram sink.

The code for the output template was copied almost entirely from https://github.com/serilog/serilog-sinks-console. So the credit for this part goes to the Serilog contributors.

# How to use it

* Install the ppioli.Telegram.Sink package

* Configure the sink:

```csharp
new LoggerConfiguration()
.WriteTo.TelegramSink(
 telegramApiKey: "theapikey",
 telegramChatId: "thechatid"
);
```
You can pass also an output template, the default template is

```csharp
[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message}
```

You can get the api key using the BotFather https://telegram.me/botfather

To get the chat id follow the instructions stated in https://stackoverflow.com/questions/32423837/telegram-bot-how-to-get-a-group-chat-id
