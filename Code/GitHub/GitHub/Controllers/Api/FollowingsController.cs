using System.Web.Http;
using GitHub.Core;
using GitHub.Core.Dtos;
using GitHub.Core.Models;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var following = _unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);
            if (following != null)
                return BadRequest("Following already exists.");

            var f = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Followings.Add(f);
            _unitOfWork.Complete();
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(userId, id);

            if (following==null)
            return NotFound();

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();
            return Ok(id);

        }
    }
}
