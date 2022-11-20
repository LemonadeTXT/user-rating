using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Common.Models;

namespace UserRating.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public void Like(int userId, int appraiserId)
        {
            _profileRepository.Like(userId, appraiserId);
        }

        public void Dislike(int userId, int appraiserId)
        {
            _profileRepository.Dislike(userId, appraiserId);
        }
    }
}
