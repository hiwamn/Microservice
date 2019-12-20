using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Services.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository,ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activeCategory = await _categoryRepository.GetAsync(category);
            if (activeCategory == null)
            {
                throw new ActioException("category_not_found", $"Category : {category} was not found.");
            }
            var activity = new Activity(id, name, activeCategory, description, userId, createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}
