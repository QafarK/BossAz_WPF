using BossAz_WPF.Models.DataBaseModels;
using BossAzWPF;
using System.Collections.ObjectModel;
using System.IO;

namespace BossAz_WPF.AppDBContext;

public class AppDbContext
{
    public static List<string> Users=[];

    public AppDbContext()
    {
        using FileStream fileRead = new(App.filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fileRead);

        do
        {
            string? line = reader.ReadLine();
            if (line is not null)
                Users.Add(line);
        } while (!reader.EndOfStream);
    }

}
