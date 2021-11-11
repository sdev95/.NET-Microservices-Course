using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Data 
{
    public interface IPlatformRepo 
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatFormById(int id);
        void CreatePlatform(Platform plat);
    }

}