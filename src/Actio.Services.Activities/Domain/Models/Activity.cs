using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        protected Activity()
        { 
        }

        public Activity(Guid id,string name,Category category,string description,Guid userId,DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
            Category = category.Name;
        }

    }
}
