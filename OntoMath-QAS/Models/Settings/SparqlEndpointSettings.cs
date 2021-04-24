namespace OntoMath_QAS.Models.Settings
{
    /// <summary>
    /// Настройки точки доступа SPARQL.
    /// </summary>
    public sealed class SparqlEndpointSettings
    {
        /// <summary>
        /// Имя настройки в файле конфигураций.
        /// </summary>
        public const string Key = "SparqlEndpoint";

        /// <summary>
        /// Адрес точки доступа.
        /// </summary>
        public string Uri { get; set; }
    }
}