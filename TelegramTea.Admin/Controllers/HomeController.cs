using Microsoft.AspNetCore.Mvc;
using TelegramTea.Repositories;

namespace TelegramTea.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhotoRepository _photoRepository;

        public HomeController()
        {
            _photoRepository = new PhotoRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Photos()
        {
            //TODO: По три карточки в ряд
            //TODO: Вместе с датой - сколько дней назад была добавлена(сегодня, вчера, x дней назад)
            //TODO: Определить новый класс модели - не отдавать на View PhotoEntity
            var photos = _photoRepository.GetAllPhotos();

            return View(photos);
        }
    }
}
