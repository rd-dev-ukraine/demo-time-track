using System;

namespace LanceTrack.Domain.Projects
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}