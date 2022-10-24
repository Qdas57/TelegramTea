using ConsoleApp;
using System;
using System.Collections.Generic;
using TelegramTea.Data.Enities;

namespace TelegramTea.Repositories
{
    internal class RequestRepository
    {
        //CRUD - Create, Read, Update, Delete

        private readonly PhotoContext _photoContext;

        public RequestRepository(PhotoContext photoContext)
        {
            _photoContext = photoContext;
        }

        public RequestEntity CreateRequest(string requsetType, int chatId, string firstName)
        {
            RequestEntity createdRequest = new RequestEntity()
            {
                RequestType = requsetType,
                ChatId = chatId,
                FirstName = firstName,
                Date = DateTime.Now
            };

            _photoContext.Add(createdRequest);

            _photoContext.SaveChanges();

            return createdRequest;
        }

        public List<RequestEntity> GetAllRequests()
        {
            //TODO: сделать
            throw new NotImplementedException();
        }

        public int GetRequestCountByType(string type)
        {
            //TODO: сделать
            throw new NotImplementedException();
        }
    }
}
