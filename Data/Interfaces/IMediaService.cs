using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Interfaces
{
    public interface IMediaService
    {

        /// <summary>
        /// Öffnet die Kamera des Gerätes, um ein Foto auszunehmen.
        /// </summary>
        /// <returns>Die Bilddatei als Byte-Array oder null.</returns>
        Task<byte[]?> CapturePhotoAsync();

        /// <summary>
        /// Öffnet die Fotogalerie des Gerätes, um ein Bild auszuwählen.
        /// </summary>
        /// <returns>Die Bilddatei als Byte-Array oder null.</returns>
        Task<byte[]?> PickPhotoAsync();
    }
}
