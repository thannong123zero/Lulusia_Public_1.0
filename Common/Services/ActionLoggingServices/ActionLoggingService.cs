using Common.Models;
using Common.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Common.Services.ActionLoggingServices
{
    public class ActionLoggingService : IActionloggingService
    {
        private readonly IMongoCollection<UserActionModel> _userActionsCollection;
        private readonly IJwtService _jwtService;
        public ActionLoggingService(
            IJwtService jwtService,
            IOptions<UserActionLoggingDatabaseSettings> userActionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                userActionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userActionDatabaseSettings.Value.DatabaseName);
            _userActionsCollection = mongoDatabase.GetCollection<UserActionModel>(
                userActionDatabaseSettings.Value.UserActionsCollectionName);
            _jwtService = jwtService;
        }

        public async Task<Pagination<UserActionModel>> GetAsync(int pageIndex)
        {
            Pagination<UserActionModel> pageViewModel = new Pagination<UserActionModel>();

            var userActions = await _userActionsCollection.Find(_ => true).Sort(Builders<UserActionModel>.Sort.Descending("CreatedDate")).ToListAsync();
            pageViewModel.TotalItems = userActions.Count;
            pageViewModel.PageIndex = pageIndex;
            pageViewModel.TotalPages = (int)Math.Ceiling(pageViewModel.TotalItems / (double)pageViewModel.PageSize);
            pageViewModel.CurrentPage = pageViewModel.PageIndex;
            pageViewModel.Items = userActions.Skip((pageViewModel.CurrentPage - 1) * pageViewModel.PageSize).Take(pageViewModel.PageSize);
            return pageViewModel;
        }

        public async Task<UserActionModel?> GetAsync(string id) =>
            await _userActionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserActionModel userAction) =>
            await _userActionsCollection.InsertOneAsync(userAction);

        public async Task CreateAsync(string token, string controllerName, Enum action, Enum actionStatus )
        {
            UserActionModel userAction = new UserActionModel()
            {
                ControllerName = controllerName,
                ActionName = action.ToString(),
                Status = actionStatus.ToString(),
                UserId = _jwtService.GetUserIdFromToken(token),
                UserName = _jwtService.GetUserNameFromToken(token)
            };
            await _userActionsCollection.InsertOneAsync(userAction);
        }

        //public async Task UpdateAsync(string id, UserAction updatedBook) =>
        //    await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        //public async Task RemoveAsync(string id) =>
        //    await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
