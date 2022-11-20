using Microsoft.Data.SqlClient;
using UserRating.Common.Models;
using UserRating.Infrastructure.Connection;
using UserRating.Infrastructure.RepositoryInterfaces;
using System.Data;
using UserRating.Models;

namespace UserRating.Infrastructure.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ConnectionSettings _connectionSettings;

        public ProfileRepository(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public void Like(int userId, int appraiserId)
        {
            IsLiked(true, userId, appraiserId);
        }

        public void Dislike(int userId, int appraiserId)
        {
            IsLiked(false, userId, appraiserId);
        }

        private void IsLiked(bool liked, int userId, int appraiserId)
        {
            int value;

            if (liked)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }

            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "SELECT Count(Id) FROM Appraisers WHERE (UserId = @UserId) AND (AppraiserId = @AppraiserId)";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("UserId", userId);

                command.Parameters.AddWithValue("AppraiserId", appraiserId);

                command.Connection.Open();

                string queryLike;

                if ((int)command.ExecuteScalar() == 1)
                {
                    queryLike =
                        "UPDATE Appraisers SET Liked = @Liked WHERE (UserId = @UserId) AND (AppraiserId = @AppraiserId)";
                }
                else
                {
                    queryLike =
                        "INSERT INTO Appraisers (UserId, AppraiserId, Liked) VALUES (@UserId, @AppraiserId, @Liked)";
                }

                var commandLike = new SqlCommand(queryLike, connection);

                commandLike.Parameters.AddWithValue("UserId", userId);

                commandLike.Parameters.AddWithValue("AppraiserId", appraiserId);

                commandLike.Parameters.Add(new SqlParameter("Liked", SqlDbType.Bit)
                {
                    Value = value
                });

                commandLike.ExecuteNonQuery();
            }

            SetLikesDislikesForUser(userId);
        }

        private void SetLikesDislikesForUser(int id)
        {
            var likes = 0;
            var dislikes = 0;

            using (var connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                var query =
                    "SELECT * FROM Appraisers WHERE UserId = @UserId";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("UserId", id);

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["Liked"]) == 1)
                            {
                                likes++;
                            }
                            else
                            {
                                dislikes++;
                            }
                        }
                    }
                }

                var querySetLikesDislikes =
                    "UPDATE Users SET " +
                    "Likes = @Likes, " +
                    "Dislikes = @Dislikes " +
                    "WHERE Id = @Id";

                var cmd = new SqlCommand(querySetLikesDislikes, connection);

                cmd.Parameters.AddWithValue("Id", id);

                cmd.Parameters.Add(new SqlParameter("Likes", SqlDbType.Int)
                {
                    Value = likes
                });

                cmd.Parameters.Add(new SqlParameter("Dislikes", SqlDbType.Int)
                {
                    Value = dislikes
                });

                cmd.ExecuteNonQuery();
            }
        }
    }
}