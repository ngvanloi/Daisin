using CoreLayer.Enumerators;
using CoreLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Helpes.Identity.Image
{
	public interface IImageHelper
	{
		Task<ImageUploadModel> ImageUpload(IFormFile imageFile, ImageType imageType, string? folderName);

		string DeleteImage(string imageName);
	}
}
