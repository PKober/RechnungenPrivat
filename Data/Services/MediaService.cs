using RechnungenPrivat.Data.Interfaces;

using System.IO;

namespace RechnungenPrivat.Data.Services
{
    public class MediaService : IMediaService
    {

        private async Task<byte[]?> ConvertFileResultToByteArray(FileResult fileResult)
        {
            if (fileResult == null)
            {
                return null;
            }

   
            using (var stream = await fileResult.OpenReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
            
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public async Task<byte[]?> CapturePhotoAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported)
            {
                Console.WriteLine("Keine Kamera auf dem Gerät verfügbar.");
                return null;
            }

            try
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                return await ConvertFileResultToByteArray(photo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler bei der Fotoaufnahme: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]?> PickPhotoAsync()
        {
            try
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();
                return await ConvertFileResultToByteArray(photo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler bei der Fotoauswahl: {ex.Message}");
                return null;
            }
        }
    }
}
