﻿namespace BLL.Models
{
    public class WorkingProgramGetModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SubjectName { get; set; }

        public bool IsAvailable { get; set; }
    }
}