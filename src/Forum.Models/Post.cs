using System;

namespace Forum.Models
{
    public class Post
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Text { get; private set; }

        public Post(Guid id, Guid userId, DateTime createdAt, string text)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            Text = text;
        }
    }
}
