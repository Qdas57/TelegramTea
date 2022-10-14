using ConsoleApp;
using ConsoleApp.Data.Entities;
using System;

namespace TelegramTea.Repositories
{
    public class PhotoRepository
    {
        //CRUD - Create, Read, Update, Delete

        private readonly PhotoContext _photoContext;

        public PhotoRepository()
        {
            _photoContext = new PhotoContext();
        }

        public PhotoEntity CreatePhoto(string name, string tag)
        {
            PhotoEntity photoEntity = new PhotoEntity()
            {
                DateUpload = DateTime.Now.ToString(),
                NamePhoto = name,
                Tag = tag
            };

            _photoContext.Add(photoEntity);

            _photoContext.SaveChanges();

            return photoEntity;
        }

        public PhotoEntity GetRandomPhotoByTag(string tag)
        {
            //TODO: случайную фотку по тегу
            throw new NotImplementedException();
        }

        public int GetCountPhotos()
        {
            //TODO: количество фото в бд
            throw new NotImplementedException();
        }

        public int GetCountPhotosByTag()
        {
            //TODO: количество фото в бд с таким тегом
            throw new NotImplementedException();
        }

        public PhotoEntity DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
