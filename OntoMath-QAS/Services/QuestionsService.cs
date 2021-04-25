using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OntoMath_QAS.Models.Mapper;
using OntoMath_QAS.Ontology;

namespace OntoMath_QAS.Services
{
    public class QuestionsService
    {
        public Lazy<RequestGenerator> Generator { private get; set; }

        public Lazy<QuestionMap> Map { private get; set; }

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

            var result = this.Generator.Value.GetSet(query);

            return string.Format(answerTemplate, result);
        }
    }
}