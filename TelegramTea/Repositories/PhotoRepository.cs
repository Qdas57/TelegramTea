using ConsoleApp;
using ConsoleApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramTea.Repositories
{
    public class PhotoRepository : PhotoEntity
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
            IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;
            return photoEntities.Count();
        }

        public int GetCountPhotosByTag(string tag)
        {
            
            // количество фото в бд с таким тегом
            IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;
            var result = photoEntities.Where(x => x.Tag == Tag).Count();
            return result;

        }
        public PhotoEntity DeletePhoto(int id)
        {
            
            //удаление фото
            IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;
            photoEntities = photoEntities.Where(x => x.Id == Id);

        }


    }
}
