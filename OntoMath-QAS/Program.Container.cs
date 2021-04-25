using DryIoc;

using OntoMath_QAS.Ontology;
using OntoMath_QAS.Services;

namespace OntoMath_QAS
{
    public partial class Program
    {
        /// <summary>
        /// Используемый настроенный IOC-контейнер.
        /// </summary>
        private static IContainer Container { get; }
            = new Container().With(rules => rules.With(propertiesAndFields: PropertiesAndFields.Auto));

        /// <summary>
        /// Регистрация типов в IoC-контейнере.
        /// </summary>
        /// <param name="container">Используемый IoC-контейнер</param>
        public static void Register(IContainer container)
        {
            container.Register<RequestGenerator>(Reuse.Transient);

            container.Register<QuestionsService>(Reuse.Transient);

            container.Register<QuestionMap>(Reuse.Singleton);
        }
    }
}