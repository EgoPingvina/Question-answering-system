namespace OntoMath_QAS.Models.Settings
{
    /// <summary>
    /// Настройки точки доступа SPARQL.
    /// </summary>
    public sealed class SparqlSettings
    {
        /// <summary>
        /// Имя настройки в файле конфигураций.
        /// </summary>
        public const string Key = "Sparql";

        /// <summary>
        /// Адрес точки доступа.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Ограничение количества элементов выборки.
        /// </summary>
        public string Limit { get; set; }
    }
}