using DryIoc;

namespace OntoMath_QAS
{
    public partial class Program
    {
        private static IContainer Container { get; }
                 = new Container().With(rules => rules.With(propertiesAndFields: PropertiesAndFields.Auto));

        /// <summary>
        /// Регистрация типов в IoC-контейнере.
        /// </summary>
        /// <param name="container">Используемый IoC-контейнер</param>
        public static void Register(IContainer container)
        {

        }
    }
}