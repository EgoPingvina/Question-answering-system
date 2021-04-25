using System.Collections.Generic;

namespace OntoMath_QAS.Models.Mapper
{
    /// <summary>
    /// Элементы карты преобразований человеко-машинного взаимодействия.
    /// </summary>
    public sealed class MapItem
    {
        /// <summary>
        /// Варианты вопроса на естественном языке и соответствующие им шаблоны ответа.
        /// </summary>
        public Dictionary<string, string> VariantAnswerPairs { get; set; }

        /// <summary>
        /// Соответствующий шаблон запроса на языке SPARQL.
        /// </summary>
        public string QueryTemplate { get; set; }

        /// <summary>
        /// Возвращает запрос, сформированный на основании переданных параметров.
        /// </summary>
        /// <param name="arguments">Параметры запроса, извлечённые из вопроса пользователя на естественном языке.</param>
        /// <returns>Конкретный запрос на языке SPARQL, соответствующий вопросу пользователя.</returns>
        public string ConcreteQuery(params string[] arguments)
            => string.Format(this.QueryTemplate, arguments);
    }
}