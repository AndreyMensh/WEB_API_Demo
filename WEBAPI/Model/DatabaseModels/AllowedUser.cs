namespace WEBAPI.Model.DatabaseModels
{
    public class AllowedUser
    {
        public int Id { get; set; }

        public int AllowedUserId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
