using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.About
{
    public interface IAboutRepo
    {
        AboutEntity Get();
        AboutEntity Update(AboutEntity about);
    }
}
