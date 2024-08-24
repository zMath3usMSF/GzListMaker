string caminhoArquivo = "gzlist.txt";
try
{
    if (File.Exists(caminhoArquivo))
    {
        List<string> folderString = new List<string>();
        List<List<string>> filesString = new List<List<string>>();
        List<List<int>> filesSize = new List<List<int>>();
        List<int> folderFilesCount = new List<int>();
        string[] lines = File.ReadAllLines(caminhoArquivo);
        int start = 0;
        for(int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "------------------- ----- ------------ ------------  ------------------------")
            {
                start = i + 1;
                break;
            }
        }
        folderString.Add("root");
        folderFilesCount.Add(0);
        filesString.Add(new List<string>());
        filesSize.Add(new List<int>());
        for (int i = start; i < lines.Length; i++)
        {
            if (lines[i] == "------------------- ----- ------------ ------------  ------------------------")
            {
                break;
            }
            string[] parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 4)
            {
                filesString.Add(new List<string>());
                filesSize.Add(new List<int>());
                folderString.Add(parts[3]);
                string[] folderParts = parts[3].Split('\\');
                if(folderParts.Length == 1)
                {
                    folderFilesCount[0]++;
                }
                if (folderParts.Length == 2)
                {
                    for (int j = 0; j < folderString.Count; j++)
                    {
                        if(folderParts[0] == folderString[j])
                        {
                            folderFilesCount[j]++;
                            break;
                        }
                    }
                }
                folderFilesCount.Add(0);
            }
            else
            {
                string currentFileS = parts[5];
                string[] fileParts = currentFileS.Split('\\');
                if (fileParts.Length != 1)
                {
                    string currentFileSFolder = currentFileS.Replace($"{fileParts[fileParts.Length - 1]}", "");
                    for (int j = 0; j < folderString.Count; j++)
                    {
                        if (currentFileSFolder == folderString[j] + "\\")
                        {
                            folderFilesCount[j]++;
                            if (currentFileS.Contains(".CCS"))
                            {
                                filesString[j].Add(parts[5]);
                                filesSize[j].Add(int.Parse(parts[4]));
                            }
                            break;
                        }
                    }
                }
                else
                {
                    folderFilesCount[0]++;
                    if (currentFileS.Contains(".CCS"))
                    {
                        filesString[0].Add(parts[5]);
                        filesSize[0].Add(int.Parse(parts[4]));
                    }
                }
            }
        }
        List<string> output = new List<string>();
        output.Add($"#\t\tname\t\t\tnum");
        for(int i = 0; i < folderString.Count; i++)
        {
            string currentString = folderString[i] != "root" ? folderString[i] + "/" : folderString[i];
            currentString = currentString.Replace("\\", "/").ToLower();
            output.Add($"\t\t{currentString.PadRight(15)}\t\t{folderFilesCount[i]}");
        }
        output.Add($"\t\tbinEnd");
        output.Add($"#\t\tname\t\t\tsize\t\tgzip");
        output.Add($"\tdata\t0x0");
        for(int i = 0; i < filesString.Count; i++)
        {
            for (int j = 0; j < filesString[i].Count; j++)
            {
                string currentFileSize = $"{filesSize[i][j]:X8}".ToLower();
                output.Add($"\t\t{filesString[i][j].PadRight(15)}\t\t0x{0:X8}\t0x{filesSize[i][j]:X8}".ToLower());
            }
        }
        output.Add($"\t\tbinEnd");
        output.Add($"binFileEnd");
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        File.WriteAllLines("gzlist.txt", output.ToArray());
    }
    else
    {
        Console.WriteLine($"O arquivo '{caminhoArquivo}' não foi encontrado.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}