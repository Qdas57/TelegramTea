using ConsoleApp;
using ConsoleApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Internal;
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

        public List<PhotoEntity> GetAllPhotos()
        {
            try
            {
                var result = _photoContext.Photos.ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nОшибка в GetRandomPhoto");
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nОшибка в GetRandomPhotoByTag");
                return null;
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

        public bool DeletePhoto(int id)
        {
            try
            {
                var findedPhoto = _photoContext.Photos.FirstOrDefault(p => p.Id == id);

                if (findedPhoto is null)
                {
                    //throw new ArgumentException($"Photo with id {id} not found.", nameof(id));
                    return false;
                }

                _photoContext.Remove(findedPhoto);

                _photoContext.SaveChanges();

                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message + "\nОшибка в методе DeletePhoto");
                return false;
            }

        }

        public List<string> GetTagList()
        {
            try
            {
                var query = _photoContext.Photos.Select(u => u.Tag).Distinct();

                var tags = query.ToList();

                return tags;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nОшибка в методе GetTagList");
                return null;
            }
        }

    }
}
