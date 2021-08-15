using System;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrackingCode.Application.Contracts;
using TrackingCode.Domain.Interfaces;
using TrackingCode.Domain.Models;

namespace TrackingCode.Application.Services
{
    [ApiController]
    [Route("api/[controller]")]
    
    
    public class TrackingCodeModelAppService: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TrackingCodeModel> _repository;

        public TrackingCodeModelAppService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<TrackingCodeModel> repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpPost("TrackingCodeModel")]
        public async Task<IActionResult> CreateTrackingCodeModel(TrackingCodeModelCreateDto input)
        {
            var myTrackingCodeModel = _mapper.Map<TrackingCodeModel>(input);
            
            while (true)
            {
                try
                {
                    var pc=new PersianCalendar();
                    
                    var codeTime = 
                        pc.GetYear(DateTime.Now).ToString().Substring(2,2)+
                        pc.GetMonth(DateTime.Now).ToString().PadLeft(2,'0')+
                        pc.GetDayOfMonth(DateTime.Now).ToString().PadLeft(2,'0');
                    
                    var codeRandom = RandomString();

                    myTrackingCodeModel.TrackingCodeGenerated = input.PreName + "-" + codeTime + codeRandom;
                    
                    _repository.Add(myTrackingCodeModel);
                    await _unitOfWork.CompleteAsync();

                    var myTrackingCodeModelDto = _mapper.Map<TrackingCodeModelDto>(myTrackingCodeModel);

                    return Ok(myTrackingCodeModelDto);
                    
                }
                catch
                {
                    // ignored
                }
            }

        }
        
        private static string RandomString(int length = 4)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChars = new char[length];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}