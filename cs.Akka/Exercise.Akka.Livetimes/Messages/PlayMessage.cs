using System;

namespace Exercise_Akka.Messages
{
    public class PlayMessage
    {
        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }

        public PlayMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle ?? throw new ArgumentNullException(nameof(movieTitle));
            UserId = userId;
        }
    }
}
