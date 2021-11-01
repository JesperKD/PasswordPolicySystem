using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLoginDemo.Data.DataModels;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Repositories
{
    public class FileLoginRepository : ILoginRepository
    {
        private readonly IFileSettings _fileSettings;

        public FileLoginRepository(IFileSettings fileSettings)
        {
            _fileSettings = fileSettings;

            if (string.IsNullOrEmpty(fileSettings.FileName))
            {
                throw new NullReferenceException("Filename not given");
            }

            if (File.Exists(_fileSettings.Path))
            {
                return;
            }

            var sr = File.Create(_fileSettings.Path);
            sr.Close();
        }

        public async Task<bool> CheckLogin(Login login)
        {
            var allLines = await WriteSafeReadAllLinesAsync(_fileSettings.Path);

            for (int i = 0; i < allLines.Length; i++)
            {
                string[] splitLine = SplitLine(allLines[i]);

                if (splitLine[0].Equals(login.Username))
                {
                    if (splitLine[1].Equals(login.Password))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task CreateAsync(Login createEntity)
        {
            var joinedString = string.Join(_fileSettings.Delimiter, createEntity.Username, createEntity.Password, createEntity.Attempts.ToString());

            using (StreamWriter file = new(_fileSettings.Path, append: true))
            {
                await file.WriteLineAsync(joinedString);
            }
        }

        public async Task<IEnumerable<Login>> GetAllAsync()
        {

            var lines = await WriteSafeReadAllLinesAsync(_fileSettings.Path);

            List<Login> logins = new();


            for (int i = 0; i < lines.Length; i++)
            {
                var splitLine = SplitLine(lines[i]);

                var parsedAttempts = int.TryParse(splitLine[2], out int attempts);

                logins.Add(new(
                      username: splitLine[0],
                      password: splitLine[1],
                      attempts: parsedAttempts == true ? attempts : 0
                      ));

            }

            return logins;
        }

        public Task<Login> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        private string[] SplitLine(string raw)
        {

            string[] split = raw.Split(_fileSettings.Delimiter);

            return split;
        }

        private async Task<string[]> WriteSafeReadAllLinesAsync(String path)
        {
            using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(csv))
                {
                    List<string> file = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        file.Add(await sr.ReadLineAsync());
                    }

                    return file.ToArray();
                }
            }
        }
    }
}
