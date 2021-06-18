# Question-answering-system
Магистерская диссертация по теме "Модель вопросно-ответной системы на основе онтологического подхода"

## Аннотация
Наиболее частый сценарий применения вопросно-ответных систем состоит в следующем: пользователь системы в произвольной форме на естественном языке подаёт на вход вопрос (например, «что входит в состав класса вектор?») и получает ответ, содержащий набор определений и, желательно, список веб-страниц (элементов аннотирования концептов онтологии), подтверждающих корректность ответа.

Для того чтобы обеспечить взаимодействие пользователя с программным комплексом, и сокрытой внутри вопросно-ответной системы онтологией в частности, на естественном языке, необходимо проработать систему преобразования запросов пользователя во внутреннее представление системы, а также разработать способ обратного преобразования – из ответа системы в ответ на естественном языке.

В работе решаются следующие основные задачи:
- Разработать систему преобразования вопроса пользователя в запрос к онтологии.
- Выстроить адаптивный процесс взаимодействия вопросно-ответной системы с онто-семантическими сетями.
- Разработать систему преобразования результатов поиска по онтологии в ответ на естественном языке.
- Разработать рабочий прототип модели вопросно-ответной системы на основе онтологического подхода.

Результатом работы является модель вопросно-ответной системы на основе онтологического подхода, отработанная на онтологии профессионального математического знания OntoMathPro, с возможностью дальнейшего её совершенствования как в рамках расширения и развития базы знаний OntoMathPro, так и использования с любой другой базой знаний, построенной по онтологическому подходу.

В рамках выполнения данной магистерской диссертации в прототип модели вопросно-ответной системы были заложены как возможность отвечать на вопросы пользователя, так и посредством естественно-языкового запроса иметь возможность получать сведения о самой используемой онтологии. Данная функциональная возможность представляется, например, с помощью вопроса к системе «Сколько всего концептов в онтологии», из ответа на который можно узнать количественное наполнение используемой онтологии.

## Диаграмма последовательности предлагаемой модели

![Диаграмма последовательности. Отображены порядок действий и протекающих процессов в модели вопросно-ответной системы, основанной на онтологическом подходе, предлагаемой в данной работе](https://github.com/EgoPingvina/Question-answering-system/blob/master/Documents/Images/global%20sequence%20diagram.png?raw=true)

## Алгоритм обработки вопроса

![Блок-схема обработки вопроса пользователя](https://github.com/EgoPingvina/Question-answering-system/blob/master/Documents/Images/question%20processing%20algorithm.png?raw=true)

## Демонстрация работы

![Демонстрация работы](https://github.com/EgoPingvina/Question-answering-system/blob/master/Documents/Images/%D0%94%D0%B5%D0%BC%D0%BE%D0%BD%D1%81%D1%82%D1%80%D0%B0%D1%86%D0%B8%D1%8F%20%D0%B7%D0%B0%D0%BF%D1%80%D0%BE%D1%81%D0%BE%D0%B2.gif?raw=true)
