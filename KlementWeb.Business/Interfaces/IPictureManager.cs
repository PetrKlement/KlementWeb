using Microsoft.AspNetCore.Http;
using static KlementWeb.Business.Managers.PictureManager;


namespace KlementWeb.Business.Interfaces
{
    public interface IPictureManager
    {
        void ResizePicture(string path, int width = 0, int height = 0);

        void SavePicture(IFormFile file, string fileName, PictureExtension extension, int width = 0, int height = 0);
    }
}
