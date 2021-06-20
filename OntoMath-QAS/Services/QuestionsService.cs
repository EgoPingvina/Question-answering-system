using System;
using System.Linq;

using Microsoft.Extensions.Options;

using VDS.RDF;
using VDS.RDF.Query;

using OntoMath_QAS.Ontology;
using OntoMath_QAS.Models.Settings;

namespace OntoMath_QAS.Services
{
    public class QuestionsService
    {
        public Lazy<IOptions<SparqlSettings>> Settings { get; set; } = null!;

        public Lazy<RequestGenerator> Generator { private get; set; } = null!;

        public Lazy<QuestionMap> Map { private get; set; } = null!;

        /// <summary>
        /// Заполняет шаблон ответа результатами SPARQL запроса.
        /// </summary>
        /// <param name="answerTemplate">Шаблон ответа.</param>
        /// <param name="resultSet">Ответ онтологии.</param>
        /// <returns>Возвращает строку, содержащую данные из онтологии в формате человеко-читаемого ответа.</returns>
        private string FillAnswer(string answerTemplate, SparqlResultSet resultSet)
            => string.Format(
                answerTemplate,
                resultSet.Variables.Count() == 1
                    ? ((ILiteralNode)resultSet[0][0]).Value
                    : string.Join(
                        "\n",
                        resultSet.Select(x =>
                            $"{((ILiteralNode)x[0]).Value} (подробнее: {((ILiteralNode)x[1]).Value})")));

        /// <summary>
        /// Обновление карты.
        /// </summary>
        /// <returns>Если прошло успешно-пустая строка, иначе-сообщение об ошибке.</returns>
        public string UpdateMap()
        {
            try
            {
                this.Map.Value.Update();

                return string.Empty;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string GetAnswer(string question)
        {
            var (isFound, query, answerTemplate) = this.Map.Value[question];

            if (!isFound)
            {
                return "Не удалось понять Ваш вопрос, попробуйте переформулировать, пожалуйста.";
            }

            var result = this.Generator.Value.GetSet($"{query} LIMIT {this.Settings.Value.Value.Limit}");

            return this.FillAnswer(answerTemplate, result);
        }
    }
}