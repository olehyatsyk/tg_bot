using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace tg_bot
{
    public class TgBot
    {
        TelegramBotClient botClient = new TelegramBotClient("6240924865:AAFsC8vKMhesZHDieQGqXwO2DuxfCqDbatY");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions reseiveOptions = new ReceiverOptions { AllowedUpdates = { } };
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, reseiveOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Bot {botMe.Username} started to work");
            Console.ReadKey();
        }

        private Task HandlerError(ITelegramBotClient botclient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Error in telegram bot:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botclient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botclient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botclient, Message message)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть потрібну Вам команду \n Допомога: /help \n Обмін іноземних валют: /keyboard1 \n Обмін криптовалют: /keyboard2  \n Завершення: /end");
                return;
            }
            
           
            else
              if (message.Text == "/keyboard1")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup1 = new
                    (
                      new[]
                      {
                          new KeyboardButton[] { "USD", "EUR", "PLN" },
                          new KeyboardButton[]{"GBP","CZK","TRY"}
                      }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть іноземну валюту:", replyMarkup: replyKeyboardMarkup1);
                return;
            }
            else
            if (message.Text == "TRY")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 TRY(Турецька ліра) = 1.57 UAH");
                return;

            }
            else
            if (message.Text == "CZK")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 CZK(Чеська крона) = 1.67 UAH");
                return;

            }
            else
            if (message.Text == "GBP")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 GBP(Фунт стерлінгів) = 46.1 UAH");
                return;

            }
            else
            if ( message.Text == "USD")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 USD(Долар США) = 36.75 UAH");
                return;

            }
            else
            if (message.Text == "EUR")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 EUR(Євро) = 39.55 UAH");
                return;

            }
            else
            if (message.Text == "PLN")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 PLN(Польська злота) = 8.85 UAH");
                return;
            }
            if (message.Text == "/keyboard2")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup2 = new
                    (
                      new[]
                      {
                          new KeyboardButton[] { "BTC", "ETH", "XLM" },
                          new KeyboardButton[] { "LTC", "DOGE", "BNB"}
                      }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть криптовалюту:", replyMarkup: replyKeyboardMarkup2);
                return;
            }
            else
            if (message.Text == "BNB")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 BNB(Binance Coin) = 9 578.56 UAH");
                return;

            }
            else
            if (message.Text == "DOGE")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 DOGE(Dogecoin) = 2.50 UAH");
                return;

            }
            else
            if (message.Text == "LTC")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 LTC(Litecoin) = 3 249.99 UAH");
                return;

            }
            else
            if (message.Text == "BTC")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 BTC(Bitcoin) = 974 987.25 UAH");
                return;

            }
            else
            if (message.Text == "ETH")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 ETH(Ethereum) = 67 997.85 UAH");
                return;

            }
            else
            if (message.Text == "XLM")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "1 XLM(Stellar) = 3.21 UAH");
                return;
            }
            else
            if (message.Text == "/help")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Привіт {message.Chat.FirstName}, Я бот створений щоб Ви дізналися інформацію про курс валют та криптовалют. \nЯкщо Ви хочете дізнатись ціну іноземних валют, натисніть /keyboard1 \nЯкщо Ви хочете дізнатись ціну криптовалют, натисніть /keyboard2 \nЯкщо Ви завершили, натисніть /end");
                return;

            }
            else
            if (message.Text == "/end")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Прощавай, {message.Chat.FirstName} \nРадий був допомогти \nЯкщо потрібно звертайтесь");
                return;

            }
        }
    }
}
