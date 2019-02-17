using HeyRed.Mime;
using Pho84SnackApi.Models;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Pho84SnackApi.Services
{
    public class SnackCore
    {
        // Konstante
        public const string ALLOWED_ORIGINS = "";
        public const int COOKIE_MAX_AGE_MONTH = 6;
        public const string LOGIN_CLAIM_IDENTITY = "SnackyBastas";
        public const string IMAGES_DIRECTORY = "images";
        public const string ARCHIVE_DIRECTORY = "archive";

        private const int iterlation = 10000;
        private const int saltBytesLength = 17;
        private const int passwordBytesLength = 25;

        public static string GetEncodedPassword(string password)
        {
            // Creates new salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[saltBytesLength]);

            // Creates the Rfc2898DeriveBytes and get the hash value fron password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterlation);
            byte[] hash = pbkdf2.GetBytes(passwordBytesLength);

            // Combines salt and password
            byte[] hashBytes = new byte[saltBytesLength + passwordBytesLength];
            Array.Copy(salt, 0, hashBytes, 0, saltBytesLength);
            Array.Copy(hash, 0, hashBytes, saltBytesLength, passwordBytesLength);

            string hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public static bool IsPasswordValid(string password, string storedHash)
        {
            // Extracts stored Hash
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);
            // Gets salt from hash
            byte[] salt = new byte[saltBytesLength];
            Array.Copy(storedHashBytes, 0, salt, 0, saltBytesLength);

            // Extracts password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterlation);
            byte[] inputHashBytes = pbkdf2.GetBytes(passwordBytesLength);

            // Compares result
            for (int i = 0; i < passwordBytesLength; i++)
            {
                if (storedHashBytes[i + saltBytesLength] != inputHashBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetFileName(Image image)
        {
            if (image.Id < 0)
            {
                throw new Exception("invalid Id");
            }

            if (string.IsNullOrEmpty(image.MimeType))
            {
                throw new Exception("invalid file mime type");
            }

            string extension = MimeTypesMap.GetExtension(image.MimeType);

            return image.Id + "." + extension;
        }

        public static string GetImagesDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), IMAGES_DIRECTORY);
        }

        public static string GetMimeType(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new Exception("empty filename");
            }

            string extension = filename.Split('.')[1];
            if (string.IsNullOrEmpty(extension))
            {
                throw new Exception("no extension found");
            }

            return MimeTypesMap.GetMimeType(extension);
        }

        public static string GetArchiveDirectory(string childDirectory)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), ARCHIVE_DIRECTORY, childDirectory);
        }
    }

    public enum Currency
    {
        EUR, 
        USD
    }
}
