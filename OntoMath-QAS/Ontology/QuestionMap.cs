using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using OntoMath_QAS.Models.Mapper;

namespace OntoMath_QAS.Ontology
{
    /// <summary>
    /// Карта преобразований вопросов пользователей в запросы к RDF.
    /// </summary>
    public sealed class QuestionMap
    {
        /// <summary>
        /// Путь до файла с последним состоянием карты преобразований.
        /// </summary>
        private const string path = "Data/MapItems.json";

        /// <summary>
        /// Путь до файла с человеко-читаемым форматированием запросов.
        /// </summary>
        private const string pathToSource = "Data/ReadableMap.txt";

        /// <summary>
        /// Параметры поиска соответсвий.
        /// </summary>
        private readonly RegexOptions options = RegexOptions.IgnoreCase;

        /// <summary>
        /// Хранилище последнего обновлённого состояния карты.
        /// </summary>
        private List<MapItem> storage;

        public (bool isFound, string query, string answerTemplate) this[string answer]
        {
            get
            {
                var item = this.storage.FirstOrDefault(x => x.VariantAnswerPairs.Any(v => Regex.IsMatch(answer, v.Key, options)));

                if (item == default)
                {
                    return (false, string.Empty, string.Empty);
                }

                var pair = item.VariantAnswerPairs.First(x => Regex.IsMatch(answer, x.Key, options));
                var parameter =
                    Regex.Match(answer, pair.Key, options)
                         .Groups[1]
                         .Value;

                return (true, item.ConcreteQuery(parameter), pair.Value);
            }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public QuestionMap()
        {
            this.Update();
        }

        /// <summary>
        /// Обновляет состояние карты.
        /// </summary>
        public void Update()
        {
            this.UpdateMapSource();

            // если нужного файла нет, то выдаём исключение
            if (!File.Exists(path))
            {
                throw new InvalidDataException($"Отсутствует файл карты.");
            }

            var data = File.ReadAllText(path);

            // читаем файл, десериализуем и присваиваем
            this.storage = JsonConvert.DeserializeObject<List<MapItem>>(data);
        }

        /// <summary>
        /// Обновление серилизованного состояния карты из отладного человеко-читаемого файла.
        /// </summary>
        private void UpdateMapSource()
        {
            // если нет изменённого отладочного файла, значит обновлять серилизованное состояние не нужно.
            if (!File.Exists(pathToSource))
            {
                return;
            }

            var updatedState = new List<MapItem>();
            using (var reader = new StreamReader(pathToSource))
            {
                while (!reader.EndOfStream)
                {
                    // обрабатываем варианты вопроса пользователя
                    var line = reader.ReadLine();
                    var variants = line.Split(';').ToList();

                    // соотвествующие варианты ответы
                    line = reader.ReadLine();
                    var answers = line.Split(';').ToList();

                    if (variants.Count != answers.Count)
                    {
                        throw new InvalidOperationException("Количества вариантов вопроса и шаблонов ответа не совпадают.");
                    }

                    // обрабатываем шаблон запроса
                    var query = new StringBuilder();
                    line = reader.ReadLine();
                    while (line != "+" && !reader.EndOfStream)
                    {
                        query.Append($"{line} ");

                        line = reader.ReadLine();
                    }

                    updatedState.Add(
                        new MapItem
                        {
                            VariantAnswerPairs = variants.Select((x, i) => (x, answers[i])).ToDictionary(x => x.x, x => x.Item2),
                            QueryTemplate      = query.ToString()
                        });
                }
            }

            using (var file = File.CreateText(path))
            {
                new JsonSerializer().Serialize(file, updatedState);
            }
        }
    }
}