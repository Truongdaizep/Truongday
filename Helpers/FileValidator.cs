namespace NETCORE.Helpers
{
    public interface IFileValidator
    {
        bool IsValid(IFormFile file);
    }
    public class FileValidator : IFileValidator
    {
        private readonly IConfiguration _configuration;
        private readonly int _fileSizeLimit;
        private readonly string[] _allowedExtensions;

        public FileValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileSizeLimit = _configuration.GetValue("FileUpload:FileSizeLimitInBytes", 10 * 1024 * 1024); // 1MB
            _allowedExtensions = _configuration.GetValue("FileUpload:AllowedExtensions", ".jpg,.jpeg,.png")!.Split(",");
        }

        public bool IsValid(IFormFile file)
        {
            if (file?.Length > 0)
            {
                if (file.Length > _fileSizeLimit)
                    return false;

                if (file.FileName.Length > 255)
                    return false;

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Any(e => e.Contains(extension)))
                    return false;

                return true;
            }

            return false;
        }
    }
}