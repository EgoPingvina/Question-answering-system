using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using OntoMath_QAS.Models.Mapper;

namespace OntoMath_QAS.Ontology
{
    /// <summary>
    /// Карта преобразований вопросов пользователей в запросы к RDF.
    /// </summary>
    internal sealed class QuestionMap
    {
        /// <summary>
        /// Путь до файла с последним состоянием карты преобразований.
        /// </summary>
        private const string path = "MapItems.json";

        /// <summary>
        /// Путь до файла с человеко-читаемым форматированием запросов.
        /// </summary>
        private const string pathToSource = "ReadableMap.txt";

        /// <summary>
        /// Хранилище последнего обновлённого состояния карты.
        /// </summary>
        private List<MapItem> storage;

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
                        query.Append(line);
                    }

                    updatedState.Append(
                        new MapItem
                        {
                            Variants = variants,
                            QueryTemplate = query.ToString()
                        });
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    new JsonSerializer().Serialize(writer, updatedState);
                }
            }
        }
    }
}