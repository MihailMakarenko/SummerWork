using Telegram.Bot;
using Telegram.Bot.Types;

namespace SummerWorkProject
{
    internal class WorkWithTelegram
    {
        public WorkWithTelegram()
        {
            client.StartReceiving(HandleUpdate, HandleError);
        }

        private static string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ChatId.txt");
        private static TelegramBotClient client = new TelegramBotClient("7132306822:AAG9dWxAPOFZYu-MbUTVE1LGGAovh0tH8yc");
        private static long chatId;

        public static long ChatId
        {
            get
            {
                if(chatId == 0)
                {
                    CreateFile.CreateFileMethod(_filePath);
                    var valueChatId = System.IO.File.ReadAllText(_filePath);

                    if (valueChatId == "" || valueChatId == null)
                    {
                        chatId = 0;
                    }
                    else
                    {
                    chatId = long.Parse(System.IO.File.ReadAllText(_filePath));
                    }
                }
                return chatId;
            }
            set {
                chatId = value;
                CreateFile.CreateFileMethod(_filePath);
                System.IO.File.WriteAllText(_filePath, chatId.ToString());
            }
        }

        private async Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            // НЕОБХОДИМО ДОДЕЛАТЬ
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод обрабатывает поступившие от пользователя сообщения
        /// </summary>
        /// <param name="client"></param>
        /// <param name="update"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message.Text != null)
            {
                if (message.Text.ToLower() == "id")
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"{message.Chat.Id}");
                }
            }
        }


        /// <summary>
        /// Отправляет файл в телеграмм
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns>В случае удачной отправки возвращает true если файл не был отправлен возвращет false</returns>
        async public static Task<bool> TelegramSendFile(string pathFile)
        {
            CreateFile.CreateFileMethod(pathFile);
            using (var fileStream = System.IO.File.OpenRead(pathFile))
            {
                try
                {

                await client.SendDocumentAsync(
                    chatId: ChatId,
                    document: new InputFileStream(fileStream, pathFile)
                );
                }
                catch
                {
                    return false;
                }
            }
            return true;

        }

    }
}
