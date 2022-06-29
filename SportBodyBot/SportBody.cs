using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;
using SportBodyBot.Models;
using SportBodyBot.SportClient;

namespace SportBodyBot
{
    public class SportBody
    {
        Dictionary<long, Client> allusers = new Dictionary<long, Client>();
        TelegramBotClient botClient = new TelegramBotClient("5450747507:AAGEfTxsszGegLjuidJhcaJouhpyVgKq7mg");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Bot {botMe.Username} start working");
            Console.ReadKey();
        }
        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Exception in API:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}", _ => exception.ToString()

            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
            else if (update?.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }
        private async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            Client client = null;
            for (int i = 0; i < allusers.Keys.Count; i++)
            {
                if (callbackQuery.Message.Chat.Id == allusers.Keys.ToList()[i])
                {
                    client = allusers[allusers.Keys.ToList()[i]];
                }
                else continue;
            }
            if (client == null)
            {
                client = new Client();
                allusers.Add(callbackQuery.Message.Chat.Id, client);
            }
            if (callbackQuery?.Data == "with" || callbackQuery?.Data == "without")
            {
                client.equip = callbackQuery.Data;
                Console.WriteLine(client.equip);
                if (client.target == true && client.choise == "Choose by target")
                {
                    string[] targets1 = new string[] { "abductors", "abs", "adductors", "biceps", "calves", "cardiovascular system", "delts", "forearms", "glutes", "lats", "levator scapulae", "pectorals", "quads", "serratus anterior", "spine", "traps", "triceps", "upper back" };
                    var keyboardMarkup1 = new InlineKeyboardMarkup(GetInlineKeyboard(targets1));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "choose target:", replyMarkup: keyboardMarkup1);
                    client.target = false;
                    return;
                }
                else if (client.bodypart == true && client.choise == "Choose by body part")
                {
                    string[] bodyparts = new string[] { "back", "cardio", "lower arms", "lower legs", "neck", "shoulders", "upper arms", "upper legs", "waist" };
                    var keyboardMarkup2 = new InlineKeyboardMarkup(GetInlineKeyboard(bodyparts));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "choose body part:", replyMarkup: keyboardMarkup2);
                    client.bodypart = false;
                    return;
                }
            }
            else if (callbackQuery?.Data == "abs")
            {
                var exercises = await GetTargets("abs", client);
                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (callbackQuery?.Data == "cardiovascular system")
            {
                var exercises = await GetTargets("cardiovascular system", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "abductors")
            {
                var exercises = await GetTargets("abductors", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "adductors")
            {
                var exercises = await GetTargets("adductors", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "biceps")
            {
                var exercises = await GetTargets("biceps", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "calves")
            {
                var exercises = await GetTargets("calves", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "delts")
            {
                var exercises = await GetTargets("delts", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "forearms")
            {
                var exercises = await GetTargets("forearms", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "glutes")
            {
                var exercises = await GetTargets("glutes", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "lats")
            {
                var exercises = await GetTargets("lats", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "levator scapulae")
            {
                var exercises = await GetTargets("levator scapulae", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "pectorals")
            {
                var exercises = await GetTargets("pectorals", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "quads")
            {
                var exercises = await GetTargets("quads", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "serratus anterior")
            {
                var exercises = await GetTargets("serratus anterior", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "spine")
            {
                var exercises = await GetTargets("spine", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "traps")
            {
                var exercises = await GetTargets("traps", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "triceps")
            {
                var exercises = await GetTargets("triceps", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "upper back")
            {
                var exercises = await GetTargets("upper back", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            //bodyparts
            else if (callbackQuery?.Data == "back")
            {
                var exercises = await GetBodyParts("back", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "cardio")
            {
                var exercises = await GetBodyParts("cardio", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "lower arms")
            {
                var exercises = await GetBodyParts("lower arms", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "lower legs")
            {
                var exercises = await GetBodyParts("lower legs", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "neck")
            {
                var exercises = await GetBodyParts("neck", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "shoulders")
            {
                var exercises = await GetBodyParts("shoulders", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "upper arms")
            {
                var exercises = await GetBodyParts("upper arms", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");

                }
            }
            else if (callbackQuery?.Data == "upper legs")
            {
                var exercises = await GetBodyParts("upper legs", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (callbackQuery?.Data == "waist")
            {
                var exercises = await GetBodyParts("waist", client);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "These are exerecises for you:", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }

            else if (callbackQuery?.Data == "add exercise")
            {
                var result = await SetFavorites(client.nameofexercise, callbackQuery.Message.Chat.Id);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{result}");
            }
            else if (callbackQuery?.Data == "delete exercise")
            {
                client._listremove = true;
                var exercises = await GetFavorites(callbackQuery.Message.Chat.Id);
                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "choose the name of the exercise you want to delete: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (client._listremove == true)
            {
                client.delexercise = callbackQuery.Data;
                var result = await RemoveFavorites(callbackQuery.Message.Chat.Id, client.delexercise);
                client._listremove = false;
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{result}");
            }
            else if (callbackQuery?.Data == "show list")
            {
                var exercises = await GetFavorites(callbackQuery.Message.Chat.Id);

                if (exercises != null && exercises.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Your list: ", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else
            {
                client.nameofexercise = callbackQuery.Data;
                if (callbackQuery?.Data != null)
                {
                    var gifka = await GetGifByName(callbackQuery.Data);
                    if (gifka != null && gifka.Length > 0)
                    {
                        await botClient.SendDocumentAsync(callbackQuery.Message.Chat.Id, gifka);
                        string[] options = new string[] { "add exercise" };
                        var keyboardMarkup2 = new InlineKeyboardMarkup(GetInlineKeyboard(options));
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "click on the button if you want to add the exercise to the list of favorite exercises ", replyMarkup: keyboardMarkup2);
                        return;
                    }
                }
            }
        }
        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            Client client = null;
            for (int i = 0; i < allusers.Keys.Count; i++)
            {
                if (message.Chat.Id == allusers.Keys.ToList()[i])
                {
                    client = allusers[allusers.Keys.ToList()[i]];
                }
                else continue;
            }
            if (client == null)
            {
                client = new Client();
                allusers.Add(message.Chat.Id, client);
            }

            if (message.Text == "/start")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                    new[]
                    {
                        new KeyboardButton[]{ "Choose by body part", "Choose by target", "Functional training"},
                        new KeyboardButton[]{ "Water", "List of favorite exercises"},
                        new KeyboardButton[]{ "The number of calories burned"},
                    }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Hello! You can choose exercises or water:", replyMarkup: replyKeyboardMarkup);
                return;
            }
            else if (message.Text== "/trainingprogram")
            {
                var exerciseslowerlegs = await GetBodyPartsProgram("lower legs", client);
                var exercisesupperlegs = await GetBodyPartsProgram("upper legs", client);
                var exerciseabs = await GetTargets("abs", client);
                var exercisesback = await GetBodyPartsProgram("back", client);
                var exerciseslowerarms = await GetBodyPartsProgram("lower arms", client);
                var exercisesupperarms = await GetBodyPartsProgram("upper arms", client);
                var exercisescardio = await GetTargets("cardiovascular system", client);

                List<string> list = new List<string>();
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();

                list.AddRange(exerciseslowerlegs);
                list.AddRange(exercisesupperlegs);
                list1.AddRange(exercisesback);
                list1.AddRange(exerciseslowerarms);
                list1.AddRange(exercisesupperarms);

                string[] exercises = list.ToArray();
                string[] exercises1 = list1.ToArray();
                string[] exercises2 = list2.ToArray();

                var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercises));
                var keyboardMarkupwaist = new InlineKeyboardMarkup(GetInlineKeyboard(exerciseabs));
                var keyboardMarkuparms = new InlineKeyboardMarkup(GetInlineKeyboard(exercises1));
                var keyboardMarkupcardio = new InlineKeyboardMarkup(GetInlineKeyboard(exercisescardio));

                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 1 (legs day)🦵: ", replyMarkup: keyboardMarkup);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 2 (abs day)🥵: ", replyMarkup: keyboardMarkupwaist);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 3 (upper body day)💪:", replyMarkup: keyboardMarkuparms);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 4 - day off!!!🧘‍♀️");
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 5 (legs day)🦵: ", replyMarkup: keyboardMarkup);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 6 (abs day)🥵: ", replyMarkup: keyboardMarkupwaist);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Day 7 (cardio day)🏃‍♀️: ", replyMarkup: keyboardMarkupcardio);


            }
            else if (message.Text== "/getexercisebyname")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter the name of the exercise you want to find");
                client._gifshow = true;
                return;
            }
            else if (message.Type == MessageType.Text && client._gifshow == true)
            {
                client.exercisename = message.Text;
                var exercises = await GetGifByName(client.exercisename);
                if (exercises != null && exercises.Length > 0)
                {
                    await botClient.SendDocumentAsync(message.Chat.Id, exercises);
                    client._gifshow = false;
                    return;
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (message.Text == "Choose by body part")
            {
                client.choise = message.Text;
                client.bodypart = true;
                string[] targets1 = new string[] {"with", "without"};
                var keyboardMarkup1 = new InlineKeyboardMarkup(GetInlineKeyboard(targets1));
                await botClient.SendTextMessageAsync(message.Chat.Id, "choose with or without equipment:", replyMarkup: keyboardMarkup1);
                return;
            }
            else if (message.Text == "Choose by target")
            {
                client.choise = message.Text;
                client.target = true;
                string[] targets1 = new string[] { "with", "without" };
                var keyboardMarkup1 = new InlineKeyboardMarkup(GetInlineKeyboard(targets1));
                await botClient.SendTextMessageAsync(message.Chat.Id, "choose with or without equipment:", replyMarkup: keyboardMarkup1);
                return;
            }
            else if (message.Text == "Functional training")
            {
                string[] bodyparts = new string[] { "back", "cardio", "lower arms", "lower legs", "neck", "shoulders", "upper arms", "upper legs", "waist" };
                string[] exercisesArray = new string[] { };
                string exercise;
                for (int i = 0; i < bodyparts.Length; i++)
                {
                    exercise = await GetFunctionalTraining(bodyparts[i]);
                    //Console.WriteLine(exercise);
                    exercisesArray = exercisesArray.Concat(new[] { exercise }).ToArray();
                }
                if (exercisesArray != null && exercisesArray.Length > 0)
                {
                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(exercisesArray));
                    await botClient.SendTextMessageAsync(message.Chat.Id, "These are exerecises for you:", replyMarkup: keyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (message.Text == "The number of calories burned")
            {
                client.calories = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter how long you have been training in minutes");
            }
            else if (message.Type == MessageType.Text && client.calories == true)
            {
                client.time = message.Text;
                if (int.TryParse(client.time, out int number))
                {
                    bool result = int.TryParse(client.time, out int time1);
                    int burnedCaloriescardio = time1 * 5;
                    int burnedCalories = time1 * 2;
                    client.calories = false;
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"If you did cardio, you spent: {burnedCaloriescardio} calories\notherwise you spent about {burnedCalories} calories:)");
                    return;
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, there is no options for you :(");
                }
            }
            else if (message.Text== "List of favorite exercises")
            {
                string[] options = new string[] { "show list", "delete exercise"};
                var keyboardMarkup2 = new InlineKeyboardMarkup(GetInlineKeyboard(options));
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose what interests you: ", replyMarkup:keyboardMarkup2);
            }
            else if (message.Text == "Water")
            {
                client._water = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter your weight: ");
                return;
            }
            else if (message.Type == MessageType.Text && client._water == true)
            {
                client.weight = message.Text;
                if (int.TryParse(client.weight, out int number))
                {
                    bool result = int.TryParse(client.weight, out int weight1);
                    int water = weight1 * 30;
                    client.water1 = water;
                    client._water = false;
                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                        new[]
                        {
                        new KeyboardButton[]{ "Choose by body part", "Choose by target", "Functional training" },
                        new KeyboardButton[]{ "Tracker of water", "List of favorite exercises" },
                        new KeyboardButton[]{ "The number of calories burned"},
                        }
                        )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Your water rate per day: {water} ml", replyMarkup: replyKeyboardMarkup);
                    return;
                }
                else
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                        new[]
                        {
                        new KeyboardButton[]{ "Choose by body part", "Choose by target", "Functional training" },
                        new KeyboardButton[]{ "Water", "List of favorite exercises" },
                        new KeyboardButton[]{ "The number of calories burned"},
                        }
                        )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, there is no options for you :(");
                    return;
                }
            }
            else if (message.Text == "Tracker of water")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter how much water you have already drunk: ");
                client._drunkWater = true;
                return;
            }
            else if (message.Type == MessageType.Text && client._drunkWater == true)
            {
                client.count = message.Text;
                bool result = int.TryParse(client.count, out int count1);
                client.total = client.water1 - count1;
                if (client.total > 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"You have to drink: {client.total} ml");
                }
                else if (client.total == 0)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                    new[]
                    {
                        new KeyboardButton[]{ "Choose by body part", "Choose by target", "Functional training"},
                        new KeyboardButton[]{ "Water", "List of favorite exercises"},
                        new KeyboardButton[]{ "The number of calories burned"},
                    }
                    )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Congratulations! You drank your amount of water:)", replyMarkup: replyKeyboardMarkup);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "You have already drunk your norm of water");
                }
                client._drunkWater = false;
                client.water1 = client.total;
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, there is no options for you :(");
                return;
            }
        }
        private static async Task<string[]> GetTargets(string target, Client client)
        {
            HttpClient _client = new HttpClient();
            const string _address = "https://sportexercisesapi.azurewebsites.net/Target";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"{_address}?Target={target}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var exerciseresponse = JsonConvert.DeserializeObject<List<SportModel>>(body);
                Random random = new Random();
                var data = exerciseresponse.OrderBy(x => random.Next()).Take(10);
                string[] exercisesNames = new string[] { };
                if (client.equip == "without")
                {
                    foreach (var exercise in data)
                    {
                        if (exercise.equipment == "body weight" )
                        {
                            exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                        }
                    }
                }
                else if (client.equip == "with")
                {
                    foreach (var exercise in data)
                    {
                        if (exercise.equipment != "body weight")
                        {
                            exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                        }
                    }
                }
                else
                {
                    foreach (var exercise in data)
                    {
                        exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                    }
                }
                return exercisesNames;
            };
        }
        private static async Task<string[]> GetBodyParts(string bodypart, Client client)
        {
            HttpClient _client = new HttpClient();
            const string _address = "https://sportexercisesapi.azurewebsites.net/BodyPart";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"{_address}?BodyPart={bodypart}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var exerciseresponse = JsonConvert.DeserializeObject<List<SportModel>>(body);
                Random random = new Random();
                var data = exerciseresponse.OrderBy(x => random.Next()).Take(10);
                string[] exercisesNames = new string[] { };
                if (client.equip == "without")
                {
                    foreach (var exercise in data)
                    {
                        if(exercise.equipment == "body weight")
                        {
                            exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                        }
                    }
                }
                else if (client.equip == "with")
                {
                    foreach (var exercise in data)
                    {
                        if (exercise.equipment != "body weight")
                        {
                            exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                        }
                    }
                }
                else
                {
                    foreach (var exercise in data)
                    {
                        exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                    }
                }
                return exercisesNames;
            };
        }
        private static async Task<string[]> GetBodyPartsProgram(string bodypart, Client client)
        {
            HttpClient _client = new HttpClient();
            const string _address = "https://sportexercisesapi.azurewebsites.net/BodyPart";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"{_address}?BodyPart={bodypart}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var exerciseresponse = JsonConvert.DeserializeObject<List<SportModel>>(body);
                Random random = new Random();
                var data = exerciseresponse.OrderBy(x => random.Next()).Take(4);
                string[] exercisesNames = new string[] { };
                foreach (var exercise in data)
                {
                    exercisesNames = exercisesNames.Concat(new[] { exercise.name }).ToArray();
                }
                return exercisesNames;
            };
        }
        private static async Task<string> GetGifByName(string name)
        {
            HttpClient _client = new HttpClient();
            const string _address = "https://sportexercisesapi.azurewebsites.net/Name";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"{_address}?Name={name}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var exerciseresponse = JsonConvert.DeserializeObject<List<SportModel>>(body);
                Random random = new Random();
                var data = exerciseresponse.OrderBy(x => random.Next()).Take(1);
                string exercisesgifs = "";
                foreach (var exercise in data)
                {
                    exercisesgifs = exercise.gifUrl;
                }
                return exercisesgifs;
            };
        }
        private static async Task<string> SetFavorites(string name, long userid)
        {
            FavoriteExercises setFavouriets = new FavoriteExercises();
            setFavouriets._name = name;
            setFavouriets._userid = userid;
            var jsonstr = JsonConvert.SerializeObject(setFavouriets);
            var data = new StringContent(jsonstr, Encoding.UTF8, "application/json");
            var url = $"https://sportexercisesapi.azurewebsites.net/Name?name={name}&userid={userid}";
            using var client = new HttpClient();
            var response = await client.PostAsync(url,data);
            string result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return "Added to your list:)";
            }
            else return "Error(";
        }
        private static async Task<string[]>GetFavorites(long userid)
        {
            HttpClient _client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"https://sportexercisesapi.azurewebsites.net/List?userid={userid}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<string[]>(body);
                return result;
            };
        }
        private static async Task<string> RemoveFavorites(long userid, string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sportexercisesapi.azurewebsites.net/");
                var response = await client.DeleteAsync($"Delete?userid={userid}&name={name}");
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
        private static async Task<string> GetFunctionalTraining(string bodypart)
        {
            HttpClient _client = new HttpClient();
            const string _address = "https://sportexercisesapi.azurewebsites.net/BodyPart";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(System.Web.HttpUtility.UrlPathEncode($"{_address}?BodyPart={bodypart}")),
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var exerciseresponse = JsonConvert.DeserializeObject<List<SportModel>>(body);
                Random random = new Random();
                var data = exerciseresponse.OrderBy(x => random.Next()).Take(1);
                //string[] exercisesNames = new string[] { };
                string exercisesname = "";
                foreach (var exercise in data)
                {
                    exercisesname = exercise.name;
                }
                return exercisesname;
            };
        }
        private static InlineKeyboardButton[][] GetInlineKeyboard(string[] stringArray)
            {
                var keyboardInline = new InlineKeyboardButton[stringArray.Length][];
                var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
                for (var i = 0; i < stringArray.Length; i++)
                {

                    keyboardButtons[i] = new InlineKeyboardButton(stringArray[i])
                    {
                        Text = stringArray[i],
                        CallbackData = stringArray[i],
                    };
                }
                for (var j = 1; j <= stringArray.Length; j++)
                {
                    keyboardInline[j - 1] = keyboardButtons.Take(1).ToArray();
                    keyboardButtons = keyboardButtons.Skip(1).ToArray();
                }
                return keyboardInline;
            }
    } 
}