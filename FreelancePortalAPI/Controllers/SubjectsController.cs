using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Subjects;
using Services.Services.Subjects;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Subject> Repository { get; }
        private SubjectsService SubjectsService { get; }

        public SubjectsController(IRepository<Subject> repository, SubjectsService subjectsService, IMapper mapper)
        {
            Repository = repository;
            SubjectsService = subjectsService;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SubjectModel>> Create([FromBody] SubjectModel subjectModel)
        {
            var result = SubjectsService.Create(subjectModel);

            SubjectModel subject = Mapper.Map<SubjectModel>(result);

            return Ok(subject);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<SubjectModel>>> GetSubjects()
        {
            var subjects = Repository.GetAll();

            return Ok(Mapper.Map<List<SubjectModel>>(subjects));
        }

        [HttpPut]
        public async Task<ActionResult<SubjectModel>> Update([FromBody] SubjectModel createModel)
        {
            var result = SubjectsService.Update(createModel);

            SubjectModel subjectModel = Mapper.Map<SubjectModel>(result);

            return Ok(subjectModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var subject = Repository.GetSingleOrDefault(x => x.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            Repository.Remove(subject);
            return Ok();
        }
    }
}
