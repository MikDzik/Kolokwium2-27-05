using System;

namespace MessagePublisher
{
    /// <summary>
    /// DO NOT MODIFY THIS FILE
    /// </summary>
    public abstract class Message
    {
        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
