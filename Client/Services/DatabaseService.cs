using Client.Services.Interfaces;
using ModelLibrary;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public static class DbConstants
    {
        public const string DatabaseFilename = "TodoSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

    public class DatabaseService:IDatabaseService
    {
        private readonly IRequestService _requestService;
        private SQLiteAsyncConnection Database;

        public DatabaseService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IEnumerable<Message>> GetMessages(int chatId, long fromId = -1, int limit = 20)
        {
            await Init();
            var messages = Database.Table<Message>().Where((m) => m.ChatId == chatId);
            List<Message>? answer;
            if (fromId > 0)
                answer=await messages.Where(m => m.Id < fromId).OrderByDescending(m => m.Id).Take(limit).ToListAsync();
            answer=await messages.OrderByDescending(m => m.Id).Take(limit).ToListAsync();
            if(answer.Any())return answer;
            return await _requestService.GetAsync<IEnumerable<Message>>($"/getMessages?chatId={chatId}&fromId={fromId}&limit={limit}",0,true);
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
            await Database.CreateTableAsync<Message>();
        }
    }
}
