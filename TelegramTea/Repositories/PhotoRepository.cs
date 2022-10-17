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

        public PhotoEntity GetRandomPhoto()
        {
            try
            {
                var count = _photoContext.Photos.Count();

                Random random = new Random();

                int index = random.Next(0, count - 1);

                var photos = _photoContext.Photos.ToList();

                return photos[index];
            }
            catch (Exception)
            {
                //TODO: Logging
                throw;
            }            
        }

        public PhotoEntity GetRandomPhotoByTag(string tag)
        {
            try
            {
                var query = _photoContext.Photos.Where(u => u.Tag.Contains(tag));
                
                var count = query.Count();

                Random random = new Random();

                int index = random.Next(0, count - 1);

                var photos = query.ToList();

                return photos[index];
            }
            catch (Exception)
            {
                //TODO: Logging
                throw;
            }
        }

        public int GetCountPhotos()
        {
            IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;

            return photoEntities.Count();
        }

        public int GetCountPhotosByTag(string tag)
        {
            IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;

            var result = photoEntities.Where(x => x.Tag == Tag).Count();

            return result;

        }
        
        public PhotoEntity DeletePhoto(int id)
        {
            throw new NotImplementedException();
            //удаление фото
            //IQueryable<PhotoEntity> photoEntities = _photoContext.Photos;
            //photoEntities = photoEntities.Where(x => x.Id == Id);

        }

        public List<string> GetTagList()
        {
            throw new NotImplementedException();
            //TODO: вернуть список уникальных
        }

    }
}
