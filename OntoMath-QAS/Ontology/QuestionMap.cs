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
        /// Хранилище последнего обновлённого состояния карты.
        /// </summary>
        private List<MapItem> storage;

        public (bool, string) this[string answer]
        {
            get
            {
                var item = this.storage.FirstOrDefault(x => x.Variants.Any(v => Regex.IsMatch(answer, v)));

                if (item == default)
                {
                    return (false, string.Empty);
                }

                var parameter =
                    Regex.Match(answer, item.Variants.First(x => Regex.IsMatch(answer, x)))
                         .Groups[1]
                         .Value;

                return (true, item.ConcreteQuery(parameter));
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

                    // обрабатываем шаблон запроса
                    var query = new StringBuilder();
                    line = reader.ReadLine();
                    while (line != "+")
                    {
                        query.Append($"{line} ");

                        line = reader.ReadLine();
                    }

                    updatedState.Add(
                        new MapItem
                        {
                            Variants      = variants,
                            QueryTemplate = query.ToString()
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