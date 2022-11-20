using UserRating.Common.Models;

namespace UserRating.Infrastructure.ServiceInterfaces
{
    public interface IProfileService
    {
        void Like(int userId, int appraiserId);

        void Dislike(int userId, int appraiserId);
    }
}
