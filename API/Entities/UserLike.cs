namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; } //user who give like other user
        public int SourceUserId { get; set; }
        public AppUser LikedUser { get; set; } //user who got like
        public int LikedUserId { get; set; }
    }
}