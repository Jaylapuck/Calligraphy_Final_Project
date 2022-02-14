using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.About;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.About
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepo _aboutRepo;

        public AboutService(IAboutRepo aboutRepo)
        {
            _aboutRepo = aboutRepo;
        }

        public IActionResult Get()
        {
            var aboutInfo = _aboutRepo.Get();
            if (aboutInfo == null) return new NotFoundResult();
            return new OkObjectResult(aboutInfo);
        }

        public IActionResult Update(AboutEntity aboutInfo)
        {
            if (aboutInfo == null) return new BadRequestResult();
            var aboutToUpdate = _aboutRepo.Get();
            if (aboutToUpdate == null) return new NotFoundResult();
            aboutToUpdate.Name = aboutInfo.Name;
            aboutToUpdate.Email = aboutInfo.Email;
            aboutToUpdate.Phone = aboutInfo.Phone;
            aboutToUpdate.Profession = aboutInfo.Profession;
            aboutToUpdate.Description = aboutInfo.Description;
            aboutToUpdate.Language = aboutInfo.Language;
            aboutToUpdate.Country = aboutInfo.Country;
            aboutToUpdate.Experience = aboutInfo.Mission;
            aboutToUpdate.Mission = aboutInfo.Mission;

            _aboutRepo.Update(aboutToUpdate);
            return new OkObjectResult(aboutToUpdate);
        }
    }
}