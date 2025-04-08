﻿using Common.Models;

namespace Common.Services.ActionLoggingServices
{
    public interface IActionloggingService
    {
        public Task<Pagination<UserActionModel>> GetAsync(int pageIndex);

        public Task<UserActionModel?> GetAsync(string id);

        public Task CreateAsync(UserActionModel userAction);
        public Task CreateAsync(string token, string controllerName, Enum action, Enum actionStatus);
    }
}
