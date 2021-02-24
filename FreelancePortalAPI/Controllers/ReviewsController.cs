using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Reviews;
using Services.Models.Reviews;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Review> Repository { get; }
        private ReviewsService ReviewsService { get; }

        public ReviewsController(IRepository<Review> repository, ReviewsService reviewsService, IMapper mapper)
        {
            Repository = repository;
            ReviewsService = reviewsService;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateModel>> Create([FromBody] CreateModel createModel)
        {
            createModel.DatePostedUTC = DateTime.UtcNow;
            var result = ReviewsService.Create(createModel);

            CreateModel reviewViewModel = Mapper.Map<CreateModel>(result);

            return Ok(reviewViewModel);
        }

        [HttpGet("user-reviews/{userId}")]
        public async Task<ActionResult<List<ViewModel>>> GetReviewsForUser([FromRoute] string userId)
        {
            var reviewsForUser = ReviewsService.GetReviewsForUser(userId);

            return Ok(Mapper.Map<List<ViewModel>>(reviewsForUser));
        }

        [HttpPut]
        public async Task<ActionResult<CreateModel>> Update([FromBody] CreateModel createModel)
        {
            var result = ReviewsService.Update(createModel);

            CreateModel reviewViewModel = Mapper.Map<CreateModel>(result);

            return Ok(reviewViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var post = Repository.GetSingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            Repository.Remove(post);
            return Ok();
        }
    }
}
