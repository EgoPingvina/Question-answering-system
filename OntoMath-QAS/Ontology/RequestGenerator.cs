using System;
using System.IO;
using System.Text;

using Microsoft.Extensions.Options;

using VDS.RDF;
using VDS.RDF.Query;

using OntoMath_QAS.Models.Settings;

using Options = VDS.RDF.Options;

namespace OntoMath_QAS.Ontology
{
    /// <summary>
    /// Узел, генерирующий запрос к онтологии и возвращающий ответ на него.
    /// </summary>
    public sealed class RequestGenerator
    {
        /// <summary>
        /// Точка доступа SPARQL.
        /// </summary>
        private readonly SparqlRemoteEndpoint endpoint;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public RequestGenerator(IOptions<SparqlEndpointSettings> settings)
        {
            // устанавливаем соединение с точкой подключения SPARQL некоторой онтологии.
            this.endpoint = new SparqlRemoteEndpoint(new Uri(settings.Value.Uri));

            // отключаем передачу отладочной информации для оптимизации.
            Options.HttpDebugging = false;
        }

        /// <summary>
        /// Получить данные в виде набора(коллекции) результатов(строк таблицы ответа) запроса.
        /// </summary>
        /// <param name="query">Запрос к онтологии на языке SPARQL.</param>
        /// <returns>Результат запроса в виде набора результатов.</returns>
        public SparqlResultSet GetSet(string query)
            => this.endpoint.QueryWithResultSet(query);

        /// <summary>
        /// Получить данные в виде графа.
        /// </summary>
        /// <param name="query">Запрос к онтологии на языке SPARQL.</param>
        /// <returns>Результат запроса в виде графа.</returns>
        public IGraph GetGraph(string query)
        {
            this.endpoint.RdfAcceptHeader = "application/turtle";
            return endpoint.QueryWithResultGraph(query);
        }

        /// <summary>
        /// Получить данные в "сыром" виде, без предобработок и приведения к форматам данных.
        /// </summary>
        /// <param name="query">Запрос к онтологии на языке SPARQL.</param>
        /// <returns>Результат запроса в "сыром" виде.</returns>
        public string GetRaw(string query)
        {
            var result = new StringBuilder();

            using (var response = endpoint.QueryRaw(query))
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        result.AppendLine(
                            reader.ReadLine());
                    }

                    reader.Close();
                }

                response.Close();
            }

            return result.ToString();
        }
    }
}