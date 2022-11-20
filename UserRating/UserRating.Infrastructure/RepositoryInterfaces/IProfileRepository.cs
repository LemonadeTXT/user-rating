using UserRating.Common.Models;

namespace UserRating.Infrastructure.RepositoryInterfaces
{
    public interface IProfileRepository
    {
        void Like(int userId, int appraiserId);

        void Dislike(int userId, int appraiserId);
    }
}
