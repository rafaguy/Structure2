using System.Threading.Tasks;

namespace Structure.Interface
{
    /// <summary>
    /// IfileMaanager for Photo Conversion to 64 bit
    /// </summary>
    public interface IFileMgr
    {

        /// <summary>
        /// Gets the base64 image string.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        string GetBase64ImageString(string filename);

        Task<string> Base64ToFile(string base64String, string fileName);
    }
}
