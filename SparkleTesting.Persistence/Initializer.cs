using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkleTesting.Persistence
{
    public class Initializer
    {
        private readonly SparkleDbContext _db;
        public Initializer(SparkleDbContext db)
        {
            _db = db;
        }

        public async Task Initialize()
        {
            _db.Database.Migrate();

            if (_db.Test.Any())
            {
                return;
            }

            _db.Test.Add(AddFirstCourseTest());
            _db.Test.Add(AddSecondCourseTest());
            _db.Test.Add(AddThirdCourseTest());

            await _db.SaveChangesAsync();
        }

        private Test AddMockTest()
        {
            var test = new Test()
            {
                Name = "Проверочный тест",
                AttemptTime = TimeSpan.FromMinutes(5),
                InProgress = false,
            };

            test.Marks.Add(new TestMark
            {
                Name = "Все очень плохо",
                PointsThreshold = 0,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Уже получше",
                PointsThreshold = 1,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Cool",
                PointsThreshold = 2,
            });

            var optQuestion = new OptionsQuestion
            {
                Text = "Простой выбор одного ответа",
                SortOrder = 3,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Неверный",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            test.Questions.Add(optQuestion);

            optQuestion = new OptionsQuestion
            {
                Text = "Выбор нескольких ответов",
                SortOrder = 1,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Неверный",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            test.Questions.Add(optQuestion);

            var fillQuestion = new PassFillingQuestion
            {
                Text = "Заполните несколько пропусков",
                SortOrder = 2,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "Верно",
                    "Верняк"
                },
                SortOrder = 1
            });

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "Ее",
                    "уу"
                },
                SortOrder = 2
            });

            test.Questions.Add(fillQuestion);

            return test;
        }

        #region FirstCourse
        private Test AddFirstCourseTest()
        {
            var test = new Test()
            {
                Name = "Тестирование, 1-ый год обучения",
                AttemptTime = TimeSpan.FromMinutes(30),
                InProgress = false,
            };

            AddFirstCourseMarks(test);

            test.Questions.Add(Create1Course1Question());
            test.Questions.Add(Create1Course2Question());
            test.Questions.Add(Create1Course3Question());
            test.Questions.Add(Create1Course4Question());
            test.Questions.Add(Create1Course5Question());
            test.Questions.Add(Create1Course6Question());
            test.Questions.Add(Create1Course7Question());
            test.Questions.Add(Create1Course8Question());
            test.Questions.Add(Create1Course9Question());
            test.Questions.Add(Create1Course10Question());
            test.Questions.Add(Create1Course11Question());
            test.Questions.Add(Create1Course12Question());
            test.Questions.Add(Create1Course13Question());
            test.Questions.Add(Create1Course14Question());
            test.Questions.Add(Create1Course15Question());
            test.Questions.Add(Create1Course16Question());
            test.Questions.Add(Create1Course17Question());
            test.Questions.Add(Create1Course18Question());
            test.Questions.Add(Create1Course19Question());
            test.Questions.Add(Create1Course20Question());

            return test;
        }
        public void AddFirstCourseMarks(Test test)
        {
            test.Marks.Add(new TestMark
            {
                Name = "Низкий уровень освоения программы",
                PointsThreshold = 0,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Средний уровень освоения программы",
                PointsThreshold = 11,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Высокий уровень освоения программы",
                PointsThreshold = 15,
            });
        }

        public Question Create1Course1Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Пои — это …?",
                SortOrder = 1,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "шарики на цепях. Вид реквизита, который используется для развития ловкости и координации движений",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "фитили на цепях. Вид реквизита, который используется для развития ловкости и координации движений",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "шары на цепях. Вид реквизита, который используется для развития памяти и мышления",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "предмет для манипулирования, представляющий собой пару цепочек с прикрепленными к концам фитилями. С другого конца у цепочек есть специальные петли, которые надеваются на пальцы",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course2Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие года принято считать временем постепенной популяризации пои в России, когда всё больше людей знакомятся с данным видом искусства, появляются первые сборы для совместного кручения?",
                SortOrder = 2,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "2002 - 2005 г.",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "2005 - 2007 г.",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "2004 – 2005 г.",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "2000 – 2005 г.",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course3Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какое из направлений поинга лишнее?",
                SortOrder = 3,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "жонглирование пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "мульти-поинг",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "пиксель-поинг",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "партнер-поинг",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кручение 3 и 4 пои одновременно",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "контактный поинг",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course4Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Пои подразделяются на: тренировочные (пои-флаги, змеи, носки) и боевые. На какие два вида реквизита подразделяются боевые пои?",
                SortOrder = 4,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "огненные, световые",
                    "световые, огненные",
                    "огненные световые",
                    "световые огненные",
                    "огненные и световые",
                    "световые и огненные",
                     "огненныесветовые",
                    "световыеогненные",
                },
                SortOrder = 1
            });

            return fillQuestion;
        }
        public Question Create1Course5Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Родиной пои считается…?",
                SortOrder = 5,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Новая Зеландия",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Новая Гвинея",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Новый Орлеан",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Сингапур",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course6Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Заполни пропуски. Изучение элементов должно быть поэтапным от _________ к _________.",
                SortOrder = 6,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "простого",
                    "легкого"
                },
                SortOrder = 1
            });

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "сложному"
                },
                SortOrder = 2
            });

            return fillQuestion;
        }
        public Question Create1Course7Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Пои, на языке племя Маори означает…?",
                SortOrder = 7,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "«кнут»",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "«мяч»",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "«колесо»",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "«глаз»",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course8Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "В каком году, после одного европейского open-air фестиваля, пои появились в России?",
                SortOrder = 8,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "в 1990",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 2000",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 1999",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 1994",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course9Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Контактный поинг – это …?",
                SortOrder = 9,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "вид взаимодействия с реквизитом, непосредственно с даблами, при котором осуществляется прокат по внутренней (inner forearm) или внешней (outer forearm) стороне рук",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "вид взаимодействия с реквизитом, непосредственно с пои, при котором осуществляется жонгляж из правой руки в левую",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "вид взаимодействия с реквизитом, непосредственно с пои, при котором осуществляется вращение пои с высокой скоростью",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "вид взаимодействия с реквизитом, непосредственно с пои, при котором осуществляется прокат по внутренней (inner forearm) или внешней (outer forearm) стороне рук",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course10Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Большой стопорящий узел, сплетенный особым образом из толстой веревки. Разновидность фитилей для пои – это …?",
                SortOrder = 10,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "изис",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кафедрал",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "манкифист",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "ролл",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course11Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "По технике безопасности, при использовании огненного реквизита, для тушения пламени при себе необходимо иметь …?",
                SortOrder = 11,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "брезент",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "огнетушитель",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "сухой лёд",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "воду",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course12Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Как называется своего рода шарнир, который крепится к цепи, чтобы она не закручивалась при вращении пои?",
                SortOrder = 12,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "крепёж",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "крюк",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "петля",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "вертлюг",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course13Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Пои - это традиционный предмет быта племени Маори, который использовался мужчинами и женщинами. Женщины использовали пои для развития гибкости и пластики в ритуальном танце «Капахака», а мужчины, для развития - ...?",
                SortOrder = 13,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "воображения и мышления",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "силы и ловкости",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "остроты зрения и слуха",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "ритма и слуха",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course14Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Как называется техника вращения пои, когда центр вращения находится не около кистей рук, а смещается к середине цепи?",
                SortOrder = 14,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "антиспин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "изоляция",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "жонгляж",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "спин",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course15Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какой реквизит описан? «Стержень с фитилём на конце, крепящийся на ладони. Бывают только боевые»",
                SortOrder = 15,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "даблы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "снейки",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "палм-тачи",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course16Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какое топливо можно использовать для замачивания огненного оборудования?",
                SortOrder = 16,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "парафин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "бензин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "керосин",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "спирт",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course17Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Сколько оборотов имеет четырёхлепестковый антиспиновый цветок?",
                SortOrder = 17,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "3",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "4",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "2",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "6",
                IsCorrect = false,
            });

            return optQuestion;
        }
        public Question Create1Course18Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Мультипоинг – это техника кручения, когда осуществляется вращение более … пои в руках?",
                SortOrder = 18,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "2",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "3",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "только 2",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "только 4",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course19Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Внешне напоминает небольшой стафф, но в движение приводится не кистями рук, а парой специальных палочек – это? ",
                SortOrder = 19,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "скакалка",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кнут",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "диаболо",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "девилстик",
                IsCorrect = true,
            });

            return optQuestion;
        }
        public Question Create1Course20Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Разновидность реквизита по конструкции напоминающая сферу на длинной цепи, называется – …?",
                SortOrder = 20,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "драгон-стафф",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "комета",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "снейк",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "куб",
                IsCorrect = false,
            });

            return optQuestion;
        }

        #endregion

        #region ThirdCourse
        private Test AddThirdCourseTest()
        {
            var test = new Test()
            {
                Name = "Тестирование, 3-ий год обучения",
                AttemptTime = TimeSpan.FromMinutes(30),
                InProgress = false,
            };

            AddThirdCourseMarks(test);

            test.Questions.Add(Create3Course1Question());
            test.Questions.Add(Create3Course2Question());
            test.Questions.Add(Create3Course3Question());
            test.Questions.Add(Create3Course4Question());
            test.Questions.Add(Create3Course5Question());
            test.Questions.Add(Create3Course6Question());
            test.Questions.Add(Create3Course7Question());
            test.Questions.Add(Create3Course8Question());
            test.Questions.Add(Create3Course9Question());
            test.Questions.Add(Create3Course10Question());
            test.Questions.Add(Create3Course11Question());
            test.Questions.Add(Create3Course12Question());
            test.Questions.Add(Create3Course13Question());
            test.Questions.Add(Create3Course14Question());
            test.Questions.Add(Create3Course15Question());
            test.Questions.Add(Create3Course16Question());
            test.Questions.Add(Create3Course17Question());
            test.Questions.Add(Create3Course18Question());
            test.Questions.Add(Create3Course19Question());
            test.Questions.Add(Create3Course20Question());

            return test;
        }

        private void AddThirdCourseMarks(Test test)
        {
            test.Marks.Add(new TestMark
            {
                Name = "Низкий уровень освоения программы",
                PointsThreshold = 0,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Средний уровень освоения программы",
                PointsThreshold = 11,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Высокий уровень освоения программы",
                PointsThreshold = 15,
            });
        }

        private Question Create3Course1Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Техника вращения пои, при которой центр вращения находится не около кистей рук, а смещается к середине цепи - это?",
                SortOrder = 1,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "изоляция",
                },
                SortOrder = 1
            });

            return fillQuestion;
        }
        private Question Create3Course2Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Кто из нижеперечисленных пойстеров является сподвижником разновидности скользящего стиля манипуляции с пои в России?",
                SortOrder = 2,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Иван Горбунов",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Евгений Новиков",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Иван Бойко",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Лара Лё",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course3Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Какая плоскость расположена вертикально и образует прямой угол с горизонтальной и фронтальной плоскостями. Проходит через тело в переднезаднем направлении?",
                SortOrder = 3,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "профильная ",
                    "сагиттальная",
                },
                SortOrder = 1
            });

            return fillQuestion;
        }
        private Question Create3Course4Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Позиция рук за спиной/под плечами - это?",
                SortOrder = 4,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Behind The Head",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Between The Hands",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Behind The Back",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Between The legs",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course5Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Элемент, при котором руки пойстера скрещены на груди, и каждой рукой он сбоку вращает восьмёрку в противоположные стороны - это?",
                SortOrder = 5,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "трикетра",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кроссер",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "космо",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "колесо",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course6Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Направление движения пои в одну сторону – по часовой стрелке или против часовой стрелки.",
                SortOrder = 6,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Opposite Direction",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Same Direction",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "In Spin",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Anti Spin",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course7Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Что такое радиосинхронизация?",
                SortOrder = 7,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "синхронизация переключения программы реквизита между собой",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для стабилизации изображения пиксельного реквизита",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для автоматического выключения пиксельного реквизита",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для зарядки светодиодного реквизита",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course8Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Как вращаются снаряды при синхронизация вращения пои в Split Time?",
                SortOrder = 8,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "синхронно",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "асинхронно",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "гибридно",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "не вращаются вообще",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course9Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Каково второе название элемента «столла»?",
                SortOrder = 9,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "маятник",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "остановка",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "оппозитное вращение",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прокат",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course10Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Сколько оборотов имеет антиспиновый четырёхлепестковый цветок исполняемый в 3D?",
                SortOrder = 10,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "шесть",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "четыре",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "пять",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "три",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create3Course11Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "По технике безопасности, при использовании огненного реквизита, для ликвидации возгорания при себе необходимо иметь …?",
                SortOrder = 11,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Сухой лёд",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Воду",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Брезент",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Всё вышеперечисленное",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course12Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Разновидность фитилей для пои, имеющие квадратную форму. Какой реквизит описан?",
                SortOrder = 12,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "мунблейзы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "роллы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "манкифисты",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кафедралы",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create3Course13Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Металлическая конструкция для подвеса осветительных приборов над сценой, поднимаемая и опускаемая вручную или с помощью электропривода - это?",
                SortOrder = 13,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "софит",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прожектор",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "световая заливка",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "стробоскоп",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course14Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какую конфигурацию имеет синхронное вращение пои в одну сторону (по часовой стрелке)?",
                SortOrder = 14,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Same Time / Same Direction",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Split Time / Same Direction",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Same Time / Opposite Direction",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Split Time / Opposite Direction",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course15Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Через какую точку происходит переход в элементе «трикетра» для вращения в противоположную сторону?",
                SortOrder = 15,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "нижнюю",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "нижнюю / боковую",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "верхнюю",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "любую",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course16Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какое топливо можно использовать для глотания огня?",
                SortOrder = 16,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "парафин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "бензин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "керосин",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "светал",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course17Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Как необходимо держать фитиль пиротехнического оборудования для безопасного использования?",
                SortOrder = 17,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "на себя",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "от себя",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в бок",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "как угодно",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course18Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Предмет для манипулирования, напоминающий большую катушку. Пойстер, используя две палочки между которыми натянута веревка, подбрасывает, ловит, крутит и делает множество других трюков - это?",
                SortOrder = 18,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "девилстик",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "диаболо",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "факел",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "роупдарт",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create3Course19Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие из указанных ниже реквизитов являются смежными?",
                SortOrder = 19,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "даблы и пои",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "стафф и веера",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "веера и пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "даблы и пои",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create3Course20Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Контактный стиль в поинге характеризует? ",
                SortOrder = 20,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "большое количество спиновых вращений",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "большое количество бросков пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "плавность исполнения элементов ",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "большое количество прокатов",
                IsCorrect = true,
            });

            return optQuestion;
        }
        #endregion

        #region SecondCourse
        private Test AddSecondCourseTest()
        {
            var test = new Test()
            {
                Name = "Тестирование, 2-ой год обучения",
                AttemptTime = TimeSpan.FromMinutes(30),
                InProgress = false,
            };

            AddSecondCourseMarks(test);

            test.Questions.Add(Create2Course1Question());
            test.Questions.Add(Create2Course2Question());
            test.Questions.Add(Create2Course3Question());
            test.Questions.Add(Create2Course4Question());
            test.Questions.Add(Create2Course5Question());
            test.Questions.Add(Create2Course6Question());
            test.Questions.Add(Create2Course7Question());
            test.Questions.Add(Create2Course8Question());
            test.Questions.Add(Create2Course9Question());
            test.Questions.Add(Create2Course10Question());
            test.Questions.Add(Create2Course11Question());
            test.Questions.Add(Create2Course12Question());
            test.Questions.Add(Create2Course13Question());
            test.Questions.Add(Create2Course14Question());
            test.Questions.Add(Create2Course15Question());
            test.Questions.Add(Create2Course16Question());
            test.Questions.Add(Create2Course17Question());
            test.Questions.Add(Create2Course18Question());
            test.Questions.Add(Create2Course19Question());
            test.Questions.Add(Create2Course20Question());

            return test;
        }

        private void AddSecondCourseMarks(Test test)
        {
            test.Marks.Add(new TestMark
            {
                Name = "Низкий уровень освоения программы",
                PointsThreshold = 0,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Средний уровень освоения программы",
                PointsThreshold = 11,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Высокий уровень освоения программы",
                PointsThreshold = 15,
            });
        }
        private Question Create2Course1Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие из перечисленных направлений существуют в современном поинге?",
                SortOrder = 1,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Классический поиг",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Мультипоинг (3 и более пои)",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Контактный поинг",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Жонглирование",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "3D поинг",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Партнёр-поинг",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course2Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Кто из нижеперечисленных пойстеров является сподвижником контактного поинга в России?",
                SortOrder = 2,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Антон Мальцев",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Вячеслав Амосов",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Иван Бойко",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Иван Горбунов",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course3Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Каково полное название «пои», которое используют в контактном поинге?",
                SortOrder = 3,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "пиксель-пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "пендулум-пои",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "световые пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "боевые пои",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course4Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Что относится к составным частям контактных пои?",
                SortOrder = 4,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "мяч",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "вертлюг",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "стейдж",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "цепь",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "шнур",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "петля",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "ноб",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course5Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Что есть внешняя точка входа в контактном поинге?",
                SortOrder = 5,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "бросок пои вверх",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прокат по руке",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "внутренняя сторона ладони",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "внешняя сторона ладони",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course6Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Заполни пропуски. Исполняя элемент – «Антиспиновый цветок», руки осуществляют вращение ___________, а вращение пои осуществляется __________. И наоборот.",
                SortOrder = 6,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "вперед",
                    "назад",
                },
                SortOrder = 1,
            });

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "вперед",
                    "назад",
                },
                SortOrder = 2,
            });

            return fillQuestion;
        }
        private Question Create2Course7Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Что такое акселерометр?",
                SortOrder = 7,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для стабилизации изображения пиксельного реквизита",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для автоматического выключения пиксельного реквизита",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "радиопередатчик",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "прибор для зарядки светодиодного реквизита",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course8Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие стили манипуляции реквизитом «веера» существуют?",
                SortOrder = 8,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "геометрия",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "жонглирование",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "мульти-манипуляции",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "фронтальные вращение",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course9Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие два типа стаффов существуют?",
                SortOrder = 9,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "спиновый и антиспиновый",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "спиновый и инспиновый",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "спиновый и контактный",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "контактный и антиспиновый",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course10Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Два коротких изогнутых стаффа имеющие фитили на обеих концах. Существуют тренировочные и боевые (огненные и световые) – это …?",
                SortOrder = 10,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "даблы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "бугенги",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "стафф",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "веера",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course11Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "По технике безопасности, при использовании огненного реквизита, для экстренной обработки ожога при себе необходимо иметь …?",
                SortOrder = 11,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Сухой лёд",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Аспирин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Пантенол",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Всё вышеперечисленное",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course12Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Разновидность фитилей для пои, имеющая форму звёздчатого многоугольника. Какой реквизит описан?",
                SortOrder = 12,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "мунблейзы",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "кафедралы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "роллы",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "манкифисты",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course13Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "При исполнении какого элемента с реквизитом пои вращение правой руки осуществляется по часовой стрелке и проходит 4-е точки (с переходом в верхней точке) и вращение пои, так же осуществляется по часовой стрелке, образуя 3-и оборота?",
                SortOrder = 13,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "волна",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "трёхбитная восьмёрка",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "антиспиновый цветок",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "спиновый цветок",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course14Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какие стили манипуляции реквизитом «обруч» существуют?",
                SortOrder = 14,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "спиновые вращения",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "жонглирование",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "обороты на теле",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "всё вышеперечисленное",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course15Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Через какую точку происходит поворот при исполнении элемента «антиспиновый цветок»?",
                SortOrder = 15,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "нижнюю",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "боковую",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "верхнюю",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "любую",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course16Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Какое топливо можно использовать для бризинга?",
                SortOrder = 16,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "парафин",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "бензин",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "керосин",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "светал",
                IsCorrect = true,
            });

            return optQuestion;
        }
        private Question Create2Course17Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Сколько оборотов имеет элемент «трикетра»?",
                SortOrder = 17,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "3",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "4",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "2",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "6",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course18Question()
        {
            var fillQuestion = new PassFillingQuestion
            {
                Text = "Какие два вида столл существуют?",
                SortOrder = 18,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "вертикальные, горизонтальные",
                    "горизонтальные, вертикальные",
                    "вертикальные горизонтальные",
                    "горизонтальные вертикальные",
                    "вертикальные и горизонтальные",
                    "горизонтальные и вертикальные",
                     "горизонтальныевертикальные",
                    "вертикальныегоризонтальные",
                },
                SortOrder = 1,
            });

            return fillQuestion;
        }
        private Question Create2Course19Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Во скольких плоскостях исполняются 3D элементы? ",
                SortOrder = 19,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "в 2",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 3",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 4",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "в 1",
                IsCorrect = false,
            });

            return optQuestion;
        }
        private Question Create2Course20Question()
        {
            var optQuestion = new OptionsQuestion
            {
                Text = "Скользящий стиль в поинге характеризует?",
                SortOrder = 19,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "большое количество спиновых вращений",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "большое количество бросков пои",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "плавность исполнения элементов",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "плавность исполнения элементов и близость рук к телу при вращении пои",
                IsCorrect = true,
            });

            return optQuestion;
        }
        #endregion
    }
}
