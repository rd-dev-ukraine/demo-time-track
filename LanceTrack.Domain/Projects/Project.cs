﻿using System;

namespace LanceTrack.Domain.Projects
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}