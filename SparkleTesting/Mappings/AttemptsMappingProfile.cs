using AutoMapper;
using SparkleTesting.API.Models.Dto;
using SparkleTesting.API.Models.Enums;
using SparkleTesting.Application.Models;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Domain.Enum;
using System;
using System.Linq;

namespace SparkleTesting.API.Mappings
{
    public class AttemptsMappingProfile : Profile
    {
        public AttemptsMappingProfile()
        {
            CreateMap<Attempt, TestDto>();

            CreateMap<UserAttempt, AttemptDto>();

            CreateMap<Question, QuestionDto>().IncludeAllDerived();

            CreateMap<OptionsQuestion, QuestionDto>()
                .ForMember(s => s.QuestionType, map => map.MapFrom(s => s.Type));

            CreateMap<PassFillingQuestion, QuestionDto>()
                .ForMember(s => s.QuestionType, map => map.MapFrom(s => QuestionType.ShortAnswers))
                .ForMember(s => s.PassesIds, map => map.MapFrom(s => s.Passes.OrderBy(p => p.SortOrder).Select(p => p.Id)));

            CreateMap<OptionQuestionType, QuestionType>()
                .ConvertUsing((s, d) =>
                {
                    switch (s)
                    {
                        case OptionQuestionType.ChooseOne:
                            return QuestionType.SingleOption;
                        case OptionQuestionType.ChooseMany:
                            return QuestionType.ManyOptions;
                    }

                    throw new NotImplementedException();
                });

            CreateMap<AnswerDto, UserAnswer>()
                .ForMember(s=>s.Answers, map=>map.MapFrom(s=>s.Answers.ToDictionary(a=>a.Id, a=>a.UserAnswer)));
        }
    }
}
